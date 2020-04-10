using UnityEngine;

namespace Managers
{
    public enum TransformPosition
    {
        Head,
        Spine,
        Leg,
        Off
    };

    public class GameManager : MonoBehaviour
    {
        #region Singleton

        public static GameManager main;
        [SerializeField] private float motion;

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

        #region Introducing Global Dependencies

        [Header("Global Dependencies")] public float ballAttackValue; //Balls power value

        public UnityEngine.Vector3 transformPositionToShoot; //This will be the position that we are shooting.

        public Animator playerAnim; //This trigger is enabling blendTrees trigger controller for next anim.

        public bool firstTouch; //This the first calculation bool, it controls the bars function.

        public Transform[]
            goalKeeperShootPositions,
            playerShootPositions; //The transforms of the keepers should start from the worst scenario,
        //(0 - legs, 1 - spine, etc.)

        public Transform p1BallsTransform, p2BallsTransform; //These will be the positions for the balls.

        public Transform
            p1sCameraPosition, p2sCameraPosition; //These are the positions for the cameras to move on different states.

        public bool
            camStopFollow; //This is the condition for offrecords movement. Because we do not want to follow the ball from there.

        public Animator goalKeeperAnim; //This will be animator for goalkeeper's animation triggers.

        public int calculationID; //This id is for changing the value for calculation in shooting mechanics. 

        public TransformPosition ballsHitRoad; //This will be the state for controlling balls transform point. 

        public GameObject levelChange; //This will be the button for level changing.

        public GameObject powerBarIndicatorParent; //This is the powerbar indicator script.

        public Transform
            p1Pos,
            p2Pos; //These are the positions for the characters. We need these because animation states are changing characters positions.

        public GameObject p1, p2;

        private CameraControls _camera;

        public float ballCurveValue; //This will be the value for balls left, right movement.

        public bool gameStop; //This is a temporary bool for camera settings.

        #endregion

        #region TargetFrameRate

        private void Start()
        {
            Application.targetFrameRate = 30; //Setting the target frame rate for unexpected frame drop rates.
            _camera = FindObjectOfType<CameraControls>();
        }

        public void ActivateCam()
        {
            _camera.enabled = true;
        }

        #endregion
    }
}