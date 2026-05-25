using UnityEngine;

public class MaterialChanger : MonoBehaviour
{
    public Material m_NewMaterial;

    public void ChangeChildrenMaterials()
    {
        Renderer[] allRenderers = GetComponentsInChildren<Renderer>();

        foreach (Renderer childRenderer in allRenderers)
        {
            if (childRenderer != null)
            {
                childRenderer.material = m_NewMaterial;
            }
        }
    }
}