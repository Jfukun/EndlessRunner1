using UnityEngine;

[CreateAssetMenu(fileName = "New Building",menuName = "Buildings/Building Data")]
public class BuildingData : ScriptableObject
{
    [Header("Building Info")]
    public string m_BuildingID;
    public string m_BuilidingName;

    [Header("Visuals")]
    public Sprite m_Building_InitialStage;
    public Sprite m_Building_FinalStage;

    [Header("Economics")]
    public int m_BuildingCashCost;
    public int m_BuildingMaterialsCost;

    public int m_BuildingUpgradeCashCost;
    public int m_BuildingUpgradeMaterialsCost;

    public int m_BuildingCashRevenue;
    public int m_BuildingMaterialsRevenue;

    [Header("Time Settings(In seconds)")]
    public float m_ConstructionTime;
    public float m_ProductionTime;
    public float m_UpgradingTime;

    [Header("Gems features")]
    public int m_GemsToBuildInstantly;
    public int m_GemsToUpgradeInstantly;

    [Header("Storage setting")]
    public int m_StorageCashCapacity;
    public int m_StorageMaterialsCapacity;







}
