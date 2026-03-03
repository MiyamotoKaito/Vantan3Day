using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class InputBuffer : MonoBehaviour
{
    public InputAction MoveCameraAction => _moveCameraAction;
    public InputAction InteractiveAction => _interactiveAction;
    public InputAction DropAction => _dropAction;

    private const string MOVE_Camera_ACTION = "MoveCamera";
    private const string INTERACT_ACTION = "Interact"; 
    private const string DROP_ACTION = "Drop";

    private InputAction _moveCameraAction;
    private InputAction _interactiveAction;
    private InputAction _dropAction;

    private void Awake()
    {
        if (TryGetComponent<PlayerInput>(out var playerInput))
        {
            _moveCameraAction = playerInput.actions[MOVE_Camera_ACTION];
            _interactiveAction = playerInput.actions[INTERACT_ACTION];
            _dropAction = playerInput.actions[DROP_ACTION];
        }
    }
}