using System;
using UnityEngine;
using UnityEngine.Rendering;
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
    private int _direction = 1;

    [Header("目標設定")]
    [SerializeField]
    [Tooltip("ハエの群がる場所")]
    private GameObject[] Goals;
    [SerializeField]
    [Tooltip("ハエが標的を定めるまでの時間")]
    private float _waitTime;

    private GameObject _currentGoal;
    private Vector2 _start;
    private float _time;
    private void Start()
    {
        //_emargencyButton = FindObjectOfType<EmargencyButton>();
        _start = transform.position;
    }
    private void Update()
    {
        if (!GetGoal())
        {
            Move();
            return;
        }
        this.transform.position = Vector2.MoveTowards(this.transform.position,
            _currentGoal.transform.position,
            _speed * Time.deltaTime);
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
    private bool GetGoal()
    {
        _time += Time.deltaTime;
        if (_time > _waitTime)
        {
            _currentGoal = Goals[UnityEngine.Random.Range(0, Goals.Length)];
            return true;
        }
        return false;
    }
    /// <summary>
    /// ハエを殺す
    /// </summary>
    public void Kill()
    {
        this.gameObject.SetActive(false);
    }
}
