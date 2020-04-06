using Cinemachine;
using Managers;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class BallMove : MonoBehaviour
{
    #region Singleton

    public static BallMove main;
    private CinemachineBrain mainCamBrain;

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

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>(); //We reach our rigidbody at start method.
        mainCamBrain = FindObjectOfType<CinemachineBrain>();
    }

    private void FixedUpdate()
    {
        if (Time.frameCount % 3 == 0)
        {
            if (!GameManager.main.shootTheBall)
                return; //If player enters the inputs, global manager will set the trigger for movement.
            Movement();
        }
    }

    //This is the script for ball movement, Ball will move to goalkeepers selected positions.
    private void Movement()
    {
        var gameManagerPos = GameManager.main.transformPositionToShoot.position; //The position for the ball to reach, it was taken via players input.

        var position = transform.position; //This is for performance clearity.
        

        if (!GameManager.main.ballMoveStop) //If this trigger is not set by game manager, ball gets a force to reach the position.
        {
            rb.AddForce((gameManagerPos - position).normalized *
                        (GameManager.main.ballShootPowerValue * Time.fixedDeltaTime * 50)
                , ForceMode.Impulse); //We set rigidbody force, because without physics we have lag
           
        }

        if ((transform.position - gameManagerPos).sqrMagnitude < 7 && GameManager.main.ballGoesToHead) //This is for slow motion situations.
                                                                                                       //If player makes perfect hit. Ball will slow down and hit to the head.
        {
            TimeManager.main.SlowMotion();
            GameManager.main.ballMoveStop = true;
            transform.localScale = new Vector3(.25f, .25f, .25f);
            GameManager.main.cineMachines[1].SetActive(false);
            mainCamBrain.m_DefaultBlend.m_Time = .05f;
            GameManager.main.goalKeeperAnim.SetBool("HeadHit",true);
        }
        else if ((transform.position - gameManagerPos).sqrMagnitude < 7 &&
                 GameManager.main.goalkeepersPositionArrayValue == 2)
        {
            GameManager.main.goalKeeperAnim.SetBool("MidHit",true);
        }
        else if ((transform.position - gameManagerPos).sqrMagnitude < 7 &&
                 GameManager.main.goalkeepersPositionArrayValue == 1)
        {
            GameManager.main.goalKeeperAnim.SetBool("LegHit",true);
        }

        if ((transform.position - gameManagerPos).sqrMagnitude < .1f) //This is for camera follow stop and slow motion stop.
        {
            //Bu kısımda can scriptini tetikleyebilirsin
            if(GameManager.main.ballGoesToHead) TimeManager.main._timeFix = true;
            
            //CameraFollow.main
            GameManager.main.ballMoveStop = true;
        }

        if (!((transform.position - gameManagerPos).sqrMagnitude > 5f) || !GameManager.main.camStopFollow) 
        {
             ShootSystem.instance.goalKeeperHUD.SetHp(50);
                //Debug.Log(ShootSystem.instance.goalKeeperHUD);
                StartCoroutine(ShootSystem.instance.PlayerAttack());
        }

        //rb.AddForce(Vector3.forward);
    }
}