using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;

public class AdMobManager : MonoBehaviour
{
    InterstitialAd InterstitialAdRef;
    BannerView BannerViewRef;
    RewardedAd RewardedAdRef;

    public string BannerAdID , InterstitialAdID, RewardedAdID;
    bool? IsInitialized;

    private void Awake()
    {
        InterstitialAdMob();
    }

    void InterstitialAdMob()
    {
        if (IsInitialized != null) return;
        MobileAds.Initialize(initStatus =>
        {
            IsInitialized = true;
            LoadBannerAd();
            LoadInterstitialAd();
            LoadRewaedAd();
        });
    }

    void LoadBannerAd()
    {
        if (BannerViewRef != null)
        {
            DestroyBannerView();
        }
        BannerViewRef = new BannerView(BannerAdID, AdSize.Banner, AdPosition.Bottom);
        AdRequest adRequest = new AdRequest();
        BannerViewRef.LoadAd(adRequest);
        BannerViewRef.Show();
    }

    void DestroyBannerView()
    {
        BannerViewRef.Hide();
        BannerViewRef.Destroy();
        BannerViewRef = null;
    }

    void LoadInterstitialAd()
    {
        if (InterstitialAdRef != null) { DestroyInterstitialAd(); }
        AdRequest adRequest = new AdRequest();

        InterstitialAd.Load(InterstitialAdID, adRequest, (ad, error) =>
        {
            if (error != null) { Debug.LogError(error); return; }
            InterstitialAdRef = ad;
        });
    }

     public void ShowInterstitialAd()
    {
        if (InterstitialAdRef != null && InterstitialAdRef.CanShowAd())
        {
            InterstitialAdRef.Show();
        }
        else
        {
            Debug.Log("InterstitialAd can not be shown.");
        }
    }

    void DestroyInterstitialAd()
    {
        InterstitialAdRef?.Destroy();
    }

    void LoadRewaedAd()
    {
        if (RewardedAdRef != null) { DestroyRewardedAd(); }
        AdRequest adRequest = new AdRequest();
        RewardedAd.Load(RewardedAdID, adRequest, (ad, error) =>
        {
            if (error == null)
            {
                RewardedAdRef = ad;
            }
            else
            {
                Debug.LogError(error);
                RewardedAdRef = null;
            }
        });
    }

    public void ShowRewaedAd()
    {
        if (RewardedAdRef != null && RewardedAdRef.CanShowAd())
        {
            RewardedAdRef.Show(rewaed =>
            {
                Debug.Log("Reward granted successfully");
            });
        }
    }

    void DestroyRewardedAd()
    {
        RewardedAdRef?.Destroy();
    }
}
