using UnityEngine;
using DG.Tweening;
/// <summary>
/// シャッターのアニメーション処理のスクリプト
/// </summary>
public class ShutterAnim : MonoBehaviour
{
    [SerializeField] private float _animSpeed = 1.0f;
    [SerializeField] private float _openedPosY = 6.1f;
    [SerializeField] private float _closedPosY = 0f;

    private bool _isOpened = true;
    private Transform _shutterPos;
    private void Start()
    {
        _shutterPos = GetComponent<Transform>();
    }
    public void OpenAnim()
    {
        if (!_isOpened)
        {
            _shutterPos.DOMoveY(_openedPosY, _animSpeed);
            _isOpened = true;
        }
    }
    public void CloseAnim()
    {
        if (_isOpened)
        {
            _shutterPos.DOMoveY(_closedPosY, _animSpeed).SetEase(Ease.OutBounce);
            _isOpened = false;
        }
    }
}
