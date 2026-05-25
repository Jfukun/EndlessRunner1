using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public TileManager m_TileManager;

    [Header("Game Over Screens")]
    public GameObject m_GameOverScreenWithReward;
    public GameObject m_GameOverScreenWithoutReward;

    [Header("HUD (Score + Record + CurrencyHUD)")]
    public GameObject m_HUD;

    [Header("Particle Effects")]
    public ParticleSystem m_HitParticle;
    public ParticleSystem m_CoinParticle;

    private AdsManager m_AdsManager;

    [Header("Revive")]
    public int m_ReviveGemCost = 5;

    private float m_DistanceTravelled = 0f;

    private void Start()
    {
        m_TileManager = FindAnyObjectByType<TileManager>();
        m_AdsManager = FindAnyObjectByType<AdsManager>();
    }

    private void Update()
    {
        if (m_TileManager == null || m_TileManager.m_IsGameOver) return;

        float delta = m_TileManager.m_TileSpeed * Time.deltaTime;
        m_DistanceTravelled += delta;
        ScoreManager.AddDistance(delta);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("obstacles"))
        {
            m_TileManager.m_IsGameOver = true;
            Handheld.Vibrate();
            SoundsManager.SoundHit();
            if (m_HitParticle != null)
            {
                m_HitParticle.gameObject.SetActive(true);
                m_HitParticle.Play();
            }

            int bonusCoins = Mathf.FloorToInt(m_DistanceTravelled / 10f);
            if (bonusCoins > 0) GameManager.AddCoins(bonusCoins);

            ScoreManager.SaveScore();
            NotificationsManager.Instance?.SendComeBackNotification();

            m_HUD.SetActive(false);

            bool adReady = m_AdsManager != null && m_AdsManager.m_RewardedAdInfo.isLoaded;
            m_GameOverScreenWithReward.SetActive(adReady);
            m_GameOverScreenWithoutReward.SetActive(!adReady);
        }

        bool isGem = other.CompareTag("Gems") ||
                     (other.transform.parent != null && other.transform.parent.CompareTag("Gems"));

        if (isGem)
        {
            other.gameObject.SetActive(false);
            ScoreManager.AddCoin();

            if (m_CoinParticle != null)
            {
                m_CoinParticle.transform.position = other.transform.position;
                m_CoinParticle.gameObject.SetActive(true);
                m_CoinParticle.Play();
            }
        }
    }

    public void OnReviveWithAd()
    {
        m_AdsManager.ShowRewardedAd();
    }

    public void OnReviveWithGems()
    {
        if (GameManager.TrySpendGems(m_ReviveGemCost))
        {
            Revive();
        }
        else
        {
            Debug.Log("Not enough gems to revive.");
        }
    }

    public void Revive()
    {
        m_TileManager.m_IsGameOver = false;
        m_TileManager.ResetDifficulty();
        ScoreManager.ResetCurrentScore();

        m_DistanceTravelled = 0f;

        m_GameOverScreenWithReward.SetActive(false);
        m_GameOverScreenWithoutReward.SetActive(false);
        m_HUD.SetActive(true);

        FindAnyObjectByType<CurrencyHUD>()?.ForceRefresh();
    }
}