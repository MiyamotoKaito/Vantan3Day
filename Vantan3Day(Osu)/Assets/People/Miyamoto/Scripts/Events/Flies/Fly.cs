using System;
using UnityEngine;

public class Fly : MonoBehaviour
{
    public event Action<int> FliesCountChanged;
    //private EmargencyButton _emargencyButton;
    [Header("ハエの動き")]
    [SerializeField]
    [Tooltip("ハエの飛び回る速さ")]
    [Min(0)]
    private float _speed = 0.5f;
    [SerializeField]
    [Tooltip("ハエの上下の揺れ幅")]
    private float _amplitude;
    [SerializeField]
    [Tooltip("ハエの上下の揺れの速さ")]
    private float _frequency;

    private int _direction = 1;
    private float _startY;
    private void Start()
    {
        //_emargencyButton = FindObjectOfType<EmargencyButton>();
        _startY = transform.position.y;
    }
    private void Update()
    {
        Move();
    }
    /// <summary>
    /// ハエの挙動
    /// </summary>
    private void Move()
    {
        // ハエの横移動
        var x = transform.position.x + _speed * Time.deltaTime * _direction;
        // ハエの上下の揺れ
        var y = _startY + Mathf.Sin(Time.time * _frequency) * _amplitude;
        // ハエの位置を更新
        transform.position = new Vector3(x, y, 0);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        // ハエが移動範囲の端に達したとき、方向を反転させる
        if (other.gameObject.CompareTag("RangeOfMotion"))
        {
            _direction *= -1;
        }
    }
}
