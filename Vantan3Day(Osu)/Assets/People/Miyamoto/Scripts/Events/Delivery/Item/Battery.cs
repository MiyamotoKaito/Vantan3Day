using System;
using Ingame;
using UnityEngine;

public class Battery : BaseDeliveryItem
{
    private Battely _battely;
    [SerializeField]
    private float _reChargeAmount;
    private void Start()
    {
        _battely = FindAnyObjectByType<Battely>();
    }
    public override void GetEffect()
    {
        //電力を上げる処理
        Debug.Log("Good");
        _battely.RechargeEnergy(_reChargeAmount);
    }
}