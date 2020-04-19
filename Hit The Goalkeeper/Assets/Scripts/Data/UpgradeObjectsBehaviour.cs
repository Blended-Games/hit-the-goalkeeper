using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Data
{
    public class UpgradeObjectsBehaviour : MonoBehaviour
    {
        #region Singleton

        public static UpgradeObjectsBehaviour main;

        private void Awake()
        {
            main = this;
        }

        #endregion

        [SerializeField]
        private string
            upgradeName; //If this is health then set this name to healthUpgrade, else set it to damageUpgrade.

        public int upgradeCost;
        [SerializeField] private TextMeshProUGUI coinAmountText;

        private void Start()
        {
            if (!GameData.UpgradeCurrencyControl(upgradeName))
                upgradeCost = 50;
            else if (GameData.UpgradeCurrencyControl(upgradeName))
            {
                upgradeCost = 50 * (PlayerPrefs.GetInt(upgradeName) + 1);
            }

            coinAmountText.text = upgradeCost.ToString();
        }

        public void Upgrade()
        {
            GameData.BuyUpgrade(upgradeName, upgradeCost);
        }

        public void IncreaseCurrency()
        {
            upgradeCost = 50 * (PlayerPrefs.GetInt(upgradeName) + 1);
            coinAmountText.text = upgradeCost.ToString();
        }
    }
}