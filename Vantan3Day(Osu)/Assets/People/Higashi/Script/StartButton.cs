using UnityEngine;
using Cysharp.Threading.Tasks;

public class StartButton : MonoBehaviour
{
    [SerializeField] private ShutterAnim _shutterAnim;
    [SerializeField, Tooltip("インゲームシーン名")] private string _inGameSceneName;

    public void StartPush()
    {
        StartPushAsync().Forget();
    }
    private async UniTask StartPushAsync()
    {
        _shutterAnim.ShutterOpenAnim();
        await UniTask.Delay(1000, cancellationToken: this.GetCancellationTokenOnDestroy()); //1秒
        SceneManager.Instance.OnSceneLoaded?.Invoke(_inGameSceneName);
        Debug.Log("インゲームスタート");
    }
}
