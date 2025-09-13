using UnityEngine;
using UnityEngine.UI;

public class UIGameplay : MonoBehaviour
{
    public static UIGameplay Instance;

    [Header("UI Texts")]
    public Text scoreText;
    public Text coinText;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void UpdateScore(int score)
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;
    }

    public void UpdateCoins(int coins)
    {
        if (coinText != null)
            coinText.text = "Coins: " + coins;
    }
}
