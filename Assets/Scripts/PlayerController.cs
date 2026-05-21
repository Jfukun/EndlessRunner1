using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public TileManager m_TileManager;

    public GameObject m_GameOverScreenWithReward;
    public GameObject m_GameOverScreenWithoutReward;

    private void Start()
    {
        m_TileManager = FindAnyObjectByType<TileManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer==LayerMask.NameToLayer("obstacles"))
        {
            Debug.Log("Ouch");
            m_TileManager.m_IsGameOver = true;
            ScoreManager.SaveScore();
            AdsManager adsManager = FindFirstObjectByType<AdsManager>();
            if(adsManager.m_RewardedAdInfo.isLoaded)
            {
                m_GameOverScreenWithoutReward.SetActive(false);
                m_GameOverScreenWithReward.SetActive(true);
            }
            else
            {
                m_GameOverScreenWithoutReward.SetActive(true);
                m_GameOverScreenWithReward.SetActive(false);
            }




        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Coins"))
        {
            Debug.Log("Collected an item!");
            other.gameObject.SetActive(false);
            ScoreManager.AddPoint();
        }


    }
}
