using UnityEngine;

public class TimerUI : MonoBehaviour
{
    [SerializeField]
    Timer _timer;


    void Update()
    {
       string time =  _timer.GetTime().ToString();
       Debug.Log(time);
    }
}
