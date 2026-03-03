using UnityEngine;
using UnityEngine.InputSystem;

public class DropAction : MonoBehaviour
{
    private InputBuffer _inputBuffer;

    public void Init(InputBuffer inputBuffer)
    {
        _inputBuffer = inputBuffer;
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

    } 
}
