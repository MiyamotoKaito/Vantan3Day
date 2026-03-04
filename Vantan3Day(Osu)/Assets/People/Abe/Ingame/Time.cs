using UnityEngine;

public class Timer : MonoBehaviour
{
    private float _timer;

    private void Update()
    {
        _timer += Time.deltaTime;
    }
    public float GetTime()
    {
        return _timer;
    }

}
