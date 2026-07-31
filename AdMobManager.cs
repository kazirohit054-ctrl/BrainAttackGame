using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdMobManager : MonoBehaviour
{
    public static AdMobManager Instance { get; private set; }

    // REAL ADMOB IDs
    private string bannerAdUnitId = "ca-app-pub-7872555838408777/9860439454";
    private string interstitialAdUnitId = "ca-app-pub-7872555838408777/4373409131";
    private string rewardedAdUnitId = "ca-app-pub-7872555838408777/7901324671";

    private BannerView bannerView;
    private InterstitialAd interstitialAd;
    private RewardedAd rewardedAd;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        MobileAds.Initialize(initStatus =>
        {
            Debug.Log("✅ Google AdMob Initialized Successfully!");
            RequestBanner();
            LoadInterstitialAd();
            LoadRewardedAd();
        });
    }

    // --- BANNER AD ---
    public void RequestBanner()
    {
        if (bannerView != null) bannerView.Destroy();

        bannerView = new BannerView(bannerAdUnitId, AdSize.Banner, AdPosition.Bottom);
        AdRequest request = new AdRequest();
        bannerView.LoadAd(request);
    }

    // --- INTERSTITIAL AD ---
    public void LoadInterstitialAd()
    {
        if (interstitialAd != null)
        {
            interstitialAd.Destroy();
            interstitialAd = null;
        }

        AdRequest request = new AdRequest();
        InterstitialAd.Load(interstitialAdUnitId, request, (ad, error) =>
        {
            if (error != null || ad == null)
            {
                Debug.LogError("Interstitial Ad failed to load: " + error);
                return;
            }
            interstitialAd = ad;
        });
    }

    public void ShowInterstitialAd()
    {
        if (interstitialAd != null && interstitialAd.CanShowAd())
        {
            interstitialAd.Show();
            LoadInterstitialAd(); // Reload for next time
        }
        else
        {
            Debug.Log("Interstitial Ad not ready yet.");
        }
    }

    // --- REWARDED AD ---
    public void LoadRewardedAd()
    {
        if (rewardedAd != null)
        {
            rewardedAd.Destroy();
            rewardedAd = null;
        }

        AdRequest request = new AdRequest();
        RewardedAd.Load(rewardedAdUnitId, request, (ad, error) =>
        {
            if (error != null || ad == null)
            {
                Debug.LogError("Rewarded Ad failed to load: " + error);
                return;
            }
            rewardedAd = ad;
        });
    }

    public void ShowRewardedAd(Action onRewardEarned)
    {
        if (rewardedAd != null && rewardedAd.CanShowAd())
        {
            rewardedAd.Show(reward =>
            {
                Debug.Log("🎁 Player earned reward!");
                onRewardEarned?.Invoke();
            });
            LoadRewardedAd(); // Reload
        }
        else
        {
            Debug.Log("Rewarded Ad not ready.");
        }
    }
}