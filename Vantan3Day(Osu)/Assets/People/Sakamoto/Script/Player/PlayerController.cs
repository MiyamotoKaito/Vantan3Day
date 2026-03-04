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

    [SerializeField] private GameObject _rightHand;
    [SerializeField] private GameObject _rightArm;
    [SerializeField] private GameObject _leftHand;
    [SerializeField] private GameObject _leftArm;
    [SerializeField] private SpriteRenderer _currentRenderer;
    [SerializeField] private Sprite _idleRenderer;
    [SerializeField] private Sprite _pushRenderer;
    [SerializeField] private Arm[] arms;

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
        _armMover.CurrentArm = _armMover.CurrentArm != null ? _armMover.CurrentArm : _rightArm;
        _armMover.Init();
        _dropAction.Init(inputBuffer);
        _interacter.Init(inputBuffer);
        foreach (Arm arm in arms) arm.Init(this);
        RegistAction();
        IsRightHand = true;
        IsFlying = false;
    }

    private void OnDestroy()
    {
        _inputBuffer.ArmChangeAction.started -= ArmChange;
        _dropAction.UnRegistAction();
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
        _armMover.CurrentHand = _armMover.CurrentHand == _rightHand ? _leftHand : _rightHand;
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
                    Debug.Log("右手のクリックは無視します: " + hit.collider.name);
                }
                else if (hit.collider.TryGetComponent<IPointerClickHandler>(out var clickHandler))
                {
                    clickHandler.OnPointerClick(null);
                }
            }
        }

        //itemにある処理を呼び出す（インタラクト）
    }

    private void OnChange()
    {
        if (IsRightHand) IsRightHand = false;
        else IsRightHand = true;
    }
}
