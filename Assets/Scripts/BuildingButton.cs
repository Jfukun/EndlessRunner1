using UnityEngine;
using UnityEngine.UI;

public class BuildingButton : MonoBehaviour
{
    public BuildingData m_BuildingData;
    public Image m_BuildingImage;
    public Image m_OutlineImage;

    private bool m_IsSlected;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_BuildingImage.sprite = m_BuildingData.m_Building_FinalStage;
    }

    public void SelectBuilding()
    {
       GridManager.m_Instance.SetSelectedBuilding(this);
       SetOutlineState(true);
    }

    public void SetOutlineState(bool state)
    {
        m_OutlineImage.enabled = state;
    }
}
