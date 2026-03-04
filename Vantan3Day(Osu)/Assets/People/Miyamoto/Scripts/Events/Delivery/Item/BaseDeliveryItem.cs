using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseDeliveryItem : MonoBehaviour, IDelivery, IPointerClickHandler
{
    public event Action OnClicked;
    [Header("アニメーションのスピード関連")]
    [SerializeField]
    protected float _returnDuration;
    [SerializeField]
    protected float _moveSpeed;
    [SerializeField]
    protected float _toReturnTime;
    protected Animator _animator;
    private Vector3 _returnPos;
    private float _timer;
    private bool _isReturn;

    public abstract void GetEffect();

    public void OnPointerClick(PointerEventData eventData)
    {
        GetEffect();
        _animator.SetTrigger("Open");
        Return();
    }
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > _toReturnTime && !_isReturn)
        {
            Return();
            _isReturn = true;
        }
    }
    /// <summary>
    /// 帰る処理
    /// </summary>
    private void Return()
    {
        if (_isReturn) return;

        _isReturn = true;
        OnClicked?.Invoke();
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DORotate(transform.rotation.y == 0 ?
            new Vector3(0, 180, 0) : new Vector3(0, 0, 0), _returnDuration, RotateMode.Fast))
            .Append(transform.DOMove(_returnPos, _moveSpeed))
            .OnComplete(() => Destroy(gameObject));
    }
    /// <summary>
    /// 変える場所を設定
    /// 外側から受け取る
    /// </summary>
    /// <param name="returnPos"></param>
    public void GetReturnPos(Vector3 returnPos) => _returnPos = returnPos;
}
