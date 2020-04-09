using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using garagekitgames;
using SO;
using Facebook.Unity;
using GameFramework.Debugging;
using GameFramework.GameObjects.Components;
using GameFramework.GameStructure.Levels.Messages;
using GameFramework.GameStructure.Levels.ObjectModel;
using GameFramework.UI.Dialogs.Components;

public class FacebookManager : UnitySingleton<FacebookManager>
{
    void Awake()
    {
        if (FB.IsInitialized)
        {
            FB.ActivateApp();
        }
        else
        {
            //Handle FB.Init
            FB.Init(() => {
                FB.ActivateApp();
            });
        }
    }

    // Unity will call OnApplicationPause(false) when an app is resumed
    // from the background
    void OnApplicationPause(bool pauseStatus)
    {
        // Check the pauseStatus to see if we are in the foreground
        // or background
        if (!pauseStatus)
        {
            //app resume
            if (FB.IsInitialized)
            {
                FB.ActivateApp();
            }
            else
            {
                //Handle FB.Init
                FB.Init(() => {
                    FB.ActivateApp();
                });
            }
        }
    }

    public void OnLevelClear()
    {
        LogAchieveLevelEvent(GameFramework.GameStructure.GameManager.Instance.Levels.Selected.Name);

        System.Collections.Generic.Dictionary<string, string> mapEvent = new
            System.Collections.Generic.Dictionary<string, string>();
        mapEvent.Add("af_timesPlayed", GameFramework.GameStructure.GameManager.Instance.Levels.Selected.StarsWon.ToString());

       // AppsFlyer.trackRichEvent(GameFramework.GameStructure.GameManager.Instance.Levels.Selected.Name, mapEvent);
    }

    public void LogAchieveLevelEvent(string level)
    {
        var parameters = new Dictionary<string, object>();
        parameters[AppEventParameterName.Level] = level;
        FB.LogAppEvent(
            AppEventName.AchievedLevel,null,
            parameters
        );
    }

    /**
 * Include the Facebook namespace via the following code:
 * using Facebook.Unity;
 *
 * For more details, please take a look at:
 * developers.facebook.com/docs/unity/reference/current/FB.LogAppEvent
 */
    public void LogLevel1Event(double valToSum)
    {
        FB.LogAppEvent(
            "Level1",
            (float)valToSum
        );
    }

    public void LogLevel1Event()
    {
        FB.LogAppEvent(
            "Level1"
        );
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
