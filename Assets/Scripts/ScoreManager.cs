using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private static ScoreManager m_Instance;
    private int m_Score = 0;
    private int m_RecordScore= 0;

    public TextMeshProUGUI m_CurrentScoreText;
    public TextMeshProUGUI m_RecordScoreText;

    private void Awake()
    {
        if (m_Instance == null)
        {
            m_Instance = this;
            m_RecordScore = PlayerPrefs.GetInt("RecordScore",0);
            m_RecordScoreText.text = "Record: "+  m_RecordScore.ToString();
            m_CurrentScoreText.text = "Curent Score: "+ m_Score.ToString(); 
        }

        else Destroy(gameObject);
    }

    public static void AddPoint()
    {
        m_Instance.m_Score++;
        m_Instance.m_CurrentScoreText.text= "Curent Score: " + m_Instance.m_Score.ToString();
    }

    public static void SaveScore()
    {
        if(m_Instance.m_Score > m_Instance.m_RecordScore)
        {
            m_Instance.m_RecordScore = m_Instance.m_Score;
            PlayerPrefs.SetInt("RecordScore", m_Instance.m_Score);
            PlayerPrefs.Save();
        }
        
    }
}
