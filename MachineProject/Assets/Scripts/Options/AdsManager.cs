using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Advertisements;
public class AdsManager : MonoBehaviour, IUnityAdsListener
{
    public Offline offline;
    public EventHandler<AdFinishEventArgs> OnAdDone;
    public string GameID
    {
        get
        {
            #if UNITY_ANDROID
                     return "4293189";
#elif UNITY_IOS
                    return "4293188";
#else
                    return "";
#endif
        }
    }

    public const string SampleInterstitialAd = "TestInterstitial";
    public const string SampleBannerAd = "TestBanner";
    public const string SampleRewardedAd = "TestRewarded";

    private void Awake()
    {
        Advertisement.Initialize(GameID, true);
        
    }
        
    public void ShowInterstitialAd()
    {
        if (!offline.hasInternet)
        {
            return;
        }
        if (Advertisement.IsReady(SampleInterstitialAd))
        {
            Advertisement.Show(SampleInterstitialAd);
        }
        else
        {
            Debug.Log("No Ads");
        }
    }

    IEnumerator ShowBannerAd_Routine()
    {
        while (!Advertisement.isInitialized)
        {
            yield return new WaitForSeconds(0.5f);
        }

        Advertisement.Banner.SetPosition(BannerPosition.TOP_CENTER);
        Advertisement.Banner.Show(SampleBannerAd);
    }

    public void ShowBannerAd()
    {
        if (!offline.hasInternet)
        {
            return;
        }
        StartCoroutine(ShowBannerAd_Routine());
    }

    public void HideBannerAd()
    {
        if (Advertisement.Banner.isLoaded)
        {
            Advertisement.Banner.Hide();
        }
    }

    private void Start()
    {
        Advertisement.AddListener(this);
    }
    public void OnUnityAdsReady(string placementId)
    {
        Debug.Log($"Done Loading{placementId}");
    }
    public void OnUnityAdsDidError(string message)
    {
        Debug.Log($"Ads Error: {message}");
    }
    public void OnUnityAdsDidStart(string placementId)
    {
        Debug.Log($"Ads Start {placementId}");
    }

    public void ShowRewardedAd()
    {
        if (!offline.hasInternet)
        {
            return;
        }
        if (Advertisement.IsReady(SampleRewardedAd))
        {
            Advertisement.Show(SampleRewardedAd);
        }
        else
        {
            Debug.Log("No Ads");
        }
    }
    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if(OnAdDone != null)
        {
            AdFinishEventArgs args = new AdFinishEventArgs(placementId, showResult);
            OnAdDone(this, args);
        }
    }
}
