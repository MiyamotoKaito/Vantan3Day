using UnityEngine;
using UnityEngine.EventSystems;

public class Cursor : MonoBehaviour, IPointerClickHandler
{
    public bool IsOpen => _isOpen;

    [SerializeField] private SpriteRenderer _currentRenderer;
    [SerializeField] private Sprite _startRenderer;
    [SerializeField] private Sprite _endRenderer;
    [SerializeField] private Sprite _openRenderer;

    private bool _isOpen = false;
    private bool _canOpen = false;

    private void Start()
    {
        _currentRenderer.sprite = _startRenderer;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Hand") && !_isOpen)
        {
            _currentRenderer.sprite = _endRenderer;
            _canOpen = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Hand") && !_isOpen)
        {
            _currentRenderer.sprite = _startRenderer;
            _canOpen = false;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!_canOpen) return;

        if (_isOpen)
        {
            _currentRenderer.sprite = _startRenderer;
            _isOpen = false;
        }
        else
        {
            _currentRenderer.sprite = _openRenderer;
            _isOpen = true;
        }
        Debug.Log("Cursor.OnPointerClick: Clicked on cursor object " + gameObject.name);
        //_animator.SetTrigger("Clicked");
    }
}
