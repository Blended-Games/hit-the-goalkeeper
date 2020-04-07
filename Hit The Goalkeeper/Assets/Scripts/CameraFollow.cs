using Cinemachine;
using DG.Tweening;
using Managers;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    #region Singleton

    public static CameraFollow main;

    private void Start()
    {
        _cinemachineBrain = GetComponent<CinemachineBrain>();
        _cam = GameManager.main.cineMachines[1].GetComponent<CinemachineVirtualCamera>();
    }

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

    public bool
        isNotFollow; //This will be the trigger for following. We set this from Characters animation state exit. (Make changes)

    private CinemachineBrain _cinemachineBrain; //Cinemachine brain is for camera changes.
    private CinemachineVirtualCamera _cam; //This is the camera thats being zoomed.

    [TextArea] [Header("Message To Artists")] [SerializeField]
    private string ZoomEffect = "You can change the zoom effects values from this area.";

    public int fieldOfViewEndValue; //This is the end value for camera zoom.
    public float duration; //This is the duration for camera zoom effect.
    public bool easeActive;
    public Ease ease;

    #endregion

    #region CamFollow

    public void CinemacHineClose()
    {
        _cinemachineBrain.enabled = false; //Setting this false because we don't want to follow anymore.
    }

    #endregion

    #region CameraFieldOFViewChange

    //This function is changing the zooming effect of the camera.
    public void StartFieldOfViewChange()
    {
        if (!easeActive)
        {
            DOTween.defaultAutoKill = false;
            var anim = DOTween.To(() => _cam.m_Lens.FieldOfView, x => _cam.m_Lens.FieldOfView = x, fieldOfViewEndValue,
                duration);
            anim.Play();
            anim.Complete();
            anim.PlayBackwards();
        }
        else if (easeActive)
        {
            DOTween.defaultAutoKill = false;
            var anim = DOTween.To(() => _cam.m_Lens.FieldOfView, x => _cam.m_Lens.FieldOfView = x, fieldOfViewEndValue,
                duration).SetEase(ease);
            anim.Play();
            anim.Complete();
            anim.PlayBackwards();
        }
    }

    #endregion
}