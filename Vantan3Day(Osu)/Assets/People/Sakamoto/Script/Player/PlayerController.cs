using System;
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

        //手にあるアイテムを取得
        var item = hand.GetComponentInChildren<Item>();
        if (item == null)
        {
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var hits = Physics2D.OverlapPointAll(mousePos);
            Collider2D best = null;
            int bestOrder = int.MinValue;
            float bestZ = float.MaxValue;

            if (hits != null)
            {
                for (int i = 0; i < hits.Length; i++)
                {
                    var h = hits[i];
                    if (h == null) continue;
                    var arm = h.GetComponentInParent<Arm>();
                    var isArm = arm != null;
                    Debug.Log($" hit[{i}] name={h.gameObject.name} isArm={isArm} layer={h.gameObject.layer}");
                    if (isArm) continue; // skip hand colliders

                    var sr = h.GetComponent<SpriteRenderer>();
                    int order = sr != null ? sr.sortingOrder : 0;
                    float z = h.transform.position.z;

                    // choose by sortingOrder then by smaller z
                    if (best == null || order > bestOrder || (order == bestOrder && z < bestZ))
                    {
                        best = h;
                        bestOrder = order;
                        bestZ = z;
                    }
                }
            }

            if (best != null)
            {
                Debug.Log($"InteractFromActiveHand: selected target={best.gameObject.name} order={bestOrder} z={bestZ}");
                if (best.TryGetComponent<IPointerClickHandler>(out var clickHandler))
                {
                    clickHandler.OnPointerClick(null);
                    foreach (var arms in _arms) arms.Attack();
                }
                else
                {
                    Debug.Log($"InteractFromActiveHand: target {best.gameObject.name} has no IPointerClickHandler");
                }
            }
            else
            {
                Debug.Log("InteractFromActiveHand: no non-hand target found");
            }
        }
        else
        {
           if(_arms[0].OnPapperArm || _arms[1].OnPapperArm)
            {
               ButtonPressed?.Invoke(item.type); 
            }
            
            
        }

        //itemにある処理を呼び出す（インタラクト）
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
