using UnityEngine;

public static class DataManager
{
    private const string HIGH_SCORE_KEY = "HighScore";
    private const string COIN_KEY = "Coins";

    public static int HighScore
    {
        get => PlayerPrefs.GetInt(HIGH_SCORE_KEY, 0);
        set
        {
            PlayerPrefs.SetInt(HIGH_SCORE_KEY, value);
            PlayerPrefs.Save();
        }
    }

    public static int Coins
    {
        get => PlayerPrefs.GetInt(COIN_KEY, 0);
        set
        {
            PlayerPrefs.SetInt(COIN_KEY, value);
            PlayerPrefs.Save();
        }
    }

    public static void AddCoin(int amount) => Coins += amount;
}
