using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Cursor : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private SpriteRenderer _currentRenderer;
    [SerializeField] private Sprite _startRenderer;
    [SerializeField] private Sprite _endRenderer;
    [SerializeField] private Animator _animator;

    private void Start()
    {
        _currentRenderer.sprite = _startRenderer;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Hand"))
        {
            _currentRenderer.sprite = _endRenderer;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Hand"))
        {
            _currentRenderer.sprite = _startRenderer;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _currentRenderer.sprite = null;
        Debug.Log("Cursor.OnPointerClick: Clicked on cursor object " + gameObject.name);
        //_animator.SetTrigger("Clicked");
    }
}
