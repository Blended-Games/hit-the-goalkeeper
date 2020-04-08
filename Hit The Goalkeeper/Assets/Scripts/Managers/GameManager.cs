using UnityEngine;

namespace Managers
{
    public enum TransformPosition
    {
        Head,
        Spine,
        Leg
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

        [Header("Global Dependencies")] public float ballShootPowerValue;
        public float ballAttackValue; //Balls power value

        public UnityEngine.Vector3 transformPositionToShoot; //This will be the position that we are shooting.

        public Animator playerAnim; //This trigger is enabling blendTrees trigger controller for next anim.

        public bool firstTouch; //This the first calculation bool, it controls the bars function.

        [TextArea] [SerializeField] private string messageForArtists =
            "You need to add the corresponding transform positions to these arrays. " +
            "Start from the worst condition (0 - leg, 1 - spine, 2 - head, etc..).";

        public Transform[]
            goalKeeperShootPositions,
            playerShootPositions; //The transforms of the keepers should start from the worst scenario,
        //(0 - legs, 1 - spine, etc.)

        [TextArea] [SerializeField] private string messageForArtists2 =
            "You need to add the corresponding ball positions to these Transforms." +
            "Ball wil transform to those areas on different game states.";

        public Transform p1BallsTransform, p2BallsTransform; //These will be the positions for the balls.

        [TextArea] [SerializeField] private string messageForArtists3 =
            "You need to add the corresponding camera positions to these Transforms. " +
            "I suggest you to add the camera transform position and make it a prefab with the character.";

        public Transform
            p1sCameraPosition, p2sCameraPosition; //These are the positions for the cameras to move on different states.

        public bool shootTheBall; //This will be the trigger for balls movement;

        public bool ballMoveStop; //This will stop rigidbody force and make 

        public bool
            camStopFollow; //This is the condition for offrecords movement. Because we do not want to follow the ball from there.

        public Animator goalKeeperAnim; //This will be animator for goalkeeper's animation triggers.

        public int calculationID; //This id is for changing the value for calculation in shooting mechanics. 

        public TransformPosition ballsHitRoad; //This will be the state for controlling balls transform point. 

        #endregion

        #region TargetFrameRate

        private void Start()
        {
            Application.targetFrameRate = 30; //Setting the target frame rate for unexpected frame drop rates.
        }

        #endregion
    }
}