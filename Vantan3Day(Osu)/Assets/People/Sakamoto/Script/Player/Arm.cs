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
    private PlayerController _playerController;

    public void Init(PlayerController playerController)
    {
        _playerController = playerController;
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
            if (_isRightArm != _playerController.IsRightHand)
            {
                IsActive = true;
            }
            else
            {
                IsActive = false;
            }
        }
    }
}
