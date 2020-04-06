using System.Collections;
using System.Collections.Generic;
using GUI;
using Managers;
using UnityEngine;


public enum PlayerState{PlayerTurn,GoalKeeperTurn,Won,Lost}
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
   public  Unit unitPlayer;
   public  Unit unitGoalKeeper;
    public HUDScript playerHUD;
    public HUDScript goalKeeperHUD;
    

    // Start is called before the first frame update
    void Start()
    {
      state= PlayerState.PlayerTurn;
    }
    public void SetupShoot()
    {
    // playerHUD.SetHud(unitPlayer);
   goalKeeperHUD.SetHud(unitPlayer);

  
    }
  public  IEnumerator PlayerAttack(){
    bool isDead=  unitGoalKeeper.TakeDamage(unitPlayer.damage);
      Debug.Log(unitPlayer.currentHP);
       goalKeeperHUD.SetHp(unitPlayer.currentHP);

    yield return new WaitForSeconds(0.2f);

    if(isDead)
    {
        state=PlayerState.Won;
        EndShoot();
    }
    else{
        state=PlayerState.GoalKeeperTurn;
        StartCoroutine(GoalKeeperTurn());
    }
    }
   
  public IEnumerator GoalKeeperTurn(){ 
       bool isDead= unitPlayer.TakeDamage(unitGoalKeeper.damage);
      unitGoalKeeper.currentHP=(int)Random.Range(5f,25f);
       playerHUD.SetHp(unitGoalKeeper.currentHP);
       yield return new WaitForSeconds(.2f);
        if(isDead)
           {
             state=PlayerState.Lost;
             EndShoot();
         }
        else{
              state=PlayerState.PlayerTurn;

        }
     
     }

       void EndShoot(){
            if(state==PlayerState.Won) Debug.Log( "you won");
            else if(state==PlayerState.Lost) Debug.Log( "you lost");
       }
}

    


