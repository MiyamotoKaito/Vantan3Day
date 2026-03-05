using System;
using System.Linq;
using NUnit.Framework.Internal;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// 現在操作中の手が右手かどうか
    /// </summary>
    public bool IsRightHand { get; private set; } = true;

    /// <summary>
    ///  ハエが手にいる状態。
    /// </summary>
    public bool IsFlying { get; private set; } = false;

    /// <summary>
    /// OkボタンNGボタンを押すイベント。
    /// </summary>
    public event Action<ExaminationType> ButtonPressed;

    /// <summary>
    /// 荷物をすでに持っているかどうか。
    /// </summary>
    public bool IsPickUped => _isPickUped;

    [SerializeField] private GameObject _rightHand;
    [SerializeField] private GameObject _rightArm;
    [SerializeField] private GameObject _leftHand;
    [SerializeField] private GameObject _leftArm;
    [SerializeField] private SpriteRenderer _currentRenderer;
    [SerializeField] private Sprite _idleRenderer;
    [SerializeField] private Sprite _pushRenderer;
    [SerializeField] private Arm[] _arms;

    private InputBuffer _inputBuffer;
    private ArmMover _armMover;
    private DropAction _dropAction;
    private Interacter _interacter;
    private bool _isPickUped = false;

    public void Init(InputBuffer inputBuffer)
    {
        _inputBuffer = inputBuffer;
        _armMover = GetComponent<ArmMover>();
        _dropAction = GetComponent<DropAction>();
        _interacter = GetComponent<Interacter>();
        _armMover.CurrentHand = _armMover.CurrentHand != null ? _armMover.CurrentHand : _rightHand;
        _armMover.PreviousHand = _armMover.PreviousHand != null ? _armMover.PreviousHand : _leftHand;
        _armMover.CurrentArm = _armMover.CurrentArm != null ? _armMover.CurrentArm : _rightArm;
        _armMover.Init();
        _dropAction.Init(inputBuffer);
        _interacter.Init(inputBuffer);
        foreach (Arm arm in _arms) arm.Init(_armMover, this);
        RegistAction();
        IsRightHand = true;
        IsFlying = false;
        // 初期状態に合わせて各 Arm の IsActive を同期
        foreach (Arm arm in _arms) arm.SetActive(arm.IsRightArm == IsRightHand);
    }

    private void OnDestroy()
    {
        UnRestActions();
    }

    public void UnRestActions()
    {
        _inputBuffer.ArmChangeAction.started -= ArmChange;
        _dropAction.UnRegistAction();
        _interacter.UnregistAction();
    }

    private void RegistAction()
    {
        _inputBuffer.ArmChangeAction.started += ArmChange;
    }

    private void ArmChange(InputAction.CallbackContext context)
    {
        if (_rightHand == null || _leftHand == null || _rightArm == null || _leftArm == null)
            return;
        OnChange();
        // 切替後の現在操作手に合わせて各腕のアクティブ状態を更新
        foreach (Arm arm in _arms) arm.SetActive(arm.IsRightArm == IsRightHand);
        _armMover.CurrentHand = _armMover.CurrentHand == _rightHand ? _leftHand : _rightHand;
        _armMover.PreviousHand = _armMover.PreviousHand == _rightHand ? _leftHand : _rightHand;
        _armMover.CurrentArm = _armMover.CurrentArm == _rightArm ? _leftArm : _rightArm;
    }

    public void PickUp()
    {
        _currentRenderer.sprite = _pushRenderer;
        _isPickUped = true;
    }

    // ドロップ処理：現在選択中の手にあるアイテムをドロップさせる
    public void DropFromActiveHand()
    {
        var hand = IsRightHand ? _rightHand : _leftHand;
        if (hand == null) return;

        var item = hand.GetComponentInChildren<Item>();
        if (item == null) return;

        if (!IsRightHand)
            _currentRenderer.sprite = _idleRenderer;
        StartCoroutine(item.Drop());
        _isPickUped = false;
    }

    public void InteractFromActiveHand()
    {
        var hand = IsRightHand ? _rightHand : _leftHand;
        if (hand == null) return;

        var item = hand.GetComponentInChildren<Item>();

        if (item == null)
        {
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var hits = Physics2D.OverlapPointAll(mousePos);

            if (hits == null || hits.Length == 0) return;

            // sortingOrder順でソート（手前 → 奥）
            var sortedHits = hits
                .Where(h => h != null && h.GetComponentInParent<Arm>() == null)
                .OrderByDescending(h =>
                {
                    var sr = h.GetComponent<SpriteRenderer>();
                    return sr != null ? sr.sortingOrder : 0;
                })
                .ToArray();

            Cursor frontCursor = null;
            BaseDeliveryItem backItem = null;

            foreach (var hit in sortedHits)
            {
                // 一番手前のCursorを取得
                if (frontCursor == null && hit.TryGetComponent<Cursor>(out var cursor))
                {
                    frontCursor = cursor;
                    continue;
                }

                // Cursorより奥のDeliveryItemを取得
                if (frontCursor != null && hit.TryGetComponent<BaseDeliveryItem>(out var delivery))
                {
                    backItem = delivery;
                    break;
                }
            }

            if (frontCursor != null)
            {
                // ① 閉じていて開ける状態なら開く
                if (!frontCursor.IsOpen && frontCursor.CanOpen)
                {
                    frontCursor.OnPointerClick(null);
                    return;
                }

                // ② 開いている場合
                if (frontCursor.IsOpen)
                {
                    // 下にアイテムがあるなら発火
                    if (backItem != null)
                    {
                        backItem.OnPointerClick(null);

                        foreach (var arms in _arms)
                            arms.Attack();
                    }
                    else
                    {
                        // 下に何もないなら閉じる
                        frontCursor.OnPointerClick(null);
                    }

                    return;
                }
            }
            foreach (var hit in sortedHits)
            {
                if (hit.TryGetComponent<IPointerClickHandler>(out var click))
                {
                    click.OnPointerClick(null);
                    return;
                }
            }
        }
        else
        {
            if (item.ItemType != ItemType.None)
            {
                item.Excute();
                foreach (var arms in _arms)
                    arms.PlayItemUse();
            }
            else
            {
                if (_arms[0].OnPapperArm || _arms[1].OnPapperArm)
                    ButtonPressed?.Invoke(item.type);
            }
        }
    }

    private void OnChange()
    {
        if (IsRightHand) IsRightHand = false;
        else IsRightHand = true;
    }

    public void SetIsFly(bool isFlying)
    {
        IsFlying = isFlying;
    }
}
