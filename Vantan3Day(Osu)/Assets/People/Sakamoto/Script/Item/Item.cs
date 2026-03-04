using System.Collections;
using UnityEngine;

public class Item : MonoBehaviour
{
    public bool IsPickable { get; private set; } = true;

    [SerializeField] private PlayerController _plaeyrController;
    [SerializeField] private string _handTag = "Hand";
    [SerializeField] private float _dropDelay = 2f;
    private SpriteRenderer _spriteRenderer;

    private Collider2D _collider;
    private Rigidbody2D _rb;

    private void Start()
    {
        _collider = GetComponent<Collider2D>();
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public IEnumerator Drop()
    {
        this.gameObject.transform.SetParent(null);
        _rb.bodyType = RigidbodyType2D.Dynamic;
        this._collider.enabled = true;
        this._spriteRenderer.enabled = true;
        yield return new WaitForSeconds(_dropDelay);
        IsPickable = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag(_handTag) || !IsPickable)
            return;
        // 衝突したオブジェクトから Arm を探す（親方向と子方向の両方をチェック）
        var arm = collision.GetComponentInParent<Arm>() ?? collision.GetComponentInChildren<Arm>();
        if (arm == null)
        {
            Debug.Log("Item.OnTriggerEnter2D: Arm component not found on collided object " + collision.gameObject.name + ". Allowing pickup by default.");
        }
        else
        {
            // デバッグでどの腕に当たったか確認
            Debug.Log($"Item.OnTriggerEnter2D: collided with arm (isRight={arm.IsRightArm}) on object {collision.gameObject.name}");

            // プレイヤーが飛行中等の理由で拾えない場合は弾く
            if (arm.PlayerController != null && arm.PlayerController.IsFlying) return;
            // 右手では拾えない仕様なら弾く
            if (arm.IsRightArm) return;
        }

        // Attach to the hand
        transform.SetParent(collision.transform);
        transform.localPosition = Vector3.zero;

        if (_rb != null)
        {
            _rb.bodyType = RigidbodyType2D.Kinematic;
            _rb.linearVelocity = Vector2.zero;
            _rb.angularVelocity = 0f;
        }

        if (_collider != null) _collider.enabled = false;
        if (_spriteRenderer != null) _spriteRenderer.enabled = false;

        IsPickable = false;
        _plaeyrController.PickUp();
    }
}
