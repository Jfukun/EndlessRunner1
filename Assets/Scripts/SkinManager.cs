using UnityEngine;

public class SkinManager : MonoBehaviour
{
    public Material[] m_AvailableSkins;
    void Start()
    {

        int equippedIndex = GameManager.EquippedSkinIndex;

        if (equippedIndex >= 0 && equippedIndex < m_AvailableSkins.Length)
        {
            MaterialChanger changer = GetComponent<MaterialChanger>();

            changer.m_NewMaterial = m_AvailableSkins[equippedIndex];

            changer.ChangeChildrenMaterials();

            Debug.Log($"Applied skin index {equippedIndex}");
        }
    }
}
