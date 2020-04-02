using System.Collections;
using UnityEngine;

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

    [Header("Global Dependencies")] public float ballShootPowerValue; //Balls power value

    public Transform transformPositionToShoot; //This will be the position that we are shooting.
    
    public Animator ballAnimStartTrigger; //This trigger is enabling blendTrees trigger controller for next anim.

    public Animator goalKeeper; //This is the goalkeepers animator, we will change animations accessing from this parameter.
    
    public bool firstTouch; //This the first calculation bool, it controls the bars function.

    public Transform[] goalKeeperShootPositions; //The transforms of the keepers should start from the worst scenario,

    public bool ballMoveStart; //This is the controller for ball movement.
    
    //to best scenario (0 - legs, 1 - spine, etc.)
    public int calculationID; //This id is for changing the value for calculation in shooting mechanics. 

    #endregion

    #region TargetFrameRate

    private void Start()
    {
        Application.targetFrameRate = 30; //Setting the target frame rate for unexpected frame drop rates.
    }
    

    #endregion
}