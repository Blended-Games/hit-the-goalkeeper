using Accessables;
using DG.Tweening;
using Managers;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class CameraControls : MonoBehaviour
{
    #region Singleton

    public static CameraControls main;

    private void Awake()
    {
        if (main != null && main != this)
        {
            Destroy(gameObject);
            return;
        }

        main = this;
    }

    #endregion

    #region Variables

    [TextArea] [Header("Message To Artists")] [SerializeField]
    private string ZoomEffect = "You can change the zoom effects values from this area.";

    public int fieldOfViewEndValue, fieldOfViewFirstValue; //This is the end value for camera zoom.
    public float duration; //This is the duration for camera zoom effect.
    public bool easeActive;
    public Ease ease;
    private Camera _camera;
    public bool camFollowStop;

    public Transform target; // Target to follow
    private Vector3 targetLastPos, desiredPosition;

    [TextArea] [SerializeField]
    private string messageForTheArtists = "You can change the offset of the camera follow thru here.";

    public Vector3 offsetPlayer, offsetGoalkeeper;
    private Vector3 oldOffsetPlayer, oldOffSetGoalPlayer;

    #endregion

    #region CamFollow

    void Start()
    {
        oldOffsetPlayer = offsetPlayer;
        oldOffSetGoalPlayer = offsetGoalkeeper;
        _camera = Camera.main;
    }

    private void FixedUpdate()
    {
        if (ShootSystem.instance.state == PlayerState.PlayerTurn)
        {
            desiredPosition = target.position - offsetPlayer;
        }
        else if (ShootSystem.instance.state == PlayerState.GoalKeeperTurn)
        {
            desiredPosition = target.position - offsetGoalkeeper;
        }

        if (GameManager.main.ballsHitRoad != TransformPosition.Off || camFollowStop)
        {
            var transform2 = transform;
            var position = transform.position;
            var smooothedPosition = new Vector3(Mathf.Lerp(position.x, desiredPosition.x, .125f),
                Mathf.Lerp(position.y, desiredPosition.y, .125f),
                Mathf.Lerp(position.z, desiredPosition.z, .125f));
            var transform1 = transform;
            transform1.position = smooothedPosition;
        }

        //transform.LookAt(target);
    }

    #endregion


    public void StartFieldOfViewChangeMainCam()
    {
        if (!easeActive)
        {
            DoTweenController.CameraFieldOfViewChange(_camera, fieldOfViewEndValue, fieldOfViewFirstValue, duration);
        }
        else if (easeActive)
        {
            DoTweenController.CameraFieldOfViewChangeWithEase(_camera, fieldOfViewEndValue, fieldOfViewFirstValue,
                duration, ease);
        }
    }

    public void CameraGetCloser()
    {
        camFollowStop = true;
        if (GameManager.main.ballsHitRoad != TransformPosition.Off || camFollowStop)
        {
            offsetPlayer = Vector3.Lerp(offsetPlayer, new Vector3(0, .1f, .8f), 1); 
            offsetGoalkeeper = Vector3.Lerp(offsetGoalkeeper, new Vector3(0f, -.08f, -.8f), 1);
            // if (ShootSystem.instance.state == PlayerState.PlayerTurn)
            //     DoTweenController.DoLocalMove3D(transform, offsetPlayer, 1);
            // else if (ShootSystem.instance.state == PlayerState.GoalKeeperTurn)
            //     DoTweenController.DoLocalMove3D(transform, offsetGoalkeeper, 1);
        }
    }

    public void CameraFixOffset()
    {
        offsetPlayer = oldOffsetPlayer;
        offsetGoalkeeper = oldOffSetGoalPlayer;
    }
}