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

    public void Init(ArmMover armMover, PlayerController plaeyrController)
    {
        _armMover = armMover;
        _playerController = plaeyrController;
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_isRightArm)
        {
            if (_isRightArm == _playerController.IsRightHand)
            {
                IsActive = true;
            }
            else
            {
                IsActive = false;
            }
        }
        else
        {
            if (_isRightArm == _playerController.IsRightHand)
            {
                IsActive = true;
            }
            else
            {
                IsActive = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Fly") && IsActive)
        {
            Debug.Log("ハエが手に触った");
            _animator.SetBool(_ready, true);
            _canAttack = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Fly") && IsActive)
        {
            Debug.Log("ハエが手から離れた");
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
