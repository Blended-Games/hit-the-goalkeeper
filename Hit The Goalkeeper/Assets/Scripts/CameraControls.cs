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

    public int fieldOfViewEndValue; //This is the end value for camera zoom.
    public float duration; //This is the duration for camera zoom effect.
    public bool easeActive;
    public Ease ease;
    private Camera _camera;

    public Transform target; // Target to follow
    private Vector3 targetLastPos, desiredPosition;

    [TextArea] [SerializeField]
    private string messageForTheArtists = "You can change the offset of the camera follow thru here.";

    public Vector3 offset;
    Tweener tween;

    #endregion

    #region CamFollow

    void Start()
    {
        _camera = Camera.main;
        // First create the "move to target" tween and store it as a Tweener.
        // In this case I'm also setting autoKill to FALSE so the tween can go on forever
        // (otherwise it will stop executing if it reaches the target)
        if (GameManager.main.camStopFollow) return;
        desiredPosition = target.position + offset;
        tween = transform.DOMove(desiredPosition, duration).SetEase(ease).SetAutoKill(false);
        // Store the target's last position, so it can be used to know if it changes
        // (to prevent changing the tween if nothing actually changes)
        targetLastPos = target.position;
    }

    private void Update()
    {
        // Use an Update routine to change the tween's endValue each frame
        // so that it updates to the target's position if that changed
        if (GameManager.main.camStopFollow) return;
        if (targetLastPos == target.position) return;
        // Add a Restart in the end, so that if the tween was completed it will play again
        tween.ChangeEndValue(desiredPosition, true).Restart();
        targetLastPos = target.position;
        
    }

    #endregion


    public void StartFieldOfViewChangeMainCam()
    {
        if (!easeActive)
        {
            _camera.DOFieldOfView(fieldOfViewEndValue, duration).PlayBackwards();
        }
        else if (easeActive)
        {
            _camera.DOFieldOfView(fieldOfViewEndValue, duration).SetEase(ease).PlayBackwards();
        }
    }
}