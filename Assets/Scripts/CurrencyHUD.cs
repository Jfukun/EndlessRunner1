using TMPro;
using UnityEngine;

public class CurrencyHUD : MonoBehaviour
{
    [Header("HUD References")]
    public TextMeshProUGUI m_CoinsText;
    public TextMeshProUGUI m_GemsText;

    [Header("Prefixes (feel free to use emoji or icons)")]
    public string m_CoinPrefix = "Coins: ";
    public string m_GemPrefix = "Gems: ";

    // Cache last values to avoid updating the text every frame unnecessarily
    private int m_LastCoins = -1;
    private int m_LastGems = -1;

    private void Update()
    {
        int coins = GameManager.Coins;
        int gems = GameManager.Gems;

        if (coins != m_LastCoins)
        {
            m_LastCoins = coins;
            m_CoinsText.text = m_CoinPrefix + coins;
        }

        if (gems != m_LastGems)
        {
            m_LastGems = gems;
            m_GemsText.text = m_GemPrefix + gems;
        }
    }

    // Call this if you want an instant refresh
    public void ForceRefresh()
    {
        m_LastCoins = -1;
        m_LastGems = -1;
    }
}
