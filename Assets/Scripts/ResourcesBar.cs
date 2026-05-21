using TMPro;
using UnityEngine;

public class ResourcesBar : MonoBehaviour
{
    public static ResourcesBar m_Instance;
    public TextMeshProUGUI m_CashText;
    public TextMeshProUGUI m_MaterialsText;
    public TextMeshProUGUI m_GemsText;

    private void Awake()
    {
        m_Instance = this;
    }

    private void Start()
    {
        UpdateValues();
    }

    public void UpdateValues()
    {
        m_CashText.text = GameManager.m_Cash.ToString();
        m_MaterialsText.text = GameManager.m_Materials.ToString();
        m_GemsText.text = GameManager.m_Gems.ToString();

    }


}
