
using UnityEngine;

namespace TitleScreen
{
    [DisallowMultipleComponent]
    public class MouseRaycast : MonoBehaviour
    {
        [SerializeField] Camera worldCamera;
        [SerializeField] LayerMask raycastLayerMask = ~0;
        [SerializeField] TestButton testButton;

        ButtonHover _currentHover;
        TitleButton _titleButton;
        Animator _animator;

        void Awake()
        {
            if (worldCamera == null)
            {
                worldCamera = Camera.main;
            }
        }

        void Update()
        {
            if (RaycastHoverTarget())
            {
                testButton.Hover(_currentHover.buttonType);
            }
            else if (_currentHover != null)
            {
                testButton.Unhover(_currentHover.buttonType);
                _currentHover = null;
            }
            if(_currentHover != null&& _animator != null)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Debug.Log("click");
                    _animator.SetBool("Push", true);
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    _animator.SetBool("Push", false);
                    _animator = null;
                }
            }
           


        }

        bool RaycastHoverTarget()
        {
            if (worldCamera == null) return false;

            Vector2 mouseWorld = worldCamera.ScreenToWorldPoint(Input.mousePosition);
            var hit = Physics2D.Raycast(mouseWorld, Vector2.zero, 0f, raycastLayerMask);

            if (hit.collider != null &&
                hit.collider.TryGetComponent<ButtonHover>(out var hover))
            {
                _animator = hover.GetComponent<Animator>();

                Debug.Log("hit");
                _currentHover = hover;
                return true;
            }

            Debug.Log("miss");
            return false;
        }



    }
}
