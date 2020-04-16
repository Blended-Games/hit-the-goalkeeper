using System.Collections;
using System.Collections.Generic;
using Accessables;
using DG.Tweening;
using GUI2;
using Managers;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public enum PlayerState
{
    PlayerTurn,
    GoalKeeperTurn,
    Won,
    Lost
}

public class ShootSystem : MonoBehaviour
{
    #region Singleton

    public static ShootSystem instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    #endregion

    public PlayerState state;
    public Unit unitPlayer;
    public Unit unitGoalKeeper;
    public HUDScript playerHUD;
    public HUDScript goalKeeperHUD;
    private static readonly int Dead = Animator.StringToHash("Dead");
    private static readonly int Samba = Animator.StringToHash("Samba");
    void Start()
    {
        state = PlayerState.PlayerTurn;
    }

    public void SetupShoot()
    {
        playerHUD.SetHud(unitPlayer);
        goalKeeperHUD.SetHud(unitGoalKeeper);
    }

    public void PlayerAttack()
    {   
            //CameraControls.main.CameraFixOffset();
            DisplayMessage.main.powerBarText.enabled = false;
            state = PlayerState.GoalKeeperTurn;
            BallMove.main.ChangeKeeper();
    }

    public void GoalKeeperAttack()
    {
            //CameraControls.main.CameraFixOffset();
            GameManager.main.powerBarIndicatorParent.SetActive(true);
            DisplayMessage.main.powerBarText.text = null;
            DisplayMessage.main.powerBarText.enabled = true;
            state = PlayerState.PlayerTurn;
    }

   public  void EndShoot()
    {
        if (state == PlayerState.Won)
        {
            LevelSetter.main.goalKeeperAnim.SetBool(Dead,true);
            PlayerWonAnimation();
          //LevelSetter.main.playerAnim.SetBool("Samba",true);
           DoTweenController.FirstDelayThenMoveAndRotate(BallMove.main._camera.transform, new Vector3(-.5f, 2.08f, -4.55f),
            new Vector3(11.355f, -194.25f, 0), 10, 2);
            DisplayMessage.main.ShowPowerBarText(7);
            Vibrations.VibrationSuccess();
        }
        else if (state == PlayerState.Lost)
        {   
            LevelSetter.main.goalKeeperAnim.SetBool(Dead,true);
            LevelSetter.main.playerAnim.SetBool(Dead,true);
            PLayerLostAnimation();
            DoTweenController.FirstDelayThenMoveAndRotate(BallMove.main._camera.transform, new Vector3(-.5f, 2.08f, -4.55f),
            new Vector3(17.63f, -5.95f, 0), 10, 2);
            DisplayMessage.main.ShowPowerBarText(6);
            Vibrations.VibrationFail();
        }
    }

    public void PanelHealthDisplayGoalkeeper()
    {
        playerHUD.SetHp(unitGoalKeeper.damage);
    }

    public void PanelHealthDisplayPlayer()
    {
        goalKeeperHUD.SetHp(unitPlayer.damage);
    }

    protected void PlayerWonAnimation(){

        var rand= Random.Range(1,4);
          if (rand==1)
                LevelSetter.main.playerAnim.SetBool("Samba",true);                
         else if(rand==2)    
                LevelSetter.main.playerAnim.SetBool("HipHop",true);
         else if(rand==3)    
                LevelSetter.main.playerAnim.SetBool("Chicken",true);
         else if(rand==4)    
                LevelSetter.main.playerAnim.SetBool("Victory",true);    
        
    }

        protected void PLayerLostAnimation()
        {
         var rand= Random.Range(1,4);
            if (rand==1)
                LevelSetter.main.goalKeeperAnim.SetBool("Samba",true);
            else if(rand==2)    
                LevelSetter.main.goalKeeperAnim.SetBool("HipHop",true);
            else if(rand==3)    
                LevelSetter.main.goalKeeperAnim.SetBool("Chicken",true);
            else if(rand==4)    
                LevelSetter.main.goalKeeperAnim.SetBool("Victory",true);    
        }
       
  
}