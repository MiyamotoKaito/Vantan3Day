using UnityEngine;
using DG.Tweening;
public class Open : MonoBehaviour
{
    public void Openshutter()
        {
            this.transform.DOMoveY(6.1f, 0.5f).SetEase(Ease.OutBounce);
        }
}
