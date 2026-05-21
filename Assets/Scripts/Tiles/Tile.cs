using UnityEngine;

public class Tile : MonoBehaviour
{
    public GameObject[] gemContainers;
    public bool m_IsSafeTile = false;

    private void OnEnable()
    {
        foreach (GameObject container in gemContainers)
        {
            int i = Random.Range(0, 3);
            if (i > 0)
                container.SetActive(false);
            else
            {
                container.SetActive(true);
                foreach (Transform gem in container.transform)
                    gem.gameObject.SetActive(true);
            }
        }
    }
}
