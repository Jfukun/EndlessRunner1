using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject[] m_TilesPrefabs;

    public List<GameObject> m_ActiveTiles = new List<GameObject>();
    public List<GameObject> m_InactiveTiles = new List<GameObject>();

    public float m_TileLength = 30.0f;
    public int m_NumberOfTiles = 8;
    public int m_PoolSizePerPrefab = 4;

    [Header("Speed / Difficulty")]
    public float m_TileSpeed = 10.0f;
    public float m_MaxTileSpeed = 40.0f;
    public float m_SpeedIncreaseRate = 0.8f;   

    public float m_ZposToDissappear = -30.0f;

    public bool m_IsGameOver = false;

    private float m_InitialSpeed;
    private float m_ElapsedTime = 0f;

    private void Awake()
    {
        m_InitialSpeed = m_TileSpeed;

        for (int i = 0; i < m_NumberOfTiles; i++)
        {
            for (int j = 0; j < m_PoolSizePerPrefab; j++)
            {
                GameObject tile = Instantiate(m_TilesPrefabs[i]);
                tile.SetActive(false);
                m_InactiveTiles.Add(tile);
            }
        }

        for (int i = 0; i < m_NumberOfTiles; i++)
        {
            SpawnTile(i);
        }
    }

    void Start() { }

    void Update()
    {
        if (!m_IsGameOver)
        {
            m_ElapsedTime += Time.deltaTime;
            m_TileSpeed = Mathf.Min(
                m_InitialSpeed + m_SpeedIncreaseRate * m_ElapsedTime,
                m_MaxTileSpeed
            );

            for (int i = 0; i < m_ActiveTiles.Count; i++)
            {
                GameObject currentTile = m_ActiveTiles[i];
                currentTile.transform.Translate(Vector3.back * m_TileSpeed * Time.deltaTime);

                if (currentTile.transform.position.z < m_ZposToDissappear)
                {
                    currentTile.SetActive(false);
                    m_InactiveTiles.Add(currentTile);
                    m_ActiveTiles.Remove(currentTile);
                    SpawnTile(m_ActiveTiles.Count);
                }
            }
        }
    }

    public void ResetDifficulty()
    {
        m_ElapsedTime = 0f;
        m_TileSpeed = m_InitialSpeed;
    }

    private void SpawnTile(int index)
    {
        GameObject tileToUse = null;

        if (index <= 1)
        {
            tileToUse = m_InactiveTiles.Find(tile => tile.GetComponent<Tile>().m_IsSafeTile);
        }
        else
        {
            int tileIndex = Random.Range(0, m_InactiveTiles.Count);
            tileToUse = m_InactiveTiles[tileIndex];
        }

        tileToUse.transform.position = new Vector3(0, 0, index * m_TileLength);
        m_InactiveTiles.Remove(tileToUse);
        m_ActiveTiles.Add(tileToUse);
        tileToUse.SetActive(true);
    }
}