using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private static ScoreManager m_Instance;
    private float m_Score = 0f;
    private int m_RecordScore = 0;

    public TextMeshProUGUI m_CurrentScoreText;
    public TextMeshProUGUI m_RecordScoreText;

    [Header("Score Settings")]
    [Tooltip("Adjust how fast the score increases. 0.5 = half speed, 2 = double speed.")]
    public float m_ScoreMultiplier = 0.5f;

    [Tooltip("Bonus points added when collecting a coin.")]
    public float m_CoinBonus = 10f;

    private void Awake()
    {
        if (m_Instance == null)
        {
            m_Instance = this;
            m_RecordScore = SaveGameManager.Load();
            m_RecordScoreText.text = "Record: " + m_RecordScore;
            m_CurrentScoreText.text = "Score: 0";
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void AddDistance(float delta)
    {
        m_Instance.m_Score += delta * m_Instance.m_ScoreMultiplier;
        m_Instance.m_CurrentScoreText.text = "Score: " + Mathf.FloorToInt(m_Instance.m_Score);
    }

    public static void AddCoin()
    {
        m_Instance.m_Score += m_Instance.m_CoinBonus;
        m_Instance.m_CurrentScoreText.text = "Score: " + Mathf.FloorToInt(m_Instance.m_Score);
        SoundsManager.SoundCoin();
        GameManager.AddCoins(1);
    }

    public static void SaveScore()
    {
        int finalScore = Mathf.FloorToInt(m_Instance.m_Score);
        if (finalScore > m_Instance.m_RecordScore)
        {
            m_Instance.m_RecordScore = finalScore;
            m_Instance.m_RecordScoreText.text = "Record: " + m_Instance.m_RecordScore;
        }
        SaveGameManager.Save(m_Instance.m_RecordScore);
    }

    public static int GetHighScore() => m_Instance.m_RecordScore;

    public static void ResetCurrentScore()
    {
        m_Instance.m_Score = 0f;
        m_Instance.m_CurrentScoreText.text = "Score: 0";
    }
}