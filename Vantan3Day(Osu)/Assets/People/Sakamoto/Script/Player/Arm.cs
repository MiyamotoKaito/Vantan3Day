using UnityEngine;

public class Arm : MonoBehaviour
{
    public bool IsActive { get; private set; } = true;
    public bool IsRightArm => _isRightArm;
    public PlayerController PlayerController => _playerController;
    /// <summary>
    /// 右手か左手か。
    /// </summary>
    [SerializeField] private bool _isRightArm;
    [SerializeField] private string _ready = "Ready";
    [SerializeField] private string _attack = "Attack";
    private Animator _animator;
    private PlayerController _playerController;
    private ArmMover _armMover;
    private bool _canAttack = false;

    private void Awake()
    {
        // 保険: Init が呼ばれていないケースでも Animator を取得する
        if (_animator == null)
            _animator = GetComponent<Animator>();
    }

    public void Init(ArmMover armMover, PlayerController plaeyrController)
    {
        _armMover = armMover;
        _playerController = plaeyrController;
        _animator = GetComponent<Animator>();
        IsActive = _isRightArm;
    }

    // 明示的にアクティブ状態を設定するように変更
    public void SetActive(bool active)
    {
        IsActive = active;
    }

    private void Update()
    {
        if (_animator == null) return;
        if (_playerController == null) return;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Fly") && IsActive)
        {
            _animator.SetBool(_ready, true);
            _canAttack = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
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
