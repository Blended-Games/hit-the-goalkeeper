using UnityEngine;
using FlurrySDK;

public class FlurryStart : MonoBehaviour
{
    public static FlurryStart main;
#if UNITY_ANDROID
    private readonly string FLURRY_API_KEY = FLURRY_ANDROID_API_KEY;
#elif UNITY_IPHONE
    private readonly string FLURRY_API_KEY = "CV8HDVNXWV7S63W8DZTK";
#else
    private readonly string FLURRY_API_KEY = null;
#endif

    private void Awake()
    {
        if (main != null && main != this)
        {
            Destroy(gameObject);
            return;
        }

        main = this;
    }

    private void Start()
    {
        // Initialize Flurry once.
        new Flurry.Builder()
            .WithCrashReporting(true)
            .WithLogEnabled(true)
            .WithLogLevel(Flurry.LogLevel.LogVERBOSE)
            .WithAppVersion("1.0")
            .Build(FLURRY_API_KEY);

        DontDestroyOnLoad(gameObject);
    }
}