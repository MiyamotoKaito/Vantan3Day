using UnityEngine;

public class CameraMover : MonoBehaviour
{
    private InputBuffer _inputBuffer;

    public void Init(InputBuffer inputBuffer)
    {
        _inputBuffer = inputBuffer;
    }
}
