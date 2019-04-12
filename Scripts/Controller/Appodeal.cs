using System;
using System.Collections.Generic;
using UnityEngine;
using AppodealAds.Unity.Api;
using AppodealAds.Unity.Common;

class AppodealController : MonoBehaviour
{
    static AppodealController _instance;
    AppodealRewarded rewarded;
    //AppodealNonSkippable nonSkip;

    public static AppodealController instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<AppodealController>();
            return _instance;
        }
    }

    public static void Create()
    {
        var appodeal = new GameObject("AppodealController").AddComponent<AppodealController>();
        appodeal.Init();
        DontDestroyOnLoad(appodeal.gameObject);
    }

    public void Init()
    {
        //String appKey = "d89414fbc94e6934e8aa275049438d46f35763e4c9bda502";
        //Appodeal.disableLocationPermissionCheck();
        //Appodeal.confirm(Appodeal.SKIPPABLE_VIDEO);
        //Appodeal.setTesting(true);
        //Appodeal.initialize(appKey, Appodeal.BANNER_BOTTOM | Appodeal.INTERSTITIAL | Appodeal.SKIPPABLE_VIDEO | Appodeal.REWARDED_VIDEO);

        String appKey = "d89414fbc94e6934e8aa275049438d46f35763e4c9bda502";
        Appodeal.disableLocationPermissionCheck();
        //Appodeal.setTesting(true);
        Appodeal.initialize(appKey, Appodeal.INTERSTITIAL | Appodeal.BANNER_BOTTOM | Appodeal.BANNER | Appodeal.REWARDED_VIDEO);
    }

    public void DoNoRewardAction()
    {
        rewarded.VideoClosed();
    }

    public void InitRewardActions(Action rew, Action closed)
    {
        rewarded = new AppodealRewarded(rew, closed);
        //nonSkip = new AppodealNonSkippable(rew, closed);
    }

    public void ShowBanner()
    {
        if (Appodeal.isLoaded(Appodeal.BANNER))
            Appodeal.show(Appodeal.BANNER_BOTTOM);
    }

    public void CloseBanner()
    {
        Appodeal.hide(Appodeal.BANNER_BOTTOM);
    }

    float last_show = 0;

    public void ShowInterstitial()
    {
        if (Time.realtimeSinceStartup - last_show < 10)
            return;

        GameStatistics.instance.SendStat("require_interstitial", 0);

        last_show = Time.realtimeSinceStartup;

        if (Appodeal.isLoaded(Appodeal.INTERSTITIAL))
        {
            Appodeal.show(Appodeal.INTERSTITIAL);
        }  

        //else if(Appodeal.isLoaded(Appodeal.NON_SKIPPABLE_VIDEO))
        //{
        //    Appodeal.show(Appodeal.NON_SKIPPABLE_VIDEO); 
        //}
    }

    private bool isReward = true;
    public void showRewarded()
    {
        GameStatistics.instance.SendStat("require_rewarded", 0);

        if (Appodeal.isLoaded(Appodeal.REWARDED_VIDEO))
        {
            Appodeal.show(Appodeal.REWARDED_VIDEO);
            isReward = true;
        }
        //else if (Appodeal.isLoaded(Appodeal.NON_SKIPPABLE_VIDEO))
        //{
        //    Appodeal.show(Appodeal.NON_SKIPPABLE_VIDEO);
        //    isReward = false;
        //}
        else
        {
            rewarded.VideoClosed();
        }
    }

    

}

