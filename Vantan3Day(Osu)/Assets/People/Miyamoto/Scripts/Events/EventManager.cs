using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public FliesCount FliesCount => _fliesCount;

    [SerializeField]
    [Tooltip("イベントが起きるまでのインターバル")]
    private float _interval;
    private float _time = 0;
    [Header("ハエ")]
    [SerializeField]
    private int _maxFlyCount;
    private FliesCount _fliesCount;

    private EmargencyButton _button;

    [SerializeReference, SubclassSelector]
    private List<IEvent> _event;

    private void Awake()
    {
        _button = FindAnyObjectByType<EmargencyButton>();
        _fliesCount = new FliesCount(_button, _maxFlyCount);
    }
    private void Update()
    {
        _time += Time.deltaTime;
        if (_time > _interval)
        {
            _event[UnityEngine.Random.Range(0, _event.Count)].OnEvent(this);
            _time = 0;
        }
    }
}
