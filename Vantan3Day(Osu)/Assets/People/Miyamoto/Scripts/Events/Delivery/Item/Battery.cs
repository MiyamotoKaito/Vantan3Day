using System;
using Ingame;
using UnityEngine;

public class Battery : BaseDeliveryItem
{

    [SerializeReference, SubclassSelector]
    private IEvent _rechargeEvent;
    public override void GetEffect()
    {
        //電力を上げる処理
        Debug.Log("Good");
        _rechargeEvent.OnEvent(null);
    }
}