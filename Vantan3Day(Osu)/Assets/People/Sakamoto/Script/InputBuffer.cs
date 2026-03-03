using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class InputBuffer : MonoBehaviour
{
    public InputAction MoveCameraAction => _moveCameraAction;
    public InputAction InteractiveAction => _interactiveAction;
    public InputAction DropAction => _dropAction;
    public InputAction ArmChangeAction => _armChangeAction; 

    private const string MOVE_Camera_ACTION = "MoveCamera";
    private const string INTERACT_ACTION = "Interact"; 
    private const string DROP_ACTION = "Drop";
    private const string ARM_CHANGE_ACTION = "ArmChange";

    private InputAction _moveCameraAction;
    private InputAction _interactiveAction;
    private InputAction _dropAction;
    private InputAction _armChangeAction;

    private void Awake()
    {
        if (TryGetComponent<PlayerInput>(out var playerInput))
        {
            _moveCameraAction = playerInput.actions[MOVE_Camera_ACTION];
            _interactiveAction = playerInput.actions[INTERACT_ACTION];
            _dropAction = playerInput.actions[DROP_ACTION];
            _armChangeAction = playerInput.actions[ARM_CHANGE_ACTION];
        }
    }
}