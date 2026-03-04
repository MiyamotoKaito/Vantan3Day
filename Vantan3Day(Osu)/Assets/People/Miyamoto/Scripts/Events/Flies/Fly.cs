using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
/// <summary>
/// ハエのクラス
/// </summary>
public class Fly : MonoBehaviour, IPointerClickHandler
{
    // ボタンに群がるハエの数が変更された時のイベント
    public event Action<int> FliesCountChanged;
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
    [SerializeField]
    private float _animDuration;
    private int _direction = 1;

    [Header("目標設定")]
    [SerializeField]
    [Tooltip("ハエが標的を定めるまでの時間")]
    private float _waitTime;
    [SerializeField]
    [Tooltip("目的に到達可能域までの距離")]
    private float _distance;
    [SerializeField]
    [Tooltip("目的地到達後の横の揺れ幅")]
    private float _goalAmp;
    private GoalObject _currentGoal;

    private Vector2 _start;
    private float _time;
    private bool _isGettingGoal;
    private bool _isGoal;
    private bool _isReturn;

    private CancellationTokenSource _cts;
    private void Start()
    {
        _start = transform.position;
        _cts = new CancellationTokenSource();
    }
    private void Update()
    {
        if (_isReturn) return;

        // 上を徘徊
        if (!_isGettingGoal && !_isGoal)
        {
            Move();
            SetGoal();
            return;
        }
        // Patrol中はMoveToGoalを呼ばない
        if (!_isGoal)
            MoveToGoal();
    }
    /// <summary>
    /// 初期化
    /// </summary>
    /// <param name="direction"></param>
    public void Init(int direction)
    {
        Debug.Log("ハエ生成");
        _direction = direction;
        transform.rotation = Quaternion.Euler(0f, _direction > 0f ? 180f : 0f, 0f);
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
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("RangeOfMotion"))
        {
            Return(); // 方向を反転
        }
        if (other.gameObject.TryGetComponent<EmargencyButton>(out var button))
        {
            FliesCountChanged?.Invoke(1);
        }
    }
    /// <summary>
    /// ゴールをセット
    /// </summary>
    private void SetGoal()
    {
        _time += Time.deltaTime;
        if (_time > _waitTime)
        {
            var goals = FindObjectsByType<GoalObject>(FindObjectsSortMode.None)
                        .Where(g => !g.IsActiveObject) // 非アクティブなGoalObjectのみ
                         .ToArray();

            if (goals.Length <= 0) return;

            _currentGoal = goals[UnityEngine.Random.Range(0, goals.Length)];
            _isGettingGoal = true;
            _time = 0f;
            if (transform.rotation.y == 0 || transform.position.x < _currentGoal.Position.x)
            {
                Return();
            }
            else if (transform.rotation.y == 180 || transform.position.x > _currentGoal.Position.x)
            {
                Return();
            }
        }
    }
    /// <summary>
    /// ゴールに向かって動く
    /// </summary>
    private void MoveToGoal()
    {
        if (_isGoal)
            return;

        this.transform.position = Vector2.MoveTowards(this.transform.position,
                                                      _currentGoal.Position,
                                                      _speed * Time.deltaTime);
        CheckGoal();
    }
    /// <summary>
    /// ゴールに到達したかチェック
    /// </summary>
    private void CheckGoal()
    {
        if (!_isGoal)
        {
            if (Vector2.Distance(this.transform.position, _currentGoal.Position) < _distance)
            {
                _isGoal = true;
                Patrol().Forget();
            }
        }
    }
    /// <summary>
    /// ハエを殺す
    /// </summary>
    private void Kill()
    {
        this.gameObject.SetActive(false);
    }
    /// <summary>
    /// 振り向きメソッド
    /// </summary>
    private void Return()
    {
        _direction *= -1;

        transform.DORotate(transform.rotation.y == 0? new Vector3(0, 180, 0) : new Vector3(0, 0, 0), _animDuration,RotateMode.Fast);
    }
    /// <summary>
    /// 上に戻る
    /// </summary>
    private void ReturnToHigh()
    {
        _isGoal = false;
        _isReturn = true;
        MoveToHigh().Forget();
    }
    /// <summary>
    /// 目的地付近で徘徊する
    /// </summary>
    /// <returns></returns>
    private async UniTask Patrol()
    {
        while (_isGoal)
        {
            var centerX = _currentGoal.Position.x;
            var currentX = transform.position.x;

            // 範囲外なら中心に向かって移動
            if (Mathf.Abs(currentX - centerX) >= _goalAmp)
            {
                var dir = centerX > currentX ? 1 : -1;
                var nextPos = Vector2.MoveTowards(
                    transform.position,
                    _currentGoal.Position,
                    _speed * Time.deltaTime
                );
                transform.position = nextPos;
            }
            else
            {
                // 範囲内なら通常パトロール
                var nextX = currentX + _speed * Time.deltaTime * _direction;
                if (Mathf.Abs(nextX - centerX) >= _goalAmp)
                {
                    Return();
                }
                transform.position = new Vector3(currentX + _speed * Time.deltaTime * _direction, transform.position.y, 0);
            }

            await UniTask.Yield(PlayerLoopTiming.Update, _cts.Token);
        }
    }
    /// <summary>
    /// 上に向かって動く処理
    /// </summary>
    /// <returns></returns>
    public async UniTask MoveToHigh()
    {
        Debug.Log($"MoveToHigh開始 現在地:{transform.position} 目標:{_start}");
        Vector2 currentPos = transform.position;
        while (true)
        {
            currentPos = Vector2.MoveTowards(currentPos, _start, _speed * Time.deltaTime);
            transform.position = currentPos;
            await UniTask.Yield(PlayerLoopTiming.Update, _cts.Token);

            if (Vector2.Distance(currentPos, _start) < 0.01f)
            {
                Debug.Log("到着");
                break;
            }
        }
        _isReturn = false;
        _isGoal = false;
        _isGettingGoal = false;
        Debug.Log("フラグリセット完了");
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (_isGettingGoal)
        {
            ReturnToHigh();
        }
    }
    private void OnDestroy()
    {
        _cts?.Cancel();
        _cts?.Dispose();
    }
}
