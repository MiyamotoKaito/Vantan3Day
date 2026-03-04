using UnityEngine;
using UnityEngine.EventSystems;

public class MouseRaycast2D : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null)
            {
                Debug.Log("クリックした: " + hit.collider.name);

                hit.collider.GetComponent<IPointerClickHandler>()?.OnPointerClick(null);
            }
        }
    }
}
