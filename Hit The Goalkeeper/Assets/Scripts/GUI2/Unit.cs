 using UnityEngine;

public class Unit : MonoBehaviour
{
    #region Singleton

    public static Unit main;

    private void Awake()
    {
        main = this;
    }

    #endregion


    public int damage;

    public int damageUpgrade;

    public int maxHP;

    private void Start()
    {
        switch (gameObject.tag)
        {
            case "goalkeeper" when PlayerPrefs.GetInt("highlevel") == 0:
                GameData.SetGoalkeepersHealth(0);
                GameData.SetGoalkeepersDamage(0);
                break;
            case "Player" when PlayerPrefs.GetInt("healthUpgrade") == 0:
                GameData.SetPlayersHealth(0);
                break;
            case "Player" when PlayerPrefs.GetInt("healthUpgrade") > 0:
                maxHP = PlayerPrefs.GetInt("PlayerMaxHP");
                break;
        }

        if (gameObject.CompareTag("Player") && PlayerPrefs.GetInt("damageUpgrade") == 0)
            GameData.SetPlayersDamage(0);
        else if (gameObject.CompareTag("Player") && PlayerPrefs.GetInt("damageUpgrade") > 0)
            GameData.SetPlayersDamage(1);

        if (!gameObject.CompareTag("goalkeeper") || PlayerPrefs.GetInt("highlevel") <= 0) return;
        GameData.SetGoalkeepersHealth(1);
        GameData.SetGoalkeepersDamage(1);
    }

    public bool TakeDamage(int dmg)
    {
        maxHP -= dmg;

        return maxHP <= 0;
    }
}