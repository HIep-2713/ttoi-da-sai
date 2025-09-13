using UnityEngine;
using UnityEngine.SceneManagement;

public class ReviveButton : MonoBehaviour
{
    public int reviveCost = 50;

    public void OnReviveClick()
    {
        int totalCoins = PlayerPrefs.GetInt("Coins", 0);

        if (totalCoins >= reviveCost)
        {
            // Tr? coin
            totalCoins -= reviveCost;
            PlayerPrefs.SetInt("Coins", totalCoins);
            PlayerPrefs.Save();

            // Set c? revive
            GS.Instance.isReviving = true;

            // L?u l?i d? li?u ?i?m và coin hi?n t?i
            PlayerPrefs.SetInt("ReviveScore", GS.Instance.score);
            PlayerPrefs.SetInt("ReviveCoins", GS.Instance.coinsEarned);

            // Load l?i GameScene
            SceneManager.LoadScene("GameScene");
        }
        else
        {
            Debug.Log("Not enough coins!");
            SceneManager.LoadScene("GameOverScene");
        }
    }

    public void OnSkipClick()
    {
        SceneManager.LoadScene("GameOverScene");
    }
}
