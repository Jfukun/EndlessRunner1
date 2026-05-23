using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

public class AdsManager : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener, IUnityAdsInitializationListener
{
    public string m_AndroidGameID;
    bool m_TestMode = true;

    public class AdInfo
    {
        public string adID;
        public bool isLoaded;
        public float countdownToRetryLoad;

        public AdInfo(string adID)
        {
            this.adID = adID;
            isLoaded = false;
            countdownToRetryLoad = 3f;
        }
    }

    public AdInfo m_InterstitialAdInfo = new AdInfo("Interstitial_Android");
    public AdInfo m_BannerAdInfo = new AdInfo("Banner_Android");
    public AdInfo m_RewardedAdInfo = new AdInfo("Rewarded_Android");

    private System.Collections.Generic.Dictionary<string, AdInfo> m_AllAds
        = new System.Collections.Generic.Dictionary<string, AdInfo>();

    void Start()
    {
        Advertisement.Initialize(m_AndroidGameID, m_TestMode, this);
    }

    public void LoadAd(AdInfo thisAd)
    {
        m_AllAds.Add(thisAd.adID, thisAd);
        Advertisement.Load(thisAd.adID, this);
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        m_AllAds[placementId].isLoaded = true;
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        StartCoroutine(RetryLoad(placementId));
    }

    private IEnumerator RetryLoad(string placementId)
    {
        float countdown = m_AllAds[placementId].countdownToRetryLoad;
        while (countdown > 0f)
        {
            countdown -= Time.deltaTime;
            yield return null;
        }
        Advertisement.Load(placementId, this);
    }

    public void ShowInterstitialAd()
    {
        if (m_AllAds[m_InterstitialAdInfo.adID].isLoaded)
            Advertisement.Show(m_InterstitialAdInfo.adID, this);
        else
            SceneManager.LoadScene(0);
    }

    public void ShowRewardedAd()
    {
        if (m_AllAds[m_RewardedAdInfo.adID].isLoaded)
            Advertisement.Show(m_RewardedAdInfo.adID, this);
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        if (placementId == m_InterstitialAdInfo.adID)
        {
            SceneManager.LoadScene(0);
        }
        else if (placementId == m_RewardedAdInfo.adID)
        {
            if (showCompletionState == UnityAdsShowCompletionState.COMPLETED)
                FindFirstObjectByType<PlayerController>().Revive();
            else
                SceneManager.LoadScene(0);
        }
    }

    public void OnInitializationComplete()
    {
        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        LoadAd(m_InterstitialAdInfo);
        LoadAd(m_BannerAdInfo);
        LoadAd(m_RewardedAdInfo);
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.LogError($"[AdsManager] Initialization failed: {message}");
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        SceneManager.LoadScene(0);
    }

    public void OnUnityAdsShowStart(string placementId) { }
    public void OnUnityAdsShowClick(string placementId) { }
}