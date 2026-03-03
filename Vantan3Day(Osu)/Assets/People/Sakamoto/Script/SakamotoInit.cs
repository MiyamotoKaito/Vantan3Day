using UnityEngine;

public class SakamotoInit : MonoBehaviour
{
    [SerializeField] private InputBuffer _inputBuffer;
    [SerializeField] private CameraMover _cameraMover;

    public void Awake()
    {
        _cameraMover?.Init(_inputBuffer);
    }
}
