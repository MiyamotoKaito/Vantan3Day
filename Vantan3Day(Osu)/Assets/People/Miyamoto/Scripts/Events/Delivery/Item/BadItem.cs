using UnityEngine;

public class BadItem : BaseDeliveryItem
{
    public override void GetEffect()
    {
        // なんか悪い効果
        Debug.Log("Bad");
    }
}
