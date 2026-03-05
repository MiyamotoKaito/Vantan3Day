using UnityEngine;
using UnityEngine.EventSystems;

public class Shutter : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Animator _animator;
    [SerializeField] private bool _isOpen = false;
    [SerializeField] private string _str = "";
    private bool _canOpen = false;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Hand"))
        {
            _canOpen = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Hand"))
        {
            _canOpen = false;
        }
    }
    public void OpenShutter(bool isOpen)
    {
        _animator.SetBool("Open", isOpen);
        Debug.Log("OpenShutter: " + isOpen);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!_canOpen) return;

        if (_isOpen)
        {
            _isOpen = false;
            _animator.SetBool(_str,_isOpen);
        }
        else
        {
            _isOpen = true;
            _animator.SetBool(_str,_isOpen);

        }
    }
}
