using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : GoalObject
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
    /// OkボタンNGボタンを押すイベント
    /// </summary>
    public event Action<ExaminationType> ButtonPressed;

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
    }

    public void InteractFromActiveHand()
    {
        var hand = IsRightHand ? _rightHand : _leftHand;
        if (hand == null) return;

        //手にあるアイテムを取得
        var item = hand.GetComponentInChildren<Item>();
        if (item == null)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
            if (hit.collider != null)
            {
                Debug.Log("クリックした: " + hit.collider.name);
                // 右手に当たったクリックは無視する
                var arm = hit.collider.GetComponentInParent<Arm>();
                if (arm != null && arm.IsRightArm)
                {
                }
                else if (hit.collider.TryGetComponent<IPointerClickHandler>(out var clickHandler))
                {
                    clickHandler.OnPointerClick(null);
                    foreach (var arms in _arms) arms.Attack();
                }
            }
        }
        else
        {
            //TODO 書類の上のみで反応するようにする
            // アイテムがある場合はインタラクト処理を呼び出す
            ButtonPressed?.Invoke(item.type);
            Debug.Log(item.type);
        }

        //itemにある処理を呼び出す（インタラクト）
    }

    private void OnChange()
    {
        if (IsRightHand) IsRightHand = false;
        else IsRightHand = true;
    }

    private void Update()
    {
        _pos = _armMover.PreviousHand.transform.position;
    }
}
