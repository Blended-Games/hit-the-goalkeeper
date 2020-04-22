using GUI2;
using UnityEngine;


    public static class GameData
    {
        public static bool UpgradeCurrencyControl(string key)
        {
            return
                PlayerPrefs.GetInt(key) >
                0 && PlayerPrefs.HasKey(key); //Controlling the level of this object. If its on level one we will set first costs, else cost will ben duplicated with the level.
        }

        private static bool CurrencyControl(int upgradeCost)
        {
            return PlayerPrefs.GetInt("currency") >= upgradeCost;
        }

        public static void BuyUpgrade(string key, int upgradeCost)
        {
            if (CurrencyControl(upgradeCost))
            {
                PlayerPrefs.SetInt("currency",
                    PlayerPrefs.GetInt("currency") - upgradeCost); //Decrease upgrade amount from the coin.
                CoinText.main.SetCoinText(); //Set new coin amount to coin text
                switch (key)
                {
                    case "POWER":
                        SetPlayersDamage(1);
                        Unit.SetMaxDamage();
                        UpgradePanel.main.damageUpgrade.IncreaseCurrency(key);
                        break;
                    case "HEALTH":
                        SetPlayersHealth(1);
                        UpgradePanel.main.healthUpgrade.IncreaseCurrency(key);
                        break;
                }

                PlayerPrefs.SetInt(key, PlayerPrefs.GetInt(key) + 1); //Increased the level of this upgrade
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
                PlayerPrefs.SetInt("GoalkeeperMaxHP", ShootSystem.instance.unitGoalKeeper.maxHP);
                ShootSystem.instance.goalKeeperHUD.SetHud(ShootSystem.instance.unitGoalKeeper);
            }

            else if (id == 1)
            {
                ShootSystem.instance.unitGoalKeeper.maxHP = PlayerPrefs.GetInt("GoalkeeperMaxHP") +
                                                            ((PlayerPrefs.GetInt("GoalkeeperMaxHP") * 10) / 100);
                PlayerPrefs.SetInt("GoalkeeperMaxHP", ShootSystem.instance.unitGoalKeeper.maxHP);
                ShootSystem.instance.goalKeeperHUD.SetHud(ShootSystem.instance.unitGoalKeeper);
            }
            else if (id == 2)
            {
                ShootSystem.instance.unitGoalKeeper.maxHP = PlayerPrefs.GetInt("GoalkeeperMaxHP");
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
            else if (id == 2)
            {
                ShootSystem.instance.unitGoalKeeper.damageUpgrade =
                    PlayerPrefs.GetInt("GoalkeepersDamage");
            }
        }

        public static void GameCurrencySave(int value)
        {
            PlayerPrefs.SetInt("currency", value);
        }
    }
