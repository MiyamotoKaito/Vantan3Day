using UnityEngine;
using UnityEngine.InputSystem;

public class Interacter : MonoBehaviour
{
    private PlayerController _playerController;
    private InputBuffer _inputBuffer;

    public void Init(InputBuffer inputBuffer)
    {
        _playerController = GetComponent<PlayerController>();
        _inputBuffer = inputBuffer;
        RegistAction();
    }

    private void RegistAction()
    {
        _inputBuffer.InteractiveAction.started += Interact;
    }

    public void UnregistAction()
    {
        _inputBuffer.InteractiveAction.started -= Interact;
    }

    private void Interact(InputAction.CallbackContext context)
    {
        // PlayerController に処理を委譲して、現在アクティブな手からドロップさせる
        if (_playerController == null)
            _playerController = GetComponent<PlayerController>();

        if (_playerController.IsFlying) return; // 飛行中はドロップできない

        _playerController.InteractFromActiveHand();
    }
}
