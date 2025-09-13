using UnityEngine;
using UnityEngine.SceneManagement;

public class GS : MonoBehaviour
{
    public static GS Instance;

    [Header("UI Canvases (assign in Inspector)")]
    public GameObject canvasMenu;
    public GameObject canvasGamePlay;

    [Header("Spawning")]
    public EnemySpawner enemySpawner;

    [Header("Game Data")]
    public bool isPlaying = false;
    public int score = 0;
    public int coinsEarned = 0;
    [Header("Revive")]
    public bool isReviving = false;
    [Header("Player")]
    public GameObject playerPrefab;

    [Header("Runtime")]
    public GameObject player; 
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    public void PlayGame()
    {
        isPlaying = true;
        if (canvasMenu) canvasMenu.SetActive(false);
        if (canvasGamePlay) canvasGamePlay.SetActive(true);
        if (enemySpawner) enemySpawner.canSpawn = true;

        score = 0;
        coinsEarned = 0;
    }

    public void AddScore(int amount)
    {
        score += amount;
        if (UIGameplay.Instance != null)
            UIGameplay.Instance.UpdateScore(score);
    }

    public void AddCoin(int amount)
    {
        coinsEarned += amount;
        if (UIGameplay.Instance != null)
            UIGameplay.Instance.UpdateCoins(coinsEarned);
    }

    public void PlayerDie()
    {
        isPlaying = false;
        if (enemySpawner) enemySpawner.canSpawn = false;

        // l?u score cu?i cùng
        ScoreHolder.lastScore = score;

        // c?p nh?t best score
        int best = PlayerPrefs.GetInt("BestScore", 0);
        if (score > best)
        {
            PlayerPrefs.SetInt("BestScore", score);
            PlayerPrefs.Save();
        }

        // c?ng d?n coin
        int totalCoins = PlayerPrefs.GetInt("Coins", 0);
        totalCoins += coinsEarned;
        PlayerPrefs.SetInt("Coins", totalCoins);
        PlayerPrefs.Save();

        // chuy?n scene
        SceneManager.LoadScene("WaitScene");
    }

    public void BackToMenu()
    {
        isPlaying = false;
        if (canvasMenu) canvasMenu.SetActive(true);
        if (canvasGamePlay) canvasGamePlay.SetActive(false);
        if (enemySpawner) enemySpawner.canSpawn = false;
    }
}
