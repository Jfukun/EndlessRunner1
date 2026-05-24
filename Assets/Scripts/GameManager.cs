using UnityEngine;

public static class GameManager
{
    public static int Coins { get; private set; }
    public static int Gems { get; private set; }

    // Called by SaveGameManager.Load() to restore persisted values
    public static void SetCoins(int amount) { Coins = amount; }
    public static void SetGems(int amount) { Gems = amount; }

    public static void AddCoins(int amount) { Coins += amount; }
    public static void AddGems(int amount) { Gems += amount; }

    public static bool TrySpendCoins(int amount)
    {
        if (Coins < amount) return false;
        Coins -= amount;
        return true;
    }

    public static bool TrySpendGems(int amount)
    {
        if (Gems < amount) return false;
        Gems -= amount;
        return true;
    }
}