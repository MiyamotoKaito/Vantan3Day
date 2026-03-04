using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public FliesCount FliesCount => _fliesCount;

    [SerializeField]
    [Tooltip("イベントが起きるまでのインターバル")]
    private float _interval;
    private FliesCount _fliesCount;
    private float time = 0;

    [SerializeReference, SubclassSelector]
    private List<IEvent> _event;

    private void Awake()
    {
        _fliesCount = new FliesCount();
    }
    private void Update()
    {
        time += Time.deltaTime;
        if (time > _interval)
        {
            _event[UnityEngine.Random.Range(0, _event.Count)].OnEvent(this);
            time = 0;
        }
    }
}
