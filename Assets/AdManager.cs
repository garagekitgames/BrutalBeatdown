using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Monetization;
//using EasyMobile;
using UnityEngine.Events;
using SO;
using garagekitgames;
using GameFramework.GameStructure.Players.ObjectModel;
using GameFramework.GameStructure.Levels.ObjectModel;
using GameFramework.GameObjects.Components;
using GameFramework.GameStructure.Levels.Messages;
using GameFramework.Preferences;

public class AdManager : UnitySingletonPersistent<AdManager>
{
    private string gameId = "3021785";
    private bool testMode = false;


    public bool isInterstitialAdReady = false;

    public bool isRewardedAdReady = false;

    public string unityRewardedAdPlacementID = "rewardedVideo";
    public string unityInterstitialAdPlacementID = "vide0";

    public UnityEvent OnReviveAdSuccess;
    public UnityEvent OnReviveAdFailure;

    public UnityEvent OnDoubleCoinAdSuccess;
    public UnityEvent OnDoubleCoinAdFailure;

    public UnityEvent OnFreeCoinsAdSuccess;
    public UnityEvent OnFreeCoinsAdFailure;

    //public IntVariable cashCollected;
    //public IntVariable cashCollectedThisRound;


    //public IntVariable freeCoinRewardValue;

    //public BoolVariable removeAds;

    public int roundOverCount = 0;

    public string interstitialAdUnitId = "a3414493255e38d6";
    public string rewardedAdUnitId = "945b3d89f898f3e9";
    public string bannerAdUnitId = "9cf8d2c872101b4c"; // Retrieve the id from your account

    public bool canShowAd = false;
    public float timeBetweenAds = 60;
    public float timeElapsedTime;

    public int rewardCoinValue = 50;

    public bool paused = false;
    public float _prePauseTimeScale = 1;

    //public IntVariable currentLevel;

    public enum RewardType
    {
        Revive,
        DoubleCoins,
        FreeCash
    }

    public RewardType rewardType = RewardType.DoubleCoins;

    void OnEnable()
    {
        //Advertising.RewardedAdCompleted += RewardedAdCompletedHandler;
        //Advertising.RewardedAdSkipped += RewardedAdSkippedHandler;
    }

    // Unsubscribe events
    void OnDisable()
    {
        //Advertising.RewardedAdCompleted -= RewardedAdCompletedHandler;
        //Advertising.RewardedAdSkipped -= RewardedAdSkippedHandler;
    }

    // Event handler called when a rewarded ad has completed
    //void RewardedAdCompletedHandler(RewardedAdNetwork network, AdLocation location)
    //{
    //    switch (rewardType)
    //    {
    //        case RewardType.Revive:
    //            //rewardType = RewardType.Revive;
    //            OnReviveAdSuccess.Invoke();
    //            break;
    //        case RewardType.DoubleCoins:
    //            //rewardType = RewardType.DoubleCoins;
    //            OnDoubleCoinAdSuccess.Invoke();
    //            break;
    //        case RewardType.FreeCash:
    //            //rewardType = RewardType.FreeCash;
    //            OnFreeCoinsAdSuccess.Invoke();
    //            break;
    //        default:
    //            //rewardType = RewardType.FreeCash;
    //            OnFreeCoinsAdSuccess.Invoke();
    //            break;
    //    }



    //    Debug.Log("Rewarded ad has completed. The user should be rewarded now.");
    //}

    // Event handler called when a rewarded ad has been skipped
    //void RewardedAdSkippedHandler(RewardedAdNetwork network, AdLocation location)
    //{
    //    switch (rewardType)
    //    {
    //        case RewardType.Revive:
    //            //rewardType = RewardType.Revive;
    //            OnReviveAdFailure.Invoke();
    //            break;
    //        case RewardType.DoubleCoins:
    //            //rewardType = RewardType.DoubleCoins;
    //            OnDoubleCoinAdFailure.Invoke();
    //            break;
    //        case RewardType.FreeCash:
    //            //rewardType = RewardType.FreeCash;
    //            OnFreeCoinsAdFailure.Invoke();
    //            break;
    //        default:
    //            //rewardType = RewardType.FreeCash;
    //            OnFreeCoinsAdFailure.Invoke();
    //            break;
    //    }


    //    Debug.Log("Rewarded ad was skipped. The user should NOT be rewarded.");
    //}

    //public TextAlig
    public override void Awake()
    {
        MaxSdkCallbacks.OnSdkInitializedEvent += (MaxSdkBase.SdkConfiguration sdkConfiguration) => {
            // AppLovin SDK is initialized, start loading ads
            OnSdkInitializedEvent();
        };
        base.Awake();
        //if (!RuntimeManager.IsInitialized())
        //    RuntimeManager.Init();

        MaxSdk.SetSdkKey("LNJk6TgBB1FiupMvwC7k-HQ6T3nFx8ZqZmZ2ZIktyAck4TRCwHGpFPvB878VTT5hd-ipkL4yVQ4yZOCUe8zSaA");
        MaxSdk.InitializeSdk();

       

    }

    private void OnSdkInitializedEvent()
    {
        InitializeRewardedAds();
        InitializeBannerAds();
        InitializeInterstitialAds();
    }

    public void InitializeInterstitialAds()
    {
        // Attach callback
        MaxSdkCallbacks.OnInterstitialLoadedEvent += OnInterstitialLoadedEvent;
        MaxSdkCallbacks.OnInterstitialLoadFailedEvent += OnInterstitialFailedEvent;
        MaxSdkCallbacks.OnInterstitialAdFailedToDisplayEvent += InterstitialFailedToDisplayEvent;
        MaxSdkCallbacks.OnInterstitialHiddenEvent += OnInterstitialDismissedEvent;

        // Load the first interstitial
        LoadInterstitial();
    }

    private void LoadInterstitial()
    {
        // Debug.Log("LoadInterstitial");
        MaxSdk.LoadInterstitial(interstitialAdUnitId);
    }

    private void OnInterstitialLoadedEvent(string adUnitId)
    {
        //Debug.Log("InterstitialLoaded");
        // Interstitial ad is ready to be shown. MaxSdk.IsInterstitialReady(interstitialAdUnitId) will now return 'true'
    }

    private void OnInterstitialFailedEvent(string adUnitId, int errorCode)
    {
        //Debug.Log("InterstitialFailed");
        // Interstitial ad failed to load. We recommend re-trying in 3 seconds.
        Invoke("LoadInterstitial", 3);
    }

    private void InterstitialFailedToDisplayEvent(string adUnitId, int errorCode)
    {
        //Debug.Log("InterstitialFailedToDisplay");
        // Interstitial ad failed to display. We recommend loading the next ad
        LoadInterstitial();
    }

    private void OnInterstitialDismissedEvent(string adUnitId)
    {



        paused = false;
        Time.timeScale = _prePauseTimeScale;



        // Debug.Log("InterstitialDismissed");
        // Interstitial ad is hidden. Pre-load the next ad
        LoadInterstitial();
    }


    public void InitializeRewardedAds()
    {
        // Attach callback
        MaxSdkCallbacks.OnRewardedAdLoadedEvent += OnRewardedAdLoadedEvent;
        MaxSdkCallbacks.OnRewardedAdLoadFailedEvent += OnRewardedAdFailedEvent;
        MaxSdkCallbacks.OnRewardedAdFailedToDisplayEvent += OnRewardedAdFailedToDisplayEvent;
        MaxSdkCallbacks.OnRewardedAdDisplayedEvent += OnRewardedAdDisplayedEvent;
        MaxSdkCallbacks.OnRewardedAdClickedEvent += OnRewardedAdClickedEvent;
        MaxSdkCallbacks.OnRewardedAdHiddenEvent += OnRewardedAdDismissedEvent;
        MaxSdkCallbacks.OnRewardedAdReceivedRewardEvent += OnRewardedAdReceivedRewardEvent;

        // Load the first RewardedAd
        LoadRewardedAd();
    }


    private void LoadRewardedAd()
    {
        MaxSdk.LoadRewardedAd(rewardedAdUnitId);
        //Debug.Log("LoadRewardedAd");
    }

    private void OnRewardedAdLoadedEvent(string adUnitId)
    {
        // Rewarded ad is ready to be shown. MaxSdk.IsRewardedAdReady(rewardedAdUnitId) will now return 'true'
        // Debug.Log("RewardedAdLoaded");
    }

    private void OnRewardedAdFailedEvent(string adUnitId, int errorCode)
    {

        // Debug.Log("RewardedAdFailed");
        // Rewarded ad failed to load. We recommend re-trying in 3 seconds.
        Invoke("LoadRewardedAd", 3);
    }

    private void OnRewardedAdFailedToDisplayEvent(string adUnitId, int errorCode)
    {
        //switch (rewardType)
        //{
        //    case RewardType.Revive:
        //        //rewardType = RewardType.Revive;
        //        OnReviveAdFailure.Invoke();
        //        break;
        //    case RewardType.DoubleCoins:
        //        //rewardType = RewardType.DoubleCoins;
        //        OnDoubleCoinAdFailure.Invoke();
        //        break;
        //    case RewardType.FreeCash:
        //        //rewardType = RewardType.FreeCash;
        //        OnFreeCoinsAdFailure.Invoke();
        //        break;
        //    default:
        //        //rewardType = RewardType.FreeCash;
        //        OnFreeCoinsAdFailure.Invoke();
        //        break;
        //}

        //Debug.Log("Rewarded ad was skipped. The user should NOT be rewarded.");
        // Rewarded ad failed to display. We recommend loading the next ad
        LoadRewardedAd();
    }

    private void OnRewardedAdDisplayedEvent(string adUnitId) { }

    private void OnRewardedAdClickedEvent(string adUnitId) { }

    private void OnRewardedAdDismissedEvent(string adUnitId)
    {
        //switch (rewardType)
        //{
        //    case RewardType.Revive:
        //        //rewardType = RewardType.Revive;
        //        OnReviveAdSuccess.Invoke();
        //        break;
        //    //case RewardType.DoubleCoins:
        //    //    //rewardType = RewardType.DoubleCoins;
        //    //    OnDoubleCoinAdSuccess.Invoke();
        //    //    break;
        //    //case RewardType.FreeCash:
        //    //    //rewardType = RewardType.FreeCash;
        //    //    OnFreeCoinsAdSuccess.Invoke();
        //    //    break;
        //    //default:
        //    //    //rewardType = RewardType.FreeCash;
        //    //    OnFreeCoinsAdSuccess.Invoke();
        //    //    break;
        //}

        //switch (rewardType)
        //{
        //    case RewardType.Revive:
        //        //rewardType = RewardType.Revive;
        //        OnReviveAdFailure.Invoke();
        //        break;
        //    case RewardType.DoubleCoins:
        //        //rewardType = RewardType.DoubleCoins;
        //        OnDoubleCoinAdFailure.Invoke();
        //        break;
        //    case RewardType.FreeCash:
        //        //rewardType = RewardType.FreeCash;
        //        OnFreeCoinsAdFailure.Invoke();
        //        break;
        //    default:
        //        //rewardType = RewardType.FreeCash;
        //        OnFreeCoinsAdFailure.Invoke();
        //        break;
        //}

        // Debug.Log("Rewarded ad was skipped. The user should NOT be rewarded.");
        // Rewarded ad is hidden. Pre-load the next ad



        paused = false;
        Time.timeScale = _prePauseTimeScale;

        LoadRewardedAd();
    }

    private void OnRewardedAdReceivedRewardEvent(string adUnitId, MaxSdk.Reward reward)
    {
        // Rewarded ad was displayed and user should receive the reward
        switch (rewardType)
        {
            case RewardType.Revive:
                //rewardType = RewardType.Revive;
                OnReviveAdSuccess.Invoke();
                break;
            case RewardType.DoubleCoins:
                //rewardType = RewardType.DoubleCoins;
                OnDoubleCoinAdSuccess.Invoke();
                break;
            case RewardType.FreeCash:
                //rewardType = RewardType.FreeCash;
                OnFreeCoinsAdSuccess.Invoke();
                break;
            default:
                //rewardType = RewardType.FreeCash;
                OnFreeCoinsAdSuccess.Invoke();
                break;
        }

        // Debug.Log("Rewarded ad has completed. The user should be rewarded now.");
    }


    public void InitializeBannerAds()
    {
        // Banners are automatically sized to 320x50 on phones and 728x90 on tablets
        // You may use the utility method `MaxSdkUtils.isTablet()` to help with view sizing adjustments
        MaxSdk.CreateBanner(bannerAdUnitId, MaxSdkBase.BannerPosition.BottomCenter);

        // Set background or background color for banners to be fully functional
        MaxSdk.SetBannerBackgroundColor(bannerAdUnitId, Color.black);
    }
    // Start is called before the first frame update
    void Start()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            gameId = "3021784";
        }
        else if (Application.platform == RuntimePlatform.Android)
        {
            gameId = "3021785";
        }
        // Monetization.Initialize(gameId, testMode);



        InitializeRewardedAds();
        InitializeBannerAds();
        InitializeInterstitialAds();

        timeElapsedTime = timeBetweenAds;

        // MaxSdk.ShowBanner(bannerAdUnitId);
    }

    private void Update()
    {
        timeElapsedTime -= Time.deltaTime;

        //if (timeElapsedTime >= timeBetweenAds && canShowAd == false)
        //{
        //    canShowAd = true;
        //    timeElapsedTime = 0;
        //}
        //else
        //{
        //    timeElapsedTime += Time.deltaTime;
        //    //canShowAd = false;
        //}

        isInterstitialAdReady = MaxSdk.IsInterstitialReady(interstitialAdUnitId);



        if (MaxSdk.IsRewardedAdReady(rewardedAdUnitId))
        {
            isRewardedAdReady = true;
        }
        else
        {
            isRewardedAdReady = false;
        }

        if(paused)
        {
            AudioListener.pause = true;
        }
        else
        {
            AudioListener.pause = false;
        }
    }

    public void ShowIterstitialAd()
    {
        //if (removeAds.value)
        //    return;

        //if (false)//(Monetization.IsReady(unityInterstitialAdPlacementID))
        //{

        //    //StartCoroutine(ShowAd(unityInterstitialAdPlacementID, false));

        //}
        // Debug.Log("ShowIterstitialAd");

        if (MaxSdk.IsInterstitialReady(interstitialAdUnitId) && timeElapsedTime <= 0 && GameFramework.GameStructure.GameManager.Instance.Levels.Selected.Number > 5)
        {
            // Debug.Log("ShowIterstitialAd : Inside");

            MaxSdk.ShowInterstitial(interstitialAdUnitId);
            paused = true;
            _prePauseTimeScale = Time.timeScale;
            Time.timeScale = 0;
            timeElapsedTime = timeBetweenAds;

        }




    }

    public void ShowRewardedAd(int reward)
    {
        // Debug.Log("ShowRewardedAd");
        switch (reward)
        {
            case 0:
                rewardType = RewardType.Revive;
                break;
            case 1:
                rewardType = RewardType.DoubleCoins;
                break;
            case 2:
                rewardType = RewardType.FreeCash;
                break;
            default:
                rewardType = RewardType.FreeCash;
                break;
        }

        if (MaxSdk.IsRewardedAdReady(rewardedAdUnitId))
        {

            // Debug.Log("ShowRewardedAd : Inside");
            MaxSdk.ShowRewardedAd(rewardedAdUnitId);
            paused = true;
            _prePauseTimeScale = Time.timeScale;
            Time.timeScale = 0;
        }

        //if (false)//(Monetization.IsReady(unityRewardedAdPlacementID))
        //{


        //   // StartCoroutine(ShowAd(unityRewardedAdPlacementID, true));


        //}

        //else if(Advertising.IsRewardedAdReady())
        //{
        //    Advertising.ShowRewardedAd();
        //}




    }

    //public IEnumerator ShowAd(string placementID, bool reward)
    //{
    //    while(!Monetization.IsReady(placementID))
    //    {
    //        yield return new WaitForSeconds(0.25f);
    //    }
    //    ShowAdPlacementContent ad = null;
    //    ad = Monetization.GetPlacementContent(placementID) as ShowAdPlacementContent;

    //    if (ad != null)
    //    {
    //        if(reward)
    //        {
    //            ad.Show(AdCallback);
    //        }
    //        else
    //        {
    //            ad.Show();
    //        }

    //    }
    //}

    //public void AdCallback(ShowResult result)
    //{
    //    if(result == ShowResult.Finished)
    //    {
    //        switch (rewardType)
    //        {
    //            case RewardType.Revive:
    //                //rewardType = RewardType.Revive;
    //                OnReviveAdSuccess.Invoke();
    //                break;
    //            case RewardType.DoubleCoins:
    //                //rewardType = RewardType.DoubleCoins;
    //                OnDoubleCoinAdSuccess.Invoke();
    //                break;
    //            case RewardType.FreeCash:
    //                //rewardType = RewardType.FreeCash;
    //                OnFreeCoinsAdSuccess.Invoke();
    //                break;
    //            default:
    //                //rewardType = RewardType.FreeCash;
    //                OnFreeCoinsAdSuccess.Invoke();
    //                break;
    //        }

    //        Debug.Log("Rewarded ad has completed. The user should be rewarded now.");
    //    }

    //    else
    //    {
    //        switch (rewardType)
    //        {
    //            case RewardType.Revive:
    //                //rewardType = RewardType.Revive;
    //                OnReviveAdFailure.Invoke();
    //                break;
    //            case RewardType.DoubleCoins:
    //                //rewardType = RewardType.DoubleCoins;
    //                OnDoubleCoinAdFailure.Invoke();
    //                break;
    //            case RewardType.FreeCash:
    //                //rewardType = RewardType.FreeCash;
    //                OnFreeCoinsAdFailure.Invoke();
    //                break;
    //            default:
    //                //rewardType = RewardType.FreeCash;
    //                OnFreeCoinsAdFailure.Invoke();
    //                break;
    //        }

    //        Debug.Log("Rewarded ad was skipped. The user should NOT be rewarded.");
    //    }


    //}

    public void OnRoundOver()
    {
        roundOverCount++;
        if (roundOverCount > 2)
        {
            //ShowInterstitialAd();
            roundOverCount = 0;
        }
    }

    IEnumerator GiveCoinsReward(float sec)
    {
        //SetStopDoingShit(true);
        //StopGainingExp();
        // StartGainingExpAfterXSec(sec);
        yield return new WaitForSeconds(sec);
        //EffectsController.Instance.CreateCash(Vector3.up * 2 + Random.insideUnitSphere, currentLevel.value * 2);
        //AddCoins(rewardCoinValue);


        //Do Function here...
    }

    public void AddCoins(int value)
    {
        //cashCollected.Add(value);
        // PersistableSO.Instance.Save();
        GameFramework.GameStructure.GameManager.Instance.Player.AddCoins(value);

        //GameFramework.GameStructure.GameManager.Instance.Player.Coins += value;
        GameFramework.GameStructure.GameManager.Instance.Player.UpdatePlayerPrefs();
        PreferencesFactory.Save();

        //cashCollectedThisRound.Add(value);
    }


    public void OnGamePaused()
    {
        //ShowBannerAd();
    }

    public void OnGameUnPaused()
    {
        // Advertising.HideBannerAd();
    }

    public void ShowBannerAd()
    {
        //if (removeAds.value)
        //    return;

        

    }
    /* private IEnumerator ShowAd()
     {
         while(!Monetization.IsReady())
     }*/

}
