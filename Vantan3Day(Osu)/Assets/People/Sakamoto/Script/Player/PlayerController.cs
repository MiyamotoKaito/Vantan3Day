using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject _rightHand;
    [SerializeField] private GameObject _rightArm;
    [SerializeField] private GameObject _leftHand;
    [SerializeField] private GameObject _leftArm;

    private InputBuffer _inputBuffer;
    private ArmMover _armMover;

    public void Init(InputBuffer inputBuffer)
    {
        _inputBuffer = inputBuffer;
        _armMover = GetComponent<ArmMover>();
        _armMover.CurrentHand = _armMover.CurrentHand != null ? _armMover.CurrentHand : _rightHand;
        _armMover.CurrentArm = _armMover.CurrentArm != null ? _armMover.CurrentArm : _rightArm;
        RegistAction();
    }

    private void OnDestroy()
    {
        _inputBuffer.ArmChangeAction.started -= ArmChange;
    }

    private void RegistAction()
    {
        _inputBuffer.ArmChangeAction.started += ArmChange;
    }

    private void ArmChange(InputAction.CallbackContext context)
    {
        if (_rightHand == null || _leftHand == null || _rightArm == null || _leftArm == null)
            return;
        _armMover.OnChange();
        _armMover.CurrentHand = _armMover.CurrentHand == _rightHand ? _leftHand : _rightHand;
        _armMover.CurrentArm = _armMover.CurrentArm == _rightArm ? _leftArm : _rightArm;
    }
}
