using UnityEngine;
using DG.Tweening;
using TMPro;
using System.Collections;

public class GameStart : MonoBehaviour
{
    [SerializeField] private Transform ScreenPanel;
    [SerializeField] private Transform CameraPs;
    [SerializeField] private TMP_Text TitleMessage;
    [SerializeField] private Shutter shutter;
    private float _timer = 0f;
    public void StartGame()
    {
        shutter.OpenShutter(true);
        ScreenPanel.DOScale(10, 3f).SetEase(Ease.InOutQuad);
        TitleMessage.DOFade(1, 3f).SetEase(Ease.InOutQuad);

        StartCoroutine(LoadAfterDelay());
    }

    private IEnumerator LoadAfterDelay()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.Instance.OnSceneLoaded?.Invoke("MasterScene");
    }

}
