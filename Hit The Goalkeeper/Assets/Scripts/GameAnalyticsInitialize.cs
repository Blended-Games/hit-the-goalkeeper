using GameAnalyticsSDK;
using UnityEngine;

public class GameAnalyticsInitialize : MonoBehaviour
{
    public static GameAnalyticsInitialize main;

    private void Awake()
    {
        if (main != null && main != this)
        {
            Destroy(gameObject);
            return;
        }

        main = this;

        GameAnalytics.Initialize();
        DontDestroyOnLoad(gameObject);
    }
}