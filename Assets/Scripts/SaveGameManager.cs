using System;
using System.IO;
using UnityEngine;

public static class SaveGameManager
{
    private static string FilePath => Path.Combine(Application.persistentDataPath, "runner_save.json");

    [Serializable]
    private class SaveData
    {
        public int highScore;
        public int totalCoins;
        public int totalGems;
        public string lastPlayed;

        // Shop data
        public bool[] unlockedSkins;
        public int equippedSkinIndex;
    }

    public static void Save()
    {
        Save(GameManager.HighScore);
    }

    public static void Save(int highScore)
    {
        SaveData data = new SaveData
        {
            highScore = highScore,
            totalCoins = GameManager.Coins,
            totalGems = GameManager.Gems,
            lastPlayed = DateTime.Now.ToString("o"),
            unlockedSkins = GameManager.UnlockedSkins,
            equippedSkinIndex = GameManager.EquippedSkinIndex
        };

        File.WriteAllText(FilePath, JsonUtility.ToJson(data, prettyPrint: true));
        Debug.Log($"[SaveGameManager] Saved — highscore: {highScore}, coins: {data.totalCoins}, gems: {data.totalGems}");
    }

    public static int Load()
    {
        // Always start currencies from zero
        int loadedHighScore = 0;

        if (!File.Exists(FilePath))
        {
            Debug.Log("[SaveGameManager] No save file found, starting fresh.");
            PlayerPrefs.DeleteKey("RecordScore");
            PlayerPrefs.Save();
            return 0;
        }

        string json = File.ReadAllText(FilePath);
        SaveData data = JsonUtility.FromJson<SaveData>(json);

        // Restore currencies into GameManager
        GameManager.SetCoins(data.totalCoins);
        GameManager.SetGems(data.totalGems);
        GameManager.HighScore = data.highScore;
        GameManager.UnlockedSkins = data.unlockedSkins;
        GameManager.EquippedSkinIndex = data.equippedSkinIndex;

        loadedHighScore = data.highScore;
        Debug.Log($"[SaveGameManager] Loaded — highscore: {data.highScore}, coins: {data.totalCoins}, gems: {data.totalGems}");

        return loadedHighScore;
    }

    public static void DeleteSave()
    {
        if (File.Exists(FilePath)) File.Delete(FilePath);
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        GameManager.SetCoins(0);
        GameManager.SetGems(0);
        GameManager.UnlockedSkins = null;
        GameManager.EquippedSkinIndex = 0;
        Debug.Log("[SaveGameManager] Save deleted.");
    }
}