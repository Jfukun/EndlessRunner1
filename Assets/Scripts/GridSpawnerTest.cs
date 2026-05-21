using UnityEngine;

public class GridSpawnerTest : MonoBehaviour
{
    public Grid m_Grid;
    public GameObject m_Prefab;

    private void Update()
    {
       Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            if(Physics.Raycast(ray,out RaycastHit hitInfo))
            {
                Vector3Int cellpos = m_Grid.WorldToCell(hitInfo.point);
                Vector3 actualPos = m_Grid.GetCellCenterWorld(cellpos);
                GameObject newCell = GameObject.Instantiate(m_Prefab, actualPos,Quaternion.identity);
                newCell.transform.GetChild(0).transform.position += new Vector3(0, (cellpos.x + cellpos.y) * -0.01f, 0);
            }
        }
    }
}
