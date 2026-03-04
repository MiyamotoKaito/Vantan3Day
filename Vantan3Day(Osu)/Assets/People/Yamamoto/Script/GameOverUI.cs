using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    public void Retry()
    {
        var name = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        SceneManager.Instance.OnSceneLoaded?.Invoke(name);
    }

    public void Title()
    {
        SceneManager.Instance.OnSceneLoaded?.Invoke("Title");
    }
}
