using TMPro;
using UnityEngine;

    public class UpgradeObjectsBehaviour : MonoBehaviour
    {
        [SerializeField] private string
            upgradeName; //If this is health then set this name to healthUpgrade, else set it to damageUpgrade.

        public int upgradeCost;
        [SerializeField] private TextMeshProUGUI coinAmountText, upgradeLevelText;

        [SerializeField] private bool setCurrency;

        private void Start()
        {
            if (!GameData.UpgradeCurrencyControl(upgradeName))
            {
                PlayerPrefs.SetInt(upgradeName, 1);
                upgradeCost = 50;
            }
            else if (GameData.UpgradeCurrencyControl(upgradeName))
            {
                upgradeCost = 50 * (PlayerPrefs.GetInt(upgradeName));
            }
            
            upgradeLevelText.text = upgradeName.ToUpper() +"("+ PlayerPrefs.GetInt(upgradeName)+ ")";
            coinAmountText.text = upgradeCost.ToString();
        }

        private void Update()
        {
            if (setCurrency)
            {
                PlayerPrefs.SetInt("currency", PlayerPrefs.GetInt("currency") + 50);
                setCurrency = false;
                CoinText.main.SetCoinText();
            }
        }

        public void Upgrade()
        {
            GameData.BuyUpgrade(upgradeName, upgradeCost);
        }

        public void IncreaseCurrency(string key)
        {
            upgradeCost = 50 * (PlayerPrefs.GetInt(key) + 1);
            coinAmountText.text = upgradeCost.ToString();
            upgradeLevelText.text = upgradeName.ToUpper() +"("+ (PlayerPrefs.GetInt(key)+1)+ ")";
        }
    }
