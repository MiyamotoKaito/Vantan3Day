using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMover : MonoBehaviour
{
    private InputBuffer _inputBuffer;
    private Camera _camera;
    private CameraConfig _cameraConfig;
    private Vector2 _moveInput;

    public void Init(InputBuffer inputBuffer, CameraConfig cameraConfig, Camera camera)
    {
        _inputBuffer = inputBuffer;
        _cameraConfig = cameraConfig;
        _camera = camera;
        RegistAction();
    }

    private void OnDestroy()
    {
        if (_inputBuffer != null)
        {
            _inputBuffer.MoveCameraAction.performed -= MoveCamera;
            _inputBuffer.MoveCameraAction.canceled -= MoveCamera;
        }
    }

    private void RegistAction()
    {
        if (_inputBuffer != null)
        {
            _inputBuffer.MoveCameraAction.performed += MoveCamera;
            _inputBuffer.MoveCameraAction.canceled += MoveCamera;
        }
    }

    private void MoveCamera(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
        Debug.Log($"CameraMover.MoveCamera: phase={context.phase}, input={_moveInput}");
    }

    private void Update()
    {
        if (_camera == null || _cameraConfig == null || _inputBuffer == null)
            return;

        var position = _camera.transform.position;
        float delta = _moveInput.x * _cameraConfig.XMoveSpeed * Time.deltaTime;
        position.x += delta;
        position.x = Mathf.Clamp(position.x, _cameraConfig.XMin, _cameraConfig.XMax);
        _camera.transform.position = position;
    }
}
