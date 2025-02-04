﻿using Managers;
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

        public int firstHP;

        private void Start()
        {
            UnitControl();
            firstHP = maxHP;
            SetMaxDamage();
        }

        public bool TakeDamage(int dmg)
        {
            maxHP -= dmg;

            return maxHP <= 0;
        }

        private void UnitControl()
        {
            switch (gameObject.tag)
            {
                case "goalkeeper" when PlayerPrefs.GetInt("highlevel") == 1:
                    GameData.SetGoalkeepersHealth(0);
                    GameData.SetGoalkeepersDamage(0);
                    break;
                case "goalkeeper" when PlayerPrefs.GetInt("highlevel") > 1:
                    if (LevelManager.Main.levelRestarted)
                    {
                        GameData.SetGoalkeepersHealth(2);
                        GameData.SetGoalkeepersDamage(2);
                    }
                    else if(LevelManager.Main.nextLevelTrigger)
                    {
                        GameData.SetGoalkeepersHealth(1);
                        GameData.SetGoalkeepersDamage(1);
                    }
                    else if(!LevelManager.Main.nextLevelTrigger)
                    {
                        GameData.SetGoalkeepersHealth(2);
                        GameData.SetGoalkeepersDamage(2);
                    }
                    break;
                case "Player" when PlayerPrefs.GetInt("HEALTH") == 0:
                    GameData.SetPlayersHealth(0);
                    break;
                case "Player" when PlayerPrefs.GetInt("HEALTH") > 0:
                    maxHP = PlayerPrefs.GetInt("PlayerMaxHP");
                    GameData.SetPlayersHealth(2);

                    break;
            }

            if (gameObject.CompareTag("Player") && PlayerPrefs.GetInt("POWER") == 0)
                GameData.SetPlayersDamage(0);
            else if (gameObject.CompareTag("Player") && PlayerPrefs.GetInt("POWER") > 0)
                GameData.SetPlayersDamage(2);
        }

        public static void SetMaxDamage() => GameManager.main.maxDamageText.text = (40 + (40 * 8) / 100 * ShootSystem.instance.unitPlayer.damageUpgrade).ToString();
        public static void SetDamageAfterTurn() => GameManager.main.maxDamageText.text = ((int) GameManager.main.ballAttackValue).ToString();
    }
