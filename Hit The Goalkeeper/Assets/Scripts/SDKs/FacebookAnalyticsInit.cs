using UnityEngine;
using Facebook.Unity;

public class FacebookAnalyticsInit : MonoBehaviour
{

    private void Awake()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            Application.targetFrameRate = 30;
        }
        FB.Init(FBInitCallback);
    }

    private void FBInitCallback()
    {
        if (FB.IsInitialized)
        {
            FB.ActivateApp();
        }
    }

    public void OnApplicationPause(bool paused)
    {
        if (!paused)
        {
            if (FB.IsInitialized)
            {
                FB.ActivateApp();
            }
        }
    }
}