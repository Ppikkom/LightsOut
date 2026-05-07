using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timers : MonoBehaviour
{
    [SerializeField] private TimerUI startTimer;
    [SerializeField] private TimerUI mainTimer;

    [SerializeField] private GameObject pausePanel;

    void Start()
    {
        startTimer.ShowUI();
        startTimer.onTimerEnd += mainTimer.TimerStart;
        startTimer.TimerStart();
    }
}
