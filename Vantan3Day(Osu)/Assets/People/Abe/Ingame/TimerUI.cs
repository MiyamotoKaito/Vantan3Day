using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class TimerUI : MonoBehaviour
{
    [SerializeField]
    Timer _timer;

    [SerializeField]
    Image _timerFill;

    [SerializeField]
    TMP_Text _dayCountText;

    // [SerializeField]
    // Day _day;


    void Update()
    {
      _timerFill.fillAmount = _timer.GetTime() / _timer.GetClearTime();
      _dayCountText.text = Day._dayCount.ToString();
    }
    void Start()
    {
        _timer.OnTimerFinished += () =>
        {
            Debug.Log("Timer Finished!");
        };
    }
}
