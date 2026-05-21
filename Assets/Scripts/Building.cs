using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;
using static SaveGameManager;

public class Building : MonoBehaviour
{
    public BuildingData m_BuildingData;
    public BuildingState m_BuildingState = BuildingState.InConstruction;
    private SpriteRenderer m_SpriteRenderer;
    public Sprite m_Andamios;

    public float m_ProductionTimer;
    public float m_ConstructionTimer;
    public float m_UpgradingTimer;

    public int m_StoredCash;
    public int m_StoredMaterials;

    public bool m_CallInitOnAwake = false;
    public enum BuildingState
    {
        InConstruction,
        InitialStage,
        Upgrading,
        FinalStage
    }
    private void Start()
    {
        if (m_CallInitOnAwake)
        {
            GridManager.m_Instance.AddExistingTile(transform.position, this);
            Init();
        }


    }
    public void Init(BuildingData data = null)
    {
        if(data) m_BuildingData = data;
        m_SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        UpdateVisuals();
        
    }

    private void UpdateVisuals()
    {
        switch(m_BuildingState)
        {
            case BuildingState.InConstruction:
                m_SpriteRenderer.sprite = m_Andamios;
                break;
            case BuildingState.InitialStage:
                m_SpriteRenderer.sprite = m_BuildingData.m_Building_InitialStage;
                break;
            case BuildingState.Upgrading:
                m_SpriteRenderer.sprite = m_Andamios;
                break;
            case BuildingState.FinalStage:
                m_SpriteRenderer.sprite = m_BuildingData.m_Building_FinalStage;
                break;
        }
    }

    private void Update()
    {
        switch (m_BuildingState)
        {
            case BuildingState.InConstruction:
                HandleConstruction();
                break;
            case BuildingState.InitialStage:
                HandleProduction();
                break;
            case BuildingState.Upgrading:
                HandleUpgrading();
                break;
            case BuildingState.FinalStage:
                m_SpriteRenderer.sprite = m_BuildingData.m_Building_FinalStage;
                break;
        }
    }

    public void TryUpgrade()
    {
        if(m_BuildingState == BuildingState.InitialStage)
        {
            (bool, bool) tryToSpendReuslt = GameManager.TryToSpend(m_BuildingData.m_BuildingUpgradeCashCost, m_BuildingData.m_BuildingUpgradeMaterialsCost, m_BuildingData.m_GemsToUpgradeInstantly);
        }
        else if(m_BuildingState == BuildingState.Upgrading)
        {

        }
    }

    private void HandleProduction()
    {
        if (m_StoredCash >= m_BuildingData.m_StorageCashCapacity && m_StoredMaterials >= m_BuildingData.m_StorageMaterialsCapacity) return;
        m_ProductionTimer += Time.deltaTime;
        if(m_ProductionTimer > m_BuildingData.m_ProductionTime)
        {
            //GameManager.AddResources(m_BuildingData.m_BuilidingCashRevenue, m_BuildingData.m_BuildingMaterialsRevenue);
            m_ProductionTimer = 0;
            m_StoredCash += m_BuildingData.m_BuildingCashRevenue;
            m_StoredMaterials += m_BuildingData.m_BuildingMaterialsRevenue;

        }
    }
    public bool TryCollect()
    {
        if(m_StoredCash > 0 || m_StoredMaterials > 0)
        {
            GameManager.AddResources(m_StoredCash, m_StoredMaterials);  
            m_StoredCash = 0;
            m_StoredMaterials = 0;
            // Add effect
            return true;
        }
        return false;
    }
    
    private void HandleConstruction()
    {
        m_ConstructionTimer += Time.deltaTime;
        if(m_ConstructionTimer > m_BuildingData.m_ProductionTime)
        {
            m_BuildingState = BuildingState.InitialStage;
            UpdateVisuals();
        }
    }

    private void HandleUpgrading()
    {
        m_UpgradingTimer += Time.deltaTime;
        if (m_UpgradingTimer > m_BuildingData.m_UpgradingTime)
        {
            m_BuildingState = BuildingState.FinalStage;
            UpdateVisuals();
        }
    }

    public void ApplyLoadedData(BuildingSaveData thisData, DateTime LastSave)
    {
        
        // Calculate new ammount while close
        float secondsSinceLastTime = (DateTime.Now - LastSave).Seconds;
        int generatedCash = (int)(secondsSinceLastTime / m_BuildingData.m_ProductionTime) * m_BuildingData.m_BuildingCashRevenue;
        int generatedMatarials = (int)(secondsSinceLastTime / m_BuildingData.m_ProductionTime) * m_BuildingData.m_BuildingMaterialsRevenue;
        
        m_StoredCash = thisData.storedCash + generatedCash;
        m_StoredMaterials = thisData.storedMaterials + generatedMatarials;

        if(m_StoredCash > m_BuildingData.m_StorageCashCapacity)
        {
            m_StoredCash = m_BuildingData.m_StorageCashCapacity;
        }
        if(m_StoredMaterials > m_BuildingData.m_StorageMaterialsCapacity)
        {
            m_StoredMaterials = m_BuildingData.m_StorageMaterialsCapacity;
        }
        Init();


    }

}
