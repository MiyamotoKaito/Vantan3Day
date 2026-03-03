using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class InputBuffer : MonoBehaviour
{
    public InputAction MoveCameraAction => _moveCameraAction;
    public InputAction InteractiveAction => _interactiveAction;
    public InputAction DropAction => _dropAction;

    private const string MOVE_Camera_ACTION = "MoveCamera";
    private const string Interactive_Action = "Interact"; 
    private const string Drop_Action = "Drop";

    private InputAction _moveCameraAction;
    private InputAction _interactiveAction;
    private InputAction _dropAction;

    private void Awake()
    {
        if (TryGetComponent<PlayerInput>(out var playerInput))
        {
            _moveCameraAction = playerInput.actions[MOVE_Camera_ACTION];
            _interactiveAction = playerInput.actions[Interactive_Action];
            _dropAction = playerInput.actions[Drop_Action];
        }
    }
}