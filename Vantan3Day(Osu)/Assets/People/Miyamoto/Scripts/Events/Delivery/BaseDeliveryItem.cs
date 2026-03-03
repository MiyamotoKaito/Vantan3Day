using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseDeliveryItem : MonoBehaviour, IDelivery, IPointerClickHandler
{
    [SerializeField]
    protected DeliveryConfig _deliveryConfig;
    protected Animator _animator;
    public abstract void GetEffect();

    public void OnPointerClick(PointerEventData eventData)
    {
        GetEffect();
    }
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
}
