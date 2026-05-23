using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public TileManager m_TileManager;

    public GameObject m_GameOverScreenWithReward;
    public GameObject m_GameOverScreenWithoutReward;

    private AdsManager m_AdsManager;

    // Cost in gems to revive without watching an ad
    public int m_ReviveGemCost = 5;

    private void Start()
    {
        m_TileManager = FindAnyObjectByType<TileManager>();
        m_AdsManager = FindAnyObjectByType<AdsManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("obstacles"))
        {
            m_TileManager.m_IsGameOver = true;

            ScoreManager.SaveScore();

            // Notify the player to come back later
            NotificationsManager.Instance.SendComeBackNotification();

            // Show the right game over screen
            bool adReady = m_AdsManager != null && m_AdsManager.m_RewardedAdInfo.isLoaded;
            m_GameOverScreenWithReward.SetActive(adReady);
            m_GameOverScreenWithoutReward.SetActive(!adReady);
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Coins"))
        {
            other.gameObject.SetActive(false);
            ScoreManager.AddCoin();  
        }
    }

    public void OnReviveWithAd()
    {
        m_AdsManager.ShowRewardedAd();
        // AdsManager.OnUnityAdsShowComplete handles setting m_IsGameOver = false
        // and calling TileManager.ResetDifficulty()
    }

    public void OnReviveWithGems()
    {
        if (GameManager.TrySpendGems(m_ReviveGemCost))
        {
            Revive();
        }
        else
        {
            Debug.Log("[PlayerController] Not enough gems to revive.");
            // Optional: show a "not enough gems" popup here
        }
    }

    public void Revive()
    {
        m_TileManager.m_IsGameOver = false;
        m_TileManager.ResetDifficulty();
        ScoreManager.ResetCurrentScore();

        m_GameOverScreenWithReward.SetActive(false);
        m_GameOverScreenWithoutReward.SetActive(false);
    }
}