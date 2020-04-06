using Managers;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class BallMove : MonoBehaviour
{
    #region Singleton

    public static BallMove main;

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
    }

    private void FixedUpdate()
    {
        if (!GameManager.main.shootTheBall) 
    
        return; //If player enters the inputs, global manager will set the trigger for movement.
        Movement();
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

        if ((transform.position - gameManagerPos).sqrMagnitude < 3 && GameManager.main.ballGoesToHead) //This is for slow motion situations.
                                                                                                       //If player makes perfect hit. Ball will slow down and hit to the head.
        {
            TimeManager.main.SlowMotion();
            CameraFollow.main.offset = new Vector3(.65f, -.16f, -.93f);
            CameraFollow.main.target = GameManager.main.transformPositionToShoot;
            GameManager.main.ballMoveStop = true;
            transform.localScale = new Vector3(.25f, .25f, .25f);               
        }

        if ((transform.position - gameManagerPos).sqrMagnitude < 1f) //This is for camera follow stop and slow motion stop.
        {
<<<<<<< Updated upstream
            AttackCompleted();
            if(GameManager.main.ballGoesToHead) TimeManager.main._timeFix = true;           
            CameraFollow.main.isNotFollow = true;
=======
            //Bu kısımda can scriptini tetikleyebilirsin

                //Debug.Log(ShootSystem.instance.goalKeeperHUD);
                StartCoroutine(ShootSystem.instance.PlayerAttack());
            if(GameManager.main.ballGoesToHead) TimeManager.main._timeFix = true;
            AttackCompleted();
            CameraFollow.main.CinemacHineClose();
>>>>>>> Stashed changes
            GameManager.main.ballMoveStop = true;
     
        }

        if (!((transform.position - gameManagerPos).sqrMagnitude > 5f) || !GameManager.main.camStopFollow) 
<<<<<<< Updated upstream
        { 
            return; //This is the condition for camera follow.
        }
       else{   
           CameraFollow.main.isNotFollow = true;
   
    
=======
        {
<<<<<<< Updated upstream
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
        }
    }

    public void AttackCompleted(){
         ShootSystem.instance.goalKeeperHUD.SetHp((int)GameManager.main.ballShootPowerValue);
         GameManager.main.shootTheBall=false;
      //   StartCoroutine (ShootSystem.instance.PlayerAttack());
    }
       public void AttackCompleted(){
           
      ShootSystem.instance.unitPlayer.currentHP=(int) GameManager.main.ballShootPowerValue;
     StartCoroutine( ShootSystem.instance.PlayerAttack());
        GameManager.main.shootTheBall=false;
    }

}