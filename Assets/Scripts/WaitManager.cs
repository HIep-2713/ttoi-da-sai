using UnityEngine;
using UnityEngine.SceneManagement;

public class WaitManager : MonoBehaviour
{
    public float waitTime = 2f;

    void Start()
    {
        // Sau waitTime giây s? chuy?n t?i GameOverScene
        Invoke(nameof(GoToGameOver), waitTime);
    }

    public void GoToGameOver()
    {
        SceneManager.LoadScene("GameOverScene");
    }

    // B?m nút Skip (n?u b?n thêm nút) g?i hàm này
    public void SkipToGameOver()
    {
        CancelInvoke();
        GoToGameOver();
    }
}
