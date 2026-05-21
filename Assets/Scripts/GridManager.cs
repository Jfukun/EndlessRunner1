using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public Grid m_Grid;
    public GameObject m_Prefab;
    public BuildingData m_SelectedBuilding;
    public BuildingButton m_SelectedBuildingButton;
    public static GridManager m_Instance;

    public Dictionary<Vector3Int,Building> m_OccupiedCells = new Dictionary<Vector3Int,Building>();

    private void Awake()
    {
        m_Instance = this;
    }
    private void Update()
    {
        if (m_SelectedBuilding)
        {
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(ray, out RaycastHit hitInfo))
                {
                    Vector3Int cellpos = m_Grid.WorldToCell(hitInfo.point);
                    if (m_OccupiedCells.ContainsKey(cellpos))
                    {
                        if (!m_OccupiedCells[cellpos].TryCollect())
                        {
                            m_OccupiedCells[cellpos].TryUpgrade();
                        }
                        return;
                      }
                    else if (m_SelectedBuilding)
                    {
                        (bool, bool) TrySpendResult = GameManager.TryToSpend(m_SelectedBuilding.m_BuildingCashCost, m_SelectedBuilding.m_BuildingMaterialsCost, m_SelectedBuilding.m_GemsToBuildInstantly);
                        if (TrySpendResult.Item1)
                        {
                            Building.BuildingState state = Building.BuildingState.InConstruction;
                            if (TrySpendResult.Item2)
                            {
                                state = Building.BuildingState.InitialStage;
                            }
                            PlaceNewBuilding(cellpos,m_SelectedBuilding,state);
                            m_SelectedBuilding = null;
                            m_SelectedBuildingButton.SetOutlineState(false);
                            m_SelectedBuildingButton = null;
                        }
                    }
                    
                       
                }
            }
        }
       
    }
    public Building PlaceNewBuilding(Vector3Int pos, BuildingData thisBuilding, Building.BuildingState state)
    {
        Vector3 actualPos = m_Grid.GetCellCenterWorld(pos);
        GameObject newCell =  GameObject.Instantiate(m_Prefab,actualPos, Quaternion.identity);
        newCell.transform.GetChild(0).transform.position += new Vector3(0, (pos.x + pos.y) * -0.01f, 0);
        Building newBuilding = newCell.GetComponent<Building>();
        newBuilding.m_BuildingState = state;
        newBuilding.Init(thisBuilding);
        AddNewTile(pos, newBuilding);
        return newBuilding;
    }

    

    public void RemoveAllBuildings()
    {
        foreach(KeyValuePair<Vector3Int,Building> cell in m_OccupiedCells)
        {
            Destroy(cell.Value.gameObject);
        }
        m_OccupiedCells.Clear();
    }

    public void AddNewTile(Vector3Int pos, Building thisBuilding)
    {
        m_OccupiedCells.Add(pos, thisBuilding);
    }
    public void AddExistingTile(Vector3 pos, Building thisBuilding)
    {
        Vector3Int intpos = m_Grid.WorldToCell(pos);
        m_OccupiedCells.Add(intpos, thisBuilding);
    }

    public void SetSelectedBuilding(BuildingButton button)
    {
        if (m_SelectedBuildingButton)
        {
            m_SelectedBuildingButton.SetOutlineState(false);
        }
        m_SelectedBuilding = button.m_BuildingData;
        m_SelectedBuildingButton = button;
        m_SelectedBuildingButton.SetOutlineState(true);
    }
}
