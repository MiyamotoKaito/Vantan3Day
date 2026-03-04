using UnityEngine;
using UnityEngine.SceneManagement;

public class Day : MonoBehaviour
{
   [SerializeField]
   public static int _dayCount = 0;

   [SerializeField]
   private int _maxDayCount = 4;
   [SerializeField]
   Timer _timer;

    private void Start()
    {
        _timer = FindAnyObjectByType<Timer>();
        _timer.OnTimerFinished += FinishDay;
    }



    private void FinishDay()
    {
        _dayCount++;
        if (_dayCount >= _maxDayCount)
        {
            SceneManager.Instance.OnSceneLoaded?.Invoke("ClearScene");
            return;
        }
       SceneManager.Instance.OnSceneLoaded?.Invoke("MasterScene");
        _timer.ResetTimer();
        Debug.Log("Day " + _dayCount);
    }


}
