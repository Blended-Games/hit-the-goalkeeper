using System;
using Accessables;
using Managers;
using UnityEngine;
using UnityEngine.Serialization;
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

    // public int fieldOfViewEndValue, fieldOfViewFirstValue; //This is the end value for camera zoom.
    // public float duration; //This is the duration for camera zoom effect.
    // public bool easeActive;
    // public Ease ease;
    public bool camFollowStop;

    public Transform target; // Target to follow
    private Vector3 _targetLastPos, _desiredPosition;

    // [TextArea] [SerializeField]
    // private string messageForTheArtists = "You can change the offset of the camera follow thru here.";

    public Vector3 offsetPlayer, offsetGoalkeeper;
    [FormerlySerializedAs("p1CamClose")] public Vector3 p1CamFaceClose;
    [FormerlySerializedAs("p2CamClose")] public Vector3 p2CamFaceClose;
    public Vector3 p1CamClose, p2camClose;

    #endregion

    #region CamFollow

    private void FixedUpdate()
    {
        switch (ShootSystem.instance.state)
        {
            case PlayerState.PlayerTurn:
                _desiredPosition = target.position - offsetPlayer;
                break;
            case PlayerState.GoalKeeperTurn:
                _desiredPosition = target.position - offsetGoalkeeper;
                break;
            case PlayerState.Won:
                break;
            case PlayerState.Lost:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        if (GameManager.main.ballsHitRoad == TransformPosition.Off && !camFollowStop) return;
        var transform2 = transform;
        var position = transform2.position;
        var smooothedPosition = new Vector3(Mathf.Lerp(position.x, _desiredPosition.x, .125f),
            Mathf.Lerp(position.y, _desiredPosition.y, .125f),
            Mathf.Lerp(position.z, _desiredPosition.z, .125f));
        var transform1 = transform;
        transform1.position = smooothedPosition;
    }

    #endregion


    /*public void StartFieldOfViewChangeMainCam()
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
    }*/

    public void CameraGetCloser()
    {
        camFollowStop = true;
        if (!camFollowStop) return;
        switch (ShootSystem.instance.state)
        {
            case PlayerState.PlayerTurn:
                if (GameManager.main.ballsHitRoad == TransformPosition.Head)
                {
                    DoTweenController.SequenceMoveAndRotate(this.transform, p1CamFaceClose, new Vector3(0, 0, 0), 2f);
                }
                else
                {
                    DoTweenController.SequenceMoveAndRotate(this.transform, p1CamClose, new Vector3(0, 0, 0), 2f);

                }
                break;
            case PlayerState.GoalKeeperTurn:
                if (GameManager.main.ballsHitRoad == TransformPosition.Head)
                {
                    DoTweenController.SequenceMoveAndRotate(this.transform, p2CamFaceClose, new Vector3(0, -180, 0), 2f);
                }
                else
                {
                    DoTweenController.SequenceMoveAndRotate(this.transform, p2camClose, new Vector3(0, -180, 0), 2f);

                }
                break;
            case PlayerState.Won:
                break;
            case PlayerState.Lost:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    /*
    public void CameraFixOffset()
    {
        offsetPlayer = oldOffsetPlayer;
        offsetGoalkeeper = oldOffSetGoalPlayer;
    
    }*/
}