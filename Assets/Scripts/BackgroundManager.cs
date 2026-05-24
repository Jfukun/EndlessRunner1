using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    public GameObject m_BackgroundPrefab;

    public int m_NumberOfBackgrounds = 10;
    public float m_BackgroundLength = 30f;
    public float m_BackgroundSpeed = 5.0f;
    public float m_ZposToDissappear = -50f;

    public bool m_IsGameOver = false;

    private Transform[] m_Backgrounds;

    private void Start()
    {
        m_Backgrounds = new Transform[m_NumberOfBackgrounds];

        for (int i = 0; i < m_NumberOfBackgrounds; i++)
        {
            m_Backgrounds[i] = Instantiate(
                m_BackgroundPrefab,
                new Vector3(0, -15, i * m_BackgroundLength),
                Quaternion.AngleAxis(90, Vector3.up)
            ).transform;
        }
    }

    private void Update()
    {
        if (m_IsGameOver) return;

        for (int i = 0; i < m_Backgrounds.Length; i++)
        {
            m_Backgrounds[i].Translate(Vector3.right * m_BackgroundSpeed * Time.deltaTime);

            if (m_Backgrounds[i].position.z < m_ZposToDissappear)
            {
                float furthestZ = m_Backgrounds[0].position.z;

                for (int j = 1; j < m_Backgrounds.Length; j++)
                {
                    if (m_Backgrounds[j].position.z > furthestZ)
                    {
                        furthestZ = m_Backgrounds[j].position.z;
                    }
                }

                m_Backgrounds[i].position = new Vector3(0, -15, furthestZ + m_BackgroundLength);
            }
        }
    }
}