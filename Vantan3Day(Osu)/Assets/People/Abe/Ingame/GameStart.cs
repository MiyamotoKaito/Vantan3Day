using UnityEngine;
using DG.Tweening;
using TMPro;
public class GameStart : MonoBehaviour
{
    [SerializeField] private Transform ScreenPanel;
    [SerializeField] private Transform CameraPs;
    [SerializeField] private TMP_Text TitleMessage;
    // public void Start()
    // {
    //     StartGame();
    // }
    public void StartGame()
    {
        ScreenPanel.DOScale(10, 3f).SetEase(Ease.InOutQuad);
        //CameraPs.DOMove(new Vector3(0, 5, 0), 3f).SetEase(Ease.InOutQuad);
        TitleMessage.DOFade(1, 3f).SetEase(Ease.InOutQuad);
        SceneManager.Instance.OnSceneLoaded?.Invoke("MasterScene");
    }
}
