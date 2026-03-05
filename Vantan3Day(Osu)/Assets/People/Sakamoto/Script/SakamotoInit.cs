using Unity.VisualScripting;
using UnityEngine;

public class SakamotoInit : MonoBehaviour
{
    [SerializeField] private InputBuffer _inputBuffer;

    [Header("Camera Settings")]
    [SerializeField] private CameraMover _cameraMover;
    [SerializeField] private CameraConfig _cameraConfig;
    [SerializeField] private Camera _camera;

    [Header("Player Settings")]
    [SerializeField] private PlayerController _playerController;

    public void Start()
    {
        _cameraMover?.Init(_inputBuffer, _cameraConfig, _camera);
        _playerController?.Init(_inputBuffer);
    }
}
