using System;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseDeliveryItem : MonoBehaviour, IDelivery, IPointerClickHandler
{
    public event Action OnClicked;

    [SerializeField]
    protected DeliveryConfig _deliveryConfig;
    // [SerializeField]
    // protected GameObject _provider;
    protected Animator _animator;

    public abstract void GetEffect();

    public void OnPointerClick(PointerEventData eventData)
    {
        GetEffect();
        OnClicked?.Invoke();
        Destroy(gameObject);
    }
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
}
