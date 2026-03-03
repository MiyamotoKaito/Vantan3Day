using System;
using UnityEngine;
/// <summary>
/// ハエのクラス
/// </summary>
public class Fly : MonoBehaviour
{
    // ボタンに群がるハエの数が変更された時のイベント
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

    //[SerializeField]
    //private float 
    private int _direction = 1;
    private Vector2 _start;
    private void Start()
    {
        //_emargencyButton = FindObjectOfType<EmargencyButton>();
        _start = transform.position;
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
        var y = _start.y + Mathf.Sin(Time.time * _frequency) * _amplitude;
        // ハエの位置を更新
        transform.position = new Vector3(x, y, 0);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("RangeOfMotion"))
        {
            _direction *= -1; // 方向を反転
        }
    }
    /// <summary>
    /// ハエを殺す
    /// </summary>
    public void Kill()
    {
        this.gameObject.SetActive(false);
    }
}
