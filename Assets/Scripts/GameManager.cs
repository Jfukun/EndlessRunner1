using UnityEngine;

public static class GameManager
{
    private const string KEY_COINS = "Coins";
    private const string KEY_GEMS = "Gems";

    public static int Coins { get; private set; }
    public static int Gems { get; private set; }

    public static void Load()
    {
        Coins = PlayerPrefs.GetInt(KEY_COINS, 0);
        Gems = PlayerPrefs.GetInt(KEY_GEMS, 0);
    }

    public static void Save()
    {
        PlayerPrefs.SetInt(KEY_COINS, Coins);
        PlayerPrefs.SetInt(KEY_GEMS, Gems);
        PlayerPrefs.Save();
    }

    public static void AddCoins(int amount)
    {
        Coins += amount;
        Save();
    }

    public static void AddGems(int amount)
    {
        Gems += amount;
        Save();
    }

    // Returns true and deducts if the player can afford it
    public static bool TrySpendCoins(int amount)
    {
        if (Coins < amount) return false;
        Coins -= amount;
        Save();
        return true;
    }

    public static bool TrySpendGems(int amount)
    {
        if (Gems < amount) return false;
        Gems -= amount;
        Save();
        return true;
    }
}