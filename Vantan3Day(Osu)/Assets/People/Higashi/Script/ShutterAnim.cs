using UnityEngine;
using DG.Tweening;
/// <summary>
/// シャッターにつける、アニメーション処理のスクリプト
/// </summary>
public class ShutterAnim : MonoBehaviour
{
    [SerializeField] private float _animTime = 1.0f;
    [SerializeField] private float _openedPosY = 6.1f;
    [SerializeField] private float _closedPosY = 0f;

    private bool _isOpened = false;
    private Transform _shutterPos;
    private void Start()
    {
        _shutterPos = GetComponent<Transform>();
    }
    public void ShutterOpenAnim()
    {
        if (!_isOpened)
        {
            _shutterPos.DOMoveY(_openedPosY, _animTime);
            _isOpened = true;
        }
    }
    public void ShutterCloseAnim()
    {
        if (_isOpened)
        {
            _shutterPos.DOMoveY(_closedPosY, _animTime).SetEase(Ease.OutBounce);
            _isOpened = false;
        }
    }
}
