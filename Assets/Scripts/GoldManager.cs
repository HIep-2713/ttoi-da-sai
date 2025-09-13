using UnityEngine;

public class GoldManager : MonoBehaviour
{
    public static GoldManager Instance;

    private int currentGold = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddGold(int amount)
    {
        currentGold += amount;
    }

    public void SaveGold()
    {
        int totalCoins = PlayerPrefs.GetInt("Coins", 0);
        totalCoins += currentGold;
        PlayerPrefs.SetInt("Coins", totalCoins);
        PlayerPrefs.Save();

        currentGold = 0; // reset sau khi l?u
    }

    public int GetGold() => currentGold;
}
