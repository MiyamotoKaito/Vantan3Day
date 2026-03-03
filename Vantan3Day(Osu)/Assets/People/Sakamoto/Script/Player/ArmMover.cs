using UnityEngine;
using UnityEngine.InputSystem;

public class ArmMover : MonoBehaviour
{
    public GameObject CurrentArm;

    [SerializeField] private PlayerConfig _playerConfig;

    private void Update()
    {
        var position = CurrentArm.transform.position;
        position.x = Mouse.current.position.ReadValue().x;
        position.y = Mouse.current.position.ReadValue().y;
        position.x = Mathf.Clamp(position.x, _playerConfig.ArmMinX, _playerConfig.ArmMaxX);
        position.y = Mathf.Clamp(position.y, _playerConfig.ArmMinY, _playerConfig.ArmMaxY);
        CurrentArm.transform.position = position;
    }
}
