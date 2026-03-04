using UnityEngine;

public class TimerUI : MonoBehaviour
{
    [SerializeField]
    Timer _timer;


    void Update()
    {
       string time =  _timer.GetTime().ToString("F2");
       Debug.Log(time);
    }
    void Start()
    {
        _timer.OnTimerFinished += () =>
        {
            Debug.Log("Timer Finished!");
        };
    }
}
