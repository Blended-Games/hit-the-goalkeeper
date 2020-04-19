using TMPro;
using UnityEngine;

public class CoinText : MonoBehaviour
{
    #region Singleton

    private void Awake()
    {
        if (main != null && main != this)
        {
            Destroy(gameObject);
            return;
        }
        main = this;
    }

    #endregion

    private TextMeshProUGUI coinText;
    public static CoinText main;


    private void Start()
    {
        coinText = GetComponent<TextMeshProUGUI>();
        SetCoinText();
    }

    public void SetCoinText()
    {
        coinText.text = PlayerPrefs.GetInt("currency").ToString();
    }
}