using UnityEngine;

public class LevelSetter : MonoBehaviour
{
    #region Singleton

    public static LevelSetter main;
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

    #region Variables
    
    [Header("Hit Point")]
    public UnityEngine.Vector3 transformPositionToShoot; //This will be the position that we are shooting.
    [Header("Animators")]
    public Animator playerAnim; //This trigger is enabling blendTrees trigger controller for next anim.
    public Animator goalKeeperAnim; //This will be animator for goalkeeper's animation triggers.
    
    [Header("Transform Positions")]
    public Transform[]
        goalKeeperShootPositions,
        playerShootPositions; //The transforms of the keepers should start from the worst scenario,
    //(0 - legs, 1 - spine, etc.)
     public Transform
            p1sCameraPosition, p2sCameraPosition; //These are the positions for the cameras to move on different states.
    [Header("Ball and Char Transform Points")]
    public Transform p1BallsTransform, p2BallsTransform; //These will be the positions for the balls.

    public Transform
        p1Pos,
        p2Pos; //These are the positions for the characters. We need these because animation states are changing characters positions.

    [Header("Characters")]
    public GameObject p1, p2;

    [Header("Materials and Textures")]
    public Material[] renderTextureMaterials; //These are variables of the players.
    
    public Texture[] p1Textures; //These will be textures for the char. It will change its deformation.
    public Texture[] p2Textures; //These will be textures for the char. It will change its deformation.

    private CameraControls _camera;


    #endregion

    // Start is called before the first frame update
    private void Start()
    {
        SetPlayersFirstTexture();
        _camera = FindObjectOfType<CameraControls>();
    }


    public void ActivateCam()
    {
        _camera.enabled = true;
    }

    private void SetPlayersFirstTexture()
    {
        renderTextureMaterials[0].mainTexture = p1Textures[0];
        renderTextureMaterials[1].mainTexture = p2Textures[0];
    }
}