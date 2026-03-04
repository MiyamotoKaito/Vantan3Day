using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// ArmMover は手（CurrentHand）と腕（CurrentArm）を制御します。
/// </summary>
public class ArmMover : MonoBehaviour
{
    [HideInInspector]
    public GameObject CurrentHand;
    [HideInInspector]
    public GameObject CurrentArm;

    [SerializeField] private PlayerConfig _playerConfig;
    [SerializeField] private Camera _camera;
    [SerializeField] private float _angleOffset = -90f;
    [SerializeField] private GameObject _rigthTarget;
    [SerializeField] private GameObject _leftTarget;
    private PlayerController _playerController;

    public void Init()
    {
        if (_camera == null) _camera = Camera.main;
        _playerController = GetComponent<PlayerController>();
    }

    private void Reset()
    {
        if (_camera == null) _camera = Camera.main;
    }

    private void Update()
    {
        if (CurrentHand == null || CurrentArm == null || _playerConfig == null)
            return;

        HandMove();
        ArmMove();
    }

    private void HandMove()
    {
        if (_camera == null)
            _camera = Camera.main;

        var mouseScreen = Mouse.current != null ? Mouse.current.position.ReadValue() : (Vector2)Input.mousePosition;

        var handPos = CurrentHand.transform.position;
        var screenDepth = _camera.WorldToScreenPoint(handPos).z;
        var screenPoint = new Vector3(mouseScreen.x, mouseScreen.y, screenDepth);
        var worldPos = _camera.ScreenToWorldPoint(screenPoint);

        // X の制限は左右で異なる設定を使用する
        if (_playerController.IsRightHand)
            worldPos.x = Mathf.Clamp(worldPos.x, _playerConfig.RightHandMinX, _playerConfig.RightHandMaxX);
        else
            worldPos.x = Mathf.Clamp(worldPos.x, _playerConfig.LeftHandMinX, _playerConfig.LeftHandMaxX);

        worldPos.y = Mathf.Clamp(worldPos.y, _playerConfig.HandMinY, _playerConfig.HandMaxY);

        CurrentHand.transform.position = worldPos;
    }

    private void ArmMove()
    {
        if (CurrentArm == null || CurrentHand == null)
            return;

        var armTransform = CurrentArm.transform;
        var handTransform = CurrentHand.transform;

        var target = _playerController.IsRightHand ? _rigthTarget : _leftTarget;
        if (target == null)
            return;

        var targetVector = target.transform.position - armTransform.position;
        CurrentArm.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(targetVector.y, targetVector.x) * Mathf.Rad2Deg + _angleOffset);
    }
}
