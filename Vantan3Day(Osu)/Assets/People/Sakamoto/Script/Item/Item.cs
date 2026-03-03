using UnityEngine;

public class Item : MonoBehaviour
{
    public bool IsPickable { get; private set; } = true;

    [SerializeField] private string _handTag = "Hand";

    private Collider2D _collider;
    private Rigidbody2D _rb;

    private void Start()
    {
        _collider = GetComponent<Collider2D>();
        _rb = GetComponent<Rigidbody2D>();
    }

    public void Drop()
    {
        this.gameObject.transform.SetParent(null);
        _rb.bodyType = RigidbodyType2D.Dynamic;
        this._collider.enabled = true;
        IsPickable = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(_handTag) && IsPickable)
        {
            this.gameObject.transform.SetParent(collision.gameObject.transform);
            this.gameObject.transform.localPosition = Vector3.zero;
            _rb.bodyType = RigidbodyType2D.Kinematic;
            this._collider.enabled = false;
            IsPickable = false;
        }

    }
}
