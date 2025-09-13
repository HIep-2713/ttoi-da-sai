using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipToGameOver : MonoBehaviour
{
    public void SkipNow()
    {
        SceneManager.LoadScene("GameOverScene");
    }
}