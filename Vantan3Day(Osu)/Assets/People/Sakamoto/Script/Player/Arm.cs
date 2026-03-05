using Cysharp.Threading.Tasks;
using UnityEngine;

public class Arm : GoalObject
{
    public string ItemUse => _itemUse;
    public bool IsActive { get; private set; } = true;
    public bool IsRightArm => _isRightArm;
    public PlayerController PlayerController => _playerController;
    public bool OnPapperArm = false;
    /// <summary>
    /// 右手か左手か。
    /// </summary>
    [SerializeField] private bool _isRightArm;
    [SerializeField] private string _ready = "Ready";
    [SerializeField] private string _attack = "Attack";
    [SerializeField] private string _isPickUped = "IsPickUped";
    [SerializeField] private string _itemUse = "ItemUse";
    [SerializeField] private float _speed = 1.0f;
    private Animator _animator;
    private PlayerController _playerController;
    private ArmMover _armMover;
    private Rigidbody2D _rb;
    private bool _canAttack = false;
    private Vector2 _prevPos;
    private Vector2 _vel;


    public void Init(ArmMover armMover, PlayerController plaeyrController)
    {
        _armMover = armMover;
        _playerController = plaeyrController;
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        IsActive = _isRightArm;
        _isActiveObject = IsActive;
    }

    public void PlayItemUse()
    {
        _animator.SetTrigger(_itemUse);
    }

    // 明示的にアクティブ状態を設定するように変更
    public void SetActive(bool active)
    {
        IsActive = active;
        _isActiveObject = active;
    }

    private void Update()
    {
        if (_animator == null) return;
        if (_playerController == null) return;


        _animator.SetBool(_isPickUped, _playerController.IsPickUped);



        var curPos = (Vector2)transform.position;
        _vel = (curPos - _prevPos) / Time.deltaTime;
        _prevPos = curPos;
        _pos = curPos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Pepper"))
        {
            OnPapperArm = true;
        }
        if (_playerController.IsPickUped) return;
        if (collision.gameObject.CompareTag("Fly") && IsActive)
        {
            _animator.SetBool(_ready, true);
            _canAttack = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Fly>(out var fly) && IsActive)
        {
            // Rigidbody2D.velocity can be zero if the arm is moved by transform.
            // Use measured local velocity instead.
            Debug.Log($"Arm collided with Fly. Measured velocity: {_vel.magnitude}");
            _playerController.SetIsFly(IsActive && fly.IsArm);
            if (_vel.magnitude > _speed)
            {
                fly.MoveToHigh().Forget();
                Debug.Log("Fly moved to high");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Pepper"))
        {
            OnPapperArm = false;
        }
        if (_playerController.IsPickUped) return;
        if (collision.gameObject.CompareTag("Fly") && IsActive)
        {
            _animator.SetBool(_ready, false);
            _canAttack = false;
        }
    }

    public void Attack()
    {
        if (IsActive && _canAttack)
        {
            _animator.SetTrigger(_attack);
            _canAttack = false;
        }
    }
}
