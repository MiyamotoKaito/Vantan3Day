using System.Collections.Generic;
using UnityEngine;

public class BadItem : BaseDeliveryItem
{

    public override void GetEffect()
    {
        // なんか悪い効果
        Debug.Log("Bad");
        ApplyEffect();
    }
    private void ApplyEffect()
    {
        for (int i = 1; i <= _numberOfEvents[Day._dayCount]; i++)
        {
            _events[Random.Range(0, _events.Count)].OnEvent(_eventManager);
        }
    }
    private void Start()
    {
        _eventManager = FindAnyObjectByType<EventManager>();
    }
    [Header("使用可能なイベントリスト")]
    [SerializeReference, SubclassSelector]
    private List<IEvent> _events;
    [Header("日にちによって起こるイベントの回数")]
    [SerializeField]
    private List<int> _numberOfEvents;

    private EventManager _eventManager;
}
