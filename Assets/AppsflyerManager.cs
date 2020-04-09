using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppsflyerManager : MonoBehaviour
{
//    // Start is called before the first frame update
//    void Start()
//    {
//        /* Mandatory - set your AppsFlyer’s Developer key. */

//        /* For detailed logging */
//        /* AppsFlyer.setIsDebug (true); */
//        //#if UNITY_IOS
//        //  /* Mandatory - set your apple app ID
//        //   NOTE: You should enter the number only and not the "ID" prefix */
//        //        AppsFlyer.setAppsFlyerKey("zcKrZYJWnrWWctCxcLNnyT");
//        //  AppsFlyer.setAppID ("1491674233");
//        //  AppsFlyer.trackAppLaunch ();
//        //#elif UNITY_ANDROID
//        ///* Mandatory - set your Android package name */
//        AppsFlyer.setAppsFlyerKey("zcKrZYJWnrWWctCxcLNnyT");
//        AppsFlyer.setAppID ("com.garagekitgames.mrsneaky");
//        ///* For getting the conversion data in Android, you need to add the "AppsFlyerTrackerCallbacks" listener.*/
//        AppsFlyer.init ("zcKrZYJWnrWWctCxcLNnyT","AppsFlyerTrackerCallbacks");
////#endif


//    }

    void Start()
    {
        /* Mandatory - set your AppsFlyer’s Developer key. */
        AppsFlyer.setAppsFlyerKey("zcKrZYJWnrWWctCxcLNnyT");
        /* For detailed logging */
        /* AppsFlyer.setIsDebug (true); */
#if UNITY_IOS
        /* Mandatory - set your apple app ID
        NOTE: You should enter the number only and not the "ID" prefix */
        AppsFlyer.setAppID("1491674233");
        AppsFlyer.getConversionData();
        AppsFlyer.trackAppLaunch();
#elif UNITY_ANDROID
     /* For getting the conversion data in Android, you need to add the "AppsFlyerTrackerCallbacks" listener.*/
     AppsFlyer.init ("zcKrZYJWnrWWctCxcLNnyT","AppsFlyerTrackerCallbacks");
#endif
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
