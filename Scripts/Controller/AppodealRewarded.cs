using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppodealAds.Unity.Common;
using AppodealAds.Unity.Api;

class AppodealRewarded : IRewardedVideoAdListener
{
    public Action Reward;
    public Action VideoClosed;

    private bool rewarded = false;
    public AppodealRewarded(Action rew, Action closed)
    {
        Appodeal.setRewardedVideoCallbacks(this);
        Reward = rew;

        VideoClosed = closed;
        rewarded = false;
    }

    public void onRewardedVideoLoaded() { rewarded = false; }
    public void onRewardedVideoFailedToLoad() { }
    public void onRewardedVideoShown()
    {
        //if (!rewarded)
        //{
        //    rewarded = true;
        //    Reward();
        //}
    }
    public void onRewardedVideoClosed() {  /*VideoClosed();*/ }
    public void onRewardedVideoFinished(int amount, string name)
    {
        if (!rewarded)
        {
            rewarded = true;
            Reward();
        }
    }

    public void onRewardedVideoClosed(bool finished)
    {
        VideoClosed();
    }

    void IRewardedVideoAdListener.onRewardedVideoLoaded(bool precache)
    {

    }

    void IRewardedVideoAdListener.onRewardedVideoFailedToLoad()
    {

    }

    void IRewardedVideoAdListener.onRewardedVideoShown()
    {

    }

    void IRewardedVideoAdListener.onRewardedVideoFinished(double amount, string name)
    {

    }

    void IRewardedVideoAdListener.onRewardedVideoClosed(bool finished)
    {

    }
}

class AppodealNonSkippable : INonSkippableVideoAdListener
{
    private Action Reward;
    private Action VideoClosed;

    private bool rewarded = false;
    public AppodealNonSkippable(Action rew, Action closed)
    {
        Appodeal.setNonSkippableVideoCallbacks(this);
        Reward = rew;

        VideoClosed = closed;
        rewarded = false;
    }

    public void onNonSkippableVideoLoaded() { rewarded = false; }
    public void onNonSkippableVideoFailedToLoad() {/* VideoClosed();*/ }
    public void onNonSkippableVideoShown()
    {
        //if (!rewarded)
        //{
        //    rewarded = true;
        //    Reward();
        //}
    }
    public void onNonSkippableVideoClosed() { }
    public void onNonSkippableVideoFinished()
    {
        if (!rewarded)
        {
            rewarded = true;
            Reward();
        }
    }

    public void onNonSkippableVideoClosed(bool finished)
    {
        VideoClosed();
    }

    public void onNonSkippableVideoLoaded(bool isPrecache)
    {

    }
}

