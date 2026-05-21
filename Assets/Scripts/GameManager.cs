using UnityEngine;

public static class GameManager 
{
    public static int m_Cash = 100;
    public static int m_Materials = 100;
    public static int m_Gems = 10;

    public static void AddResources(int cash, int materials, int gems = 0)
    {
        m_Cash += cash;
        m_Materials += materials;
        m_Gems += gems;
        ResourcesBar.m_Instance.UpdateValues();

       
    }

    public static (bool,bool) TryToSpend(int cash, int materials, int gems)
    {
        if (m_Gems >= gems)
        {
            m_Gems -= gems;
            ResourcesBar.m_Instance.UpdateValues();

            return (true,true); 
        }

        if(m_Cash >= cash && m_Materials >= materials)
        {
            m_Cash -= cash;
            m_Materials -= materials;
            ResourcesBar.m_Instance.UpdateValues();
            return (true,false);
        }

        return (false,false);
    }
}
