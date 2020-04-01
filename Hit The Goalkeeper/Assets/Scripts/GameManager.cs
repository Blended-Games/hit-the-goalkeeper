using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton

    public static GameManager main;

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

    [Header("Global Dependencies")] public int ballShootPowerValue; //Balls power value

    public Animator ballAnimStartTrigger; //This trigger is enabling blendTrees trigger controller for next anim.

    public bool firstTouch; //This the first calculation bool, it controls the bars function.

    public Transform[] goalKeeperShootPositions; //The transforms of the keepers should start from the worst scenario,

    //to best scenario (0 - legs, 1 - spine, etc.)
    public int calculationID; //This id is for changing the value for calculation in shooting mechanics. 

    #endregion

    #region TargetFrameRate

    private void Start()
    {
        Application.targetFrameRate = 60; //Setting the target frame rate for unexpected frame drop rates.
    }

    #endregion
}