using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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
        state = PlayerState.PlayerTurn;
    }

    public void SetupShoot()
    {
        playerHUD.SetHud(unitPlayer);
        goalKeeperHUD.SetHud(unitGoalKeeper);
    }

    public void PlayerAttack()
    {
        var isDead = unitGoalKeeper.TakeDamage(unitPlayer.damage);

        goalKeeperHUD.SetHp(unitPlayer.damage);

        if (isDead)
        {
            state = PlayerState.Won;
            EndShoot();
        }
        else
        {
            GameManager.main.ballAttackValue = 0;
            GameManager.main.firstTouch = false;
            //GameManager.main.playersAnimMoveStart = false;
            state = PlayerState.GoalKeeperTurn;
            Debug.Log("State goalkeepera geçti");
            BallMove.main.ChangeKeeper();
            
        }
    }

    public void GoalKeeperAttack()
    {
        Debug.Log("Goalkeeper Attacka girildi");
        var isDead = unitPlayer.TakeDamage(unitGoalKeeper.damage);

        playerHUD.SetHp(unitGoalKeeper.damage);
        if (isDead)
        {
            state = PlayerState.Lost;
            EndShoot();
        }
        else
        {
            Debug.Log("Goalkeepers state end");
            GameManager.main.firstTouch = true;
            GameManager.main.powerBarIndicatorParent.SetActive(true);
            state = PlayerState.PlayerTurn;

        }
    }

    void EndShoot()
    {
        if (state == PlayerState.Won)
        {
            Debug.Log("You won");
            GameManager.main.levelChange.SetActive(true);
        }
        else if (state == PlayerState.Lost) Debug.Log("you lost");
    }
}