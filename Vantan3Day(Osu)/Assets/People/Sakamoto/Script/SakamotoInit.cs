using UnityEditor.Search;
using UnityEngine;

public class SakamotoInit : MonoBehaviour
{
    [SerializeField] private InputBuffer _inputBuffer;
    [SerializeField] private CameraMover _cameraMover;
    [SerializeField] private CameraConfig _cameraConfig;
    [SerializeField] private Camera _camera;

    public void Awake()
    {
        _cameraMover?.Init(_inputBuffer, _cameraConfig, _camera);
    }
}
