using System.Collections;
using UnityEngine;

public class Item : MonoBehaviour
{
    public bool IsPickable { get; private set; } = true;

    [SerializeField] private string _handTag = "Hand";
    [SerializeField] private float _dropDelay = 2f;

    private Collider2D _collider;
    private Rigidbody2D _rb;

    private void Start()
    {
        _collider = GetComponent<Collider2D>();
        _rb = GetComponent<Rigidbody2D>();
    }

    public IEnumerator Drop()
    {
        this.gameObject.transform.SetParent(null);
        _rb.bodyType = RigidbodyType2D.Dynamic;
        this._collider.enabled = true;
        yield return new WaitForSeconds(_dropDelay);
        IsPickable = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(_handTag) && IsPickable)
        {
            if (TryGetComponent(out PlayerController playerController) && IsPickable)
            {
                if (playerController.IsFlying) return; // 飛行中は拾えない
            }
            this.gameObject.transform.SetParent(collision.gameObject.transform);
            this.gameObject.transform.localPosition = Vector3.zero;
            _rb.bodyType = RigidbodyType2D.Kinematic;
            this._collider.enabled = false;
            IsPickable = false;
        }

    }
}
