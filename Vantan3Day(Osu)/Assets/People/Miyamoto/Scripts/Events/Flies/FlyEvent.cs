using UnityEngine;
[System.Serializable]
public class FlyEvent : IEvent
{
    [Header("ハエ")]
    [SerializeField]
    private GameObject _fly;
    [SerializeField]
    private Vector3[] _initPos;
    public void OnEvent(EventManager eventManager)
    {
        FlyGenerate(eventManager);
    }
    private void FlyGenerate(EventManager eventManager)
    {
        var obj = Object.Instantiate(_fly, _initPos[Random.Range(0, _initPos.Length)], Quaternion.identity);
        obj.TryGetComponent<Fly>(out var fly);
        fly.Init(Random.Range(0, 2) == 0 ? -1 : 1);
        eventManager.FliesCount.Register(fly);
    }
}
