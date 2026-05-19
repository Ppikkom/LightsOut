using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timers : MonoBehaviour
{
    [SerializeField] private TimerUI startTimer;
    [SerializeField] private TimerUI mainTimer;
    void Start()
    {
        startTimer.ShowUI();
        startTimer.onTimerEnd += mainTimer.TimerStart;
        startTimer.TimerStart();
    }
}
