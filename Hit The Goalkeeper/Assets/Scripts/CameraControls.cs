using Accessables;
using DG.Tweening;
using Managers;
using UnityEngine;

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

    public Transform target; // Target to follow
    private Vector3 targetLastPos, desiredPosition;

    [TextArea] [SerializeField]
    private string messageForTheArtists = "You can change the offset of the camera follow thru here.";

    public Vector3 offsetPlayer, offsetGoalkeeper;
    
    #endregion

    #region CamFollow

    void Start()
    {
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

        var smooothedPosition = Vector3.Lerp(transform.position, desiredPosition, .125f);
        var transform1 = transform;
        transform1.position = smooothedPosition;

        //transform.LookAt(target);
        
    }

    #endregion


    public void StartFieldOfViewChangeMainCam()
    {
        if (!easeActive)
        {
            DoTweenController.CameraFieldOfViewChange(_camera,fieldOfViewEndValue,fieldOfViewFirstValue,duration);
        }
        else if (easeActive)
        {
            DoTweenController.CameraFieldOfViewChangeWithEase(_camera,fieldOfViewEndValue,fieldOfViewFirstValue,duration, ease);
        }
    }
}