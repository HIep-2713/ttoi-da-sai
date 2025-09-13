using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    private int score = 0;

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

    public void AddScore(int amount)
    {
        score += amount;
    }

    public int GetScore() => score;

    public void ResetScore() => score = 0;

    public void SaveBestScore()
    {
        int best = PlayerPrefs.GetInt("BestScore", 0);
        if (score > best)
        {
            PlayerPrefs.SetInt("BestScore", score);
            PlayerPrefs.Save();
        }
    }

    public void GameOver()
    {
        // L?u score hi?n t?i vào ScoreHolder ?? UIResult hi?n th?
        ScoreHolder.lastScore = score;

        // C?p nh?t best score
        SaveBestScore();

        // Reset ?? l?n ch?i sau b?t ??u t? 0
        ResetScore();

        // Chuy?n scene k?t qu?
        SceneManager.LoadScene("WaitScene");
    }
}
