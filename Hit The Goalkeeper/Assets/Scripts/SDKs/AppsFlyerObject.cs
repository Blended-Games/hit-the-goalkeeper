using UnityEngine;

public class AppsFlyerObject : MonoBehaviour {
    private void Start () {
        /* Mandatory - set your AppsFlyer’s Developer key. */
        AppsFlyer.setAppsFlyerKey ("BDt9xS3kmRET5qbDQgEMYk");
        /* For detailed logging */
        /* AppsFlyer.setIsDebug (true); */
#if UNITY_IOS
        /* Mandatory - set your apple app ID
        NOTE: You should enter the number only and not the "ID" prefix */
        AppsFlyer.setAppID ("1506836395");
        AppsFlyer.getConversionData();
        AppsFlyer.trackAppLaunch ();
#elif UNITY_ANDROID
     /* For getting the conversion data in Android, you need to add the "AppsFlyerTrackerCallbacks" listener.*/
     AppsFlyer.init ("YOUR_APPSFLYER_DEV_KEY","AppsFlyerTrackerCallbacks");
#endif
    }
}