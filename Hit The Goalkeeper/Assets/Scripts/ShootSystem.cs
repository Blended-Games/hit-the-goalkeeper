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
            CameraControls.main.CameraFixOffset();
            var transform1 = GameManager.main.p2Pos.transform;
            var transform2 = GameManager.main.p2.transform;
            transform2.position = transform1.position;
            transform2.rotation = transform1.rotation;
            GameManager.main.ballAttackValue = 0;
            GameManager.main.ballCurveValue = 0;
            GameManager.main.firstTouch = false;
            state = PlayerState.GoalKeeperTurn;
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
            CameraControls.main.CameraFixOffset();
            var transform1 = GameManager.main.p1Pos.transform;
            GameManager.main.p1.transform.position = transform1.position;
            GameManager.main.p1.transform.rotation = transform1.rotation;
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