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
            state = PlayerState.GoalKeeperTurn;
            GameManager.main.firstTouch = false;
            BallMove.main.ChangeKeeper();
        }
    }

    public void GoalKeeperAttack()
    {
        var isDead = unitPlayer.TakeDamage(unitGoalKeeper.damage);

        playerHUD.SetHp(unitGoalKeeper.damage);
        if (isDead)
        {
            state = PlayerState.Lost;
            EndShoot();
        }
        else
        {
            GameManager.main.firstTouch = true;
            
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