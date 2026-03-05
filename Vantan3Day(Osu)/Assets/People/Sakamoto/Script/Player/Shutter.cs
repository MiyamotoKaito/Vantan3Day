using UnityEngine;
using UnityEngine.EventSystems;

public class Shutter : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private bool _isOpen = false;
    private bool _canOpen = false;

    private void Start()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Hand") && !_isOpen)
        {
            _canOpen = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Hand") && !_isOpen)
        {
            _canOpen = false;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!_canOpen) return;

        if (_isOpen)
        {
            _isOpen = false;
            _animator.SetTrigger("Close");
        }
        else
        {
            _isOpen = true;
            _animator.SetTrigger("Open");

        }
    }
}
