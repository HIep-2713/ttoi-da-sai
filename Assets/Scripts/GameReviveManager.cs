using UnityEngine;

public class GameReviveManager : MonoBehaviour
{
    void Start()
    {
        if (GS.Instance == null)
        {
            Debug.LogError("[Revive] GS.Instance is null!");
            return;
        }

        if (GS.Instance.isReviving)
        {
            GS.Instance.isReviving = false;

            // L?y l?i ?i?m v� coin t? PlayerPrefs
            int oldScore = PlayerPrefs.GetInt("ReviveScore", 0);
            int oldCoins = PlayerPrefs.GetInt("ReviveCoins", 0);

            GS.Instance.score = oldScore;
            GS.Instance.coinsEarned = oldCoins;

            // C?p nh?t UI n?u c�
            if (UIGameplay.Instance != null)
            {
                UIGameplay.Instance.UpdateScore(oldScore);
                UIGameplay.Instance.UpdateCoins(oldCoins);
            }

            // B?t canvas gameplay v� t?t menu
            if (GS.Instance.canvasMenu != null)
                GS.Instance.canvasMenu.SetActive(false);

            if (GS.Instance.canvasGamePlay != null)
                GS.Instance.canvasGamePlay.SetActive(true);

            // Spawn l?i player t?i v? tr� revive
            if (GS.Instance.playerPrefab != null)
            {
                Vector3 revivePosition = Vector3.zero; // T�y ch?nh to? ?? revive
                GameObject player = Instantiate(GS.Instance.playerPrefab, revivePosition, Quaternion.identity);
                GS.Instance.player = player;
            }
            else
            {
                Debug.LogError("[Revive] playerPrefab is not assigned in GS!");
            }

            // Cho ph�p spawn enemy
            if (GS.Instance.enemySpawner != null)
                GS.Instance.enemySpawner.canSpawn = true;

            GS.Instance.isPlaying = true;

            // Xo� d? li?u revive
            PlayerPrefs.DeleteKey("ReviveScore");
            PlayerPrefs.DeleteKey("ReviveCoins");

            Debug.Log($"[Revive] Continue with score = {oldScore}, coins = {oldCoins}");
        }
        
    }
}
