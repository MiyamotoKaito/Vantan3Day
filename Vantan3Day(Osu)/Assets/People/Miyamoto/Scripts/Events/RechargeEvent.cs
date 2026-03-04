using Ingame;
using UnityEngine;

[System.Serializable]
public class RechargeEvent : IEvent
{
    [SerializeField]
    private float _reChargeAmount;
    public void OnEvent(EventManager eventManager)
    {
        Debug.Log("りちゃ～～じ");
        var battely = GameObject.FindAnyObjectByType<Battely>();
        battely.RechargeEnergy(_reChargeAmount);
    }
}
