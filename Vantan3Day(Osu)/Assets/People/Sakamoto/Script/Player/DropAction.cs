using UnityEngine;
using UnityEngine.InputSystem;

public class DropAction : MonoBehaviour
{
    private InputBuffer _inputBuffer;
    private PlayerController _playerController;

    public void Init(InputBuffer inputBuffer)
    {
        _inputBuffer = inputBuffer;
        _playerController = GetComponent<PlayerController>();
        RegistAction();
    }

    public void RegistAction()
    {
        _inputBuffer.DropAction.started += Drop;
    }

    public void UnRegistAction()
    {
        _inputBuffer.DropAction.started -= Drop;
    }

    private void Drop(InputAction.CallbackContext context)
    {
        // PlayerController に処理を委譲して、現在アクティブな手からドロップさせる
        if (_playerController == null)
            _playerController = GetComponent<PlayerController>();

        _playerController?.DropFromActiveHand();
    } 
}
