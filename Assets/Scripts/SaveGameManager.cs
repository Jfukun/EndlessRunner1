using NUnit.Framework;
using UnityEngine;

using System.Collections.Generic;
using System.IO;
using System.Collections;
using System;


public class SaveGameManager : MonoBehaviour
{
    public static SaveGameManager m_Instance;
    public string m_FileName = "saveData.json";
    private string m_FullSavePath;
    public List<BuildingData> m_AllavailableBuildings = new List<BuildingData>();

    public bool m_DoSave;



    private void Awake()
    {
        m_Instance = this;

        m_FullSavePath = Path.Combine(Application.persistentDataPath,m_FileName);
    }

    public IEnumerator Start()
    {
        yield return new WaitForSeconds(0.5f);
        Load();
    }

    
    

    // Update is called once per frame
    void Update()
    {
        if (m_DoSave)
        {
            m_DoSave = false;
            Save();
        }
    }

    public  void OnApplicationQuit()
    {
        Save();
    }

    public void Save()
    {
        SaveData newData = new SaveData();  
        newData.m_Materials = GameManager.m_Materials;
        newData.m_Cash = GameManager.m_Cash;
        newData.m_Gems = GameManager.m_Gems;
        newData.lastTime = System.DateTime.Now.ToString();

        foreach (var building in GridManager.m_Instance.m_OccupiedCells)
        {
            BuildingSaveData newBuildingSaveData = new BuildingSaveData();
            newBuildingSaveData.id = building.Value.m_BuildingData.m_BuildingID;
            newBuildingSaveData.pos = building.Key;
            newBuildingSaveData.buildingState = building.Value.m_BuildingState;
            newBuildingSaveData.storedCash = building.Value.m_StoredCash;
            newBuildingSaveData.storedMaterials = building.Value.m_StoredMaterials;
            newData.m_Buildings.Add(newBuildingSaveData); 
        }
        File.WriteAllText(m_FullSavePath,JsonUtility.ToJson(newData));

    }

    public void Load()
    {
        if (File.Exists(m_FullSavePath))
        {
            string json =  File.ReadAllText(m_FullSavePath);
            SaveData loadedData = JsonUtility.FromJson<SaveData>(json);
            GameManager.m_Cash = loadedData.m_Cash;
            GameManager.m_Materials = loadedData.m_Materials;
            GameManager.m_Gems = loadedData.m_Gems;
            GridManager.m_Instance.RemoveAllBuildings();
            foreach(var thisBuildingData in loadedData.m_Buildings)
            {
                BuildingData bd = m_AllavailableBuildings.Find(x => x.m_BuildingID == thisBuildingData.id);
                if (bd)
                {
                    Building newBuilding = GridManager.m_Instance.PlaceNewBuilding(thisBuildingData.pos, bd, thisBuildingData.buildingState);
                    newBuilding.ApplyLoadedData(thisBuildingData, DateTime.Parse(loadedData.lastTime));
                }
            }
            ResourcesBar.m_Instance.UpdateValues();
        }
    }

    [Serializable]
    public class SaveData
    {
        public int m_Cash;
        public int m_Materials;
        public int m_Gems;
        public string lastTime;
        public List<BuildingSaveData> m_Buildings =  new List <BuildingSaveData>();
    }

    [Serializable]

    public class BuildingSaveData
    {
        public string id;
        public Vector3Int pos;
        public Building.BuildingState buildingState;
        public int storedCash;
        public int storedMaterials;
    }
}

