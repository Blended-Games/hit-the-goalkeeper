using UnityEngine;

public static class GameData
{
    public static bool UpgradeCurrencyControl(string key)
    {
        return
            PlayerPrefs.GetInt(key) >
            0; //Controling the level of this object. If its on level one we will set first costs, else cost will ben duplicated with the level.
    }

    public static bool CurrencyControl(int upgradeCost)
    {
        return PlayerPrefs.GetInt("currency") >= upgradeCost;
    }

    public static void BuyUpgrade(string key, int upgradecost)
    {
        if (CurrencyControl(upgradecost))
        {
            PlayerPrefs.SetInt("currency",
                PlayerPrefs.GetInt("currency") - upgradecost); //Decrease upgrade amount from the coin.
            CoinText.main.SetCoinText(); //Set new coin amount to coin text
            switch (key)
            {
                case "damageUpgrade":
                    SetPlayersDamage(1);
                    break;
                case "healthUpgrade":
                    SetPlayersHealth(1);
                    break;
            }

            PlayerPrefs.SetInt(key, PlayerPrefs.GetInt(key) + 1); //Increased the level of this upgrade
            UpgradeObjectsBehaviour.main.IncreaseCurrency();
        }
        else
        {
            //Debug.Log("Insufficient amounts" + " " + key);
            //Display insufficient amounts or buy with advertisement message or something.
        }
    }

    public static void SetPlayersDamage(int id)
    {
        if (id == 0)
        {
            ShootSystem.instance.unitPlayer.damageUpgrade = 1;
            PlayerPrefs.SetInt("PlayerDamage", ShootSystem.instance.unitPlayer.damageUpgrade);

        }

        else if (id == 1)
        {
            ShootSystem.instance.unitPlayer.damageUpgrade =
                PlayerPrefs.GetInt("PlayerDamage") + 1;
            PlayerPrefs.SetInt("PlayerDamage", ShootSystem.instance.unitPlayer.damageUpgrade);

        }
    }

    public static void SetPlayersHealth(int id)
    {
        if (id == 0)
        {
            ShootSystem.instance.unitPlayer.maxHP = 100;
            PlayerPrefs.SetInt("PlayerMaxHP", ShootSystem.instance.unitPlayer.maxHP);
            ShootSystem.instance.playerHUD.SetHud(ShootSystem.instance.unitPlayer);
        }

        else if (id == 1)
        {
            ShootSystem.instance.unitPlayer.maxHP =
                PlayerPrefs.GetInt("PlayerMaxHP") + ((PlayerPrefs.GetInt("PlayerMaxHP") * 7) / 100);
            PlayerPrefs.SetInt("PlayerMaxHP", ShootSystem.instance.unitPlayer.maxHP);        
            ShootSystem.instance.playerHUD.SetHud(ShootSystem.instance.unitPlayer);
        }
    }

    public static void SetGoalkeepersHealth(int id)
    {
        if (id == 0)
        {
            ShootSystem.instance.unitGoalKeeper.maxHP = 70;
            PlayerPrefs.SetInt("GoalkeeperMaxHP", Unit.main.maxHP);
            ShootSystem.instance.goalKeeperHUD.SetHud(ShootSystem.instance.unitGoalKeeper);
        }

        else if (id == 1)
        {
            Unit.main.maxHP = PlayerPrefs.GetInt("GoalkeeperMaxHP") +
                              ((PlayerPrefs.GetInt("GoalkeeperMaxHP") * 10) / 100);
            PlayerPrefs.SetInt("GoalkeeperMaxHP", ShootSystem.instance.unitGoalKeeper.maxHP);
             ShootSystem.instance.goalKeeperHUD.SetHud(ShootSystem.instance.unitGoalKeeper);
        }
    }

    public static void SetGoalkeepersDamage(int id)
    {
        if (id == 0)
        {
            ShootSystem.instance.unitGoalKeeper.damageUpgrade = 1;
            PlayerPrefs.SetInt("GoalkeepersDamage", ShootSystem.instance.unitGoalKeeper.damageUpgrade);       
        }

        else if (id == 1)
        {
            ShootSystem.instance.unitGoalKeeper.damageUpgrade =
                PlayerPrefs.GetInt("GoalkeepersDamage") + 1;
            PlayerPrefs.SetInt("GoalkeepersDamage", ShootSystem.instance.unitGoalKeeper.damageUpgrade);
        }
    }

    public static void GameCurrencySave(int value)
    {
        PlayerPrefs.SetInt("currency", value);
    }
}