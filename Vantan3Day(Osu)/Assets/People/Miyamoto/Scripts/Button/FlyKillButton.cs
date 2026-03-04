using Ingame;
using UnityEngine;
using UnityEngine.EventSystems;

public class FlyKillButton : MonoBehaviour, IPointerClickHandler
{
    private Battely _battely;
    [SerializeField]
    private float _consumption;
    private void Start()
    {
        _battely = FindAnyObjectByType<Battely>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (_battely.CurrentEnergy >= _consumption)
        {
            var flies = FindObjectsByType<Fly>(FindObjectsSortMode.None);
            foreach (var fly in flies)
            {
                fly.Kill();
            }
        }
        _battely.ConsumeEnergy(_consumption);
    }
}
