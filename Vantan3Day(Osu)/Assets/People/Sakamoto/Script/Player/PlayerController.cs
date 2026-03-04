using UnityEngine;
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

    private InputBuffer _inputBuffer;
    private ArmMover _armMover;
    private DropAction _dropAction;

    public void Init(InputBuffer inputBuffer)
    {
        _inputBuffer = inputBuffer;
        _armMover = GetComponent<ArmMover>();
        _dropAction = GetComponent<DropAction>();
        _armMover.CurrentHand = _armMover.CurrentHand != null ? _armMover.CurrentHand : _rightHand;
        _armMover.CurrentArm = _armMover.CurrentArm != null ? _armMover.CurrentArm : _rightArm;
        _armMover.Init();
        _dropAction.Init(inputBuffer);
        RegistAction();
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

    // ドロップ処理：現在選択中の手にあるアイテムをドロップさせる
    public void DropFromActiveHand()
    {
        var hand = IsRightHand ? _rightHand : _leftHand;
        if (hand == null) return;

        var item = hand.GetComponentInChildren<Item>();
        if (item == null) return;

        StartCoroutine(item.Drop());
    }

    private void OnChange()
    {
        if (IsRightHand) IsRightHand = false;
        else IsRightHand = true;
    }
}
