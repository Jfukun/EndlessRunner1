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
    }

    public static void Save(int highScore)
    {
        SaveData data = new SaveData
        {
            highScore = highScore,
            totalCoins = GameManager.Coins,
            totalGems = GameManager.Gems,
            lastPlayed = DateTime.Now.ToString("o")
        };

        File.WriteAllText(FilePath, JsonUtility.ToJson(data, prettyPrint: true));
        Debug.Log($"[SaveGameManager] Game saved to {FilePath}");
    }

    public static void Load()
    {
        if (!File.Exists(FilePath))
        {
            Debug.Log("[SaveGameManager] No save file found, starting fresh.");
            GameManager.Load(); 
            return;
        }

        string json = File.ReadAllText(FilePath);
        SaveData data = JsonUtility.FromJson<SaveData>(json);

        GameManager.Load();
        Debug.Log($"[SaveGameManager] Loaded — highscore: {data.highScore}, coins: {data.totalCoins}, gems: {data.totalGems}");
    }
}