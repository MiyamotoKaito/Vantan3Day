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

    [SerializeField] private PlayerConfig _playerConfig; // 手の移動範囲などを保持する ScriptableObject
    [SerializeField] private Camera _camera; // 使用するカメラ（未設定時は Camera.main を使用）
    [SerializeField] private float _angleOffset = 0f; // スプライトの向き補正（度数）

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
        var armTransform = CurrentArm.transform;
        // ワールド空間で手への方向ベクトルを計算
        var dirWorld = CurrentHand.transform.position - armTransform.position;
        if (dirWorld.sqrMagnitude < 0.0001f)
            return; // ほとんど重なっている場合は回転不要

        // 親のローカル空間へ変換してから角度を求める（親の回転を考慮するため）
        Vector3 dirLocal = armTransform.parent != null ? armTransform.parent.InverseTransformDirection(dirWorld) : dirWorld;

        // 2D 回転（Z 軸）の角度を計算しオフセットを適用
        var angle = Mathf.Atan2(dirLocal.y, dirLocal.x) * Mathf.Rad2Deg + _angleOffset - 90f;

        // ローカル Z 回転を適用
        armTransform.localRotation = Quaternion.Euler(0f, 0f, angle);
    }
}
