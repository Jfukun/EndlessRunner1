using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private static ScoreManager m_Instance;
    private int m_Score = 0;
    private int m_RecordScore = 0;

    public TextMeshProUGUI m_CurrentScoreText;
    public TextMeshProUGUI m_RecordScoreText;

    private void Awake()
    {
        if (m_Instance == null)
        {
            m_Instance = this;

            SaveGameManager.Load();
            m_RecordScore = PlayerPrefs.GetInt("RecordScore", 0);

            m_RecordScoreText.text = "Record: " + m_RecordScore;
            m_CurrentScoreText.text = "Score: " + m_Score;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void AddPoint()
    {
        m_Instance.m_Score++;
        m_Instance.m_CurrentScoreText.text = "Score: " + m_Instance.m_Score;
    }

    public static void AddCoin()
    {
        m_Instance.m_Score++;
        m_Instance.m_CurrentScoreText.text = "Score: " + m_Instance.m_Score;
        GameManager.AddCoins(1);
    }

    public static void SaveScore()
    {
        if (m_Instance.m_Score > m_Instance.m_RecordScore)
        {
            m_Instance.m_RecordScore = m_Instance.m_Score;
            PlayerPrefs.SetInt("RecordScore", m_Instance.m_Score);
            PlayerPrefs.Save();
            m_Instance.m_RecordScoreText.text = "Record: " + m_Instance.m_RecordScore;
        }

        SaveGameManager.Save(m_Instance.m_RecordScore);
    }

    public static int GetHighScore() => m_Instance.m_RecordScore;

    public static void ResetCurrentScore()
    {
        m_Instance.m_Score = 0;
        m_Instance.m_CurrentScoreText.text = "Score: 0";
    }
}