using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public FliesCount FliesCount => _fliesCount;

    [SerializeField]
    [Tooltip("イベントが起きるまでのインターバル")]
    private float _interval;
    private FliesCount _fliesCount;
    private float time = 0;

    [Header("ハエ")]
    [SerializeField]
    private GameObject _fly;
    [SerializeField]
    private Vector3[] _initPos;

    private void Awake()
    {

    }
    private void Start()
    {
        _fliesCount = new FliesCount();
    }
    private void Update()
    {
         time += Time.deltaTime;
        if (time > _interval)
        {
            OnEvent();
            time = 0;
        }
    }

    private void OnEvent()
    {
        GenerateFly();
    }

    private void GenerateFly()
    {
      var obj = Instantiate(_fly, _initPos[UnityEngine.Random.Range(0, _initPos.Length)], Quaternion.identity);
        obj.TryGetComponent<Fly>(out var fly);
        fly.Init(UnityEngine.Random.Range(0, 2) == 0 ? -1 : 1);
        _fliesCount.Register(fly);
    }
}
