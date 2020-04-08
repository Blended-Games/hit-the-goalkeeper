using System.Collections;
using System.Collections.Generic;
using GUI;
using Managers;
using UnityEngine;


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


    // Start is called before the first frame update
    void Start()
    {
      state= PlayerState.PlayerTurn;
    }

    public void SetupShoot()
    {
        playerHUD.SetHud(unitPlayer);
        goalKeeperHUD.SetHud(unitGoalKeeper);
    }

    public IEnumerator PlayerAttack()
    {
        bool isDead = unitGoalKeeper.TakeDamage(unitPlayer.damage);
      
        goalKeeperHUD.SetHp(unitPlayer.currentHP);

        yield return new WaitForSeconds(0.2f);

        if (isDead)
        {
            state = PlayerState.Won;
            EndShoot();
        }
        else
        {
            //state = PlayerState.GoalKeeperTurn;

              GameManager.main.firstTouch =false;
           //StartCoroutine(BallMove.main.ChangeKeeper());
         }
    }
    public IEnumerator GoalKeeperAttack()
    {
        bool isDead = unitPlayer.TakeDamage(unitGoalKeeper.damage);
       
        playerHUD.SetHp(unitGoalKeeper.currentHP);
        yield return new WaitForSeconds(.2f);
        if (isDead)
        {
            state = PlayerState.Lost;
            EndShoot();
        }
        else
        {
              GameManager.main.firstTouch =true;
              state=PlayerState.PlayerTurn;
        }
    }

    void EndShoot()
    {
        if (state == PlayerState.Won) Debug.Log("you won");
        else if (state == PlayerState.Lost) Debug.Log("you lost");
    }
}