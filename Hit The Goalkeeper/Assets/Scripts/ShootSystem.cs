using Accessables;
using FlurrySDK;
using GameAnalyticsSDK;
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
        state = PlayerState.GoalKeeperTurn;
        BallMove.ChangeKeeper();
    }

    public void GoalKeeperAttack()
    {
        GameManager.main.powerBarIndicatorParent.SetActive(true);
        DisplayMessage.main.powerBarText.text = null;
        DisplayMessage.main.powerBarText.enabled = true;
        state = PlayerState.PlayerTurn;
        PowerBarIndicator.main.enabled = true;
    }

    public void EndShoot()
    {
        if (state == PlayerState.Won)
        {
            LevelSetter.main.goalKeeperAnim.SetBool("Dead", true);
            PlayerWonAnimation();
            DoTweenController.FirstDelayThenMoveAndRotateAndCallback(BallMove.main._camera.transform,
                new Vector3(0.4f, 1, -6),
                new Vector3(10, 180, 0), 3, 1.15f, LevelAfterPanel);
            Vibrations.VibrationSuccess();

            GameData.GameCurrencySave((PlayerPrefs.GetInt("highlevel") + 1) * 50);

            AnalyticsHTGK.AnalyticsLevelSuccess(PlayerPrefs.GetInt("highlevel").ToString(),
                LevelManager.Main.thisLevel.ToString());
        }
        else if (state == PlayerState.Lost)
        {
            LevelSetter.main.playerAnim.SetBool("Dead", true);
            PLayerLostAnimation();
            DoTweenController.FirstDelayThenMoveAndRotateAndCallback(BallMove.main._camera.transform,
                new Vector3(-.85f, 1, -4),
                new Vector3(10f, 0f, 0), 3, 1.15f, LevelAfterPanel);
            Vibrations.VibrationFail();
            GameData.GameCurrencySave(((PlayerPrefs.GetInt("highlevel") + 1) * 50) / 2);

            AnalyticsHTGK.AnalyticsLevelFail(PlayerPrefs.GetInt("highlevel").ToString(),
                LevelManager.Main.thisLevel.ToString());
        }
    }

    private void LevelAfterPanel()
    {
        switch (state)
        {
            case PlayerState.Lost:
                GameManager.main.levelFailedPanel.SetActive(true);
                break;
            case PlayerState.Won:
                GameManager.main.levelSuccessPanel.SetActive(true);
                break;
        }
    }

    public void PanelHealthDisplayGoalkeeper()
    {
        playerHUD.SetHp(unitGoalKeeper.damage);
        playerHUD.SetDamage(unitGoalKeeper.damage);
    }

    public void PanelHealthDisplayPlayer()
    {
        goalKeeperHUD.SetHp(unitPlayer.damage);
        goalKeeperHUD.SetDamage(unitPlayer.damage);
    }

    private static void PlayerWonAnimation()
    {
        var rand = Random.Range(1, 4);
        switch (rand)
        {
            case 1:
                LevelSetter.main.playerAnim.SetBool("Samba", true);
                break;
            case 2:
                LevelSetter.main.playerAnim.SetBool("HipHop", true);
                break;
            case 3:
                LevelSetter.main.playerAnim.SetBool("Chicken", true);
                break;
            case 4:
                LevelSetter.main.playerAnim.SetBool("Victory", true);
                break;
        }
    }

    private static void PLayerLostAnimation()
    {
        var rand = Random.Range(1, 4);
        switch (rand)
        {
            case 1:
                LevelSetter.main.goalKeeperAnim.SetBool("Samba", true);
                break;
            case 2:
                LevelSetter.main.goalKeeperAnim.SetBool("HipHop", true);
                break;
            case 3:
                LevelSetter.main.goalKeeperAnim.SetBool("Chicken", true);
                break;
            case 4:
                LevelSetter.main.goalKeeperAnim.SetBool("Victory", true);
                break;
        }
    }
}