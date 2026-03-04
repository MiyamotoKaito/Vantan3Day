using UnityEngine;
using UnityEngine.UI;

public class Cursor : MonoBehaviour
{
    [SerializeField] private GameObject _startRenderer;
    [SerializeField] private GameObject _changeRenderer;

    private void Start()
    {
        _startRenderer.SetActive(true);
        _changeRenderer.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Hand"))
        {
            _changeRenderer.SetActive(true);
            _startRenderer.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Hand"))
        {
            _changeRenderer.SetActive(false);
            _startRenderer.SetActive(true);
        }
    }
}
