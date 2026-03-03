using UnityEngine;
using UnityEngine.InputSystem;

// プレイヤーの腕と手をマウスに追従させるコンポーネント
// ・CurrentHand: マウスに合わせて移動する手の GameObject（ワールド座標で移動、範囲は PlayerConfig で指定）
// ・CurrentArm: 手の方向を向くように回転する腕の GameObject（親のローカル回転を使用）
public class ArmMover : MonoBehaviour
{
    [HideInInspector]
    public GameObject CurrentHand;
    [HideInInspector]
    public GameObject CurrentArm;

    /// <summary>
    ///  現在右手を操作しているか（初期値は右手）
    /// </summary>
    public bool IsRightHand { get; set; } = true;

    [SerializeField] private PlayerConfig _playerConfig;
    [SerializeField] private Camera _camera;
    [SerializeField] private float _angleOffset = -90f;
    [SerializeField] private GameObject _rigthTarget;
    [SerializeField] private GameObject _leftTarget;

    public void OnChange()
    {
        if (IsRightHand) IsRightHand = false;
        else IsRightHand = true;
    }

    private void Reset()
    {
        if (_camera == null) _camera = Camera.main;
    }

    private void Awake()
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
        if (IsRightHand)
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

        var target = IsRightHand ? _rigthTarget : _leftTarget;
        if (target == null)
            return;

        // arm は hand の子になっている想定。
        // 手基準のローカル座標で、arm の根元位置からターゲット位置へのベクトルを計算し角度を求める。
        var targetLocal = handTransform.InverseTransformPoint(target.transform.position);
        var armLocalPos = armTransform.localPosition; // arm のローカル位置（手基準）
        var dirLocal = targetLocal - armLocalPos;
        if (dirLocal.sqrMagnitude < 0.0001f)
            return;

        var angle = Mathf.Atan2(dirLocal.y, dirLocal.x) * Mathf.Rad2Deg + _angleOffset;
        armTransform.localRotation = Quaternion.Euler(0f, 0f, angle);
    }
}
