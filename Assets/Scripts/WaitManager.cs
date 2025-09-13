using UnityEngine;
using UnityEngine.SceneManagement;

public class WaitManager : MonoBehaviour
{
    public float waitTime = 2f;

    void Start()
    {
        // Sau waitTime gi�y s? chuy?n t?i GameOverScene
        Invoke(nameof(GoToGameOver), waitTime);
    }

    public void GoToGameOver()
    {
        SceneManager.LoadScene("GameOverScene");
    }

    // B?m n�t Skip (n?u b?n th�m n�t) g?i h�m n�y
    public void SkipToGameOver()
    {
        CancelInvoke();
        GoToGameOver();
    }
}
