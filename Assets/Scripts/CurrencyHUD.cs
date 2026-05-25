using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyHUD : MonoBehaviour
{
    [Header("HUD References")]
    public TextMeshProUGUI m_CoinsText;
    public TextMeshProUGUI m_GemsText;

    [Header("Currency Icons")]
    public Image m_CoinIcon;
    public Image m_GemIcon;

    public Sprite m_CoinSprite;
    public Sprite m_GemSprite;

    private int m_LastCoins = -1;
    private int m_LastGems = -1;

    private void Start()
    {
        if (m_CoinIcon != null)
            m_CoinIcon.sprite = m_CoinSprite;

        if (m_GemIcon != null)
            m_GemIcon.sprite = m_GemSprite;
    }

    private void Update()
    {
        int coins = GameManager.Coins;
        int gems = GameManager.Gems;

        if (coins != m_LastCoins)
        {
            m_LastCoins = coins;
            m_CoinsText.text = coins.ToString();
        }

        if (gems != m_LastGems)
        {
            m_LastGems = gems;
            m_GemsText.text = gems.ToString();
        }
    }

    public void ForceRefresh()
    {
        m_LastCoins = -1;
        m_LastGems = -1;
    }
}