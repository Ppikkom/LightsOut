using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timers : MonoBehaviour
{
    [SerializeField] private TimerUI startTimer;
    [SerializeField] private TimerUI mainTimer;

    [SerializeField] private Button pauseButton;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button quitButton;

    [SerializeField] private GameObject pausePanel;

    void Start()
    {
        startTimer.gameObject.SetActive(true);
        startTimer.AddTimerEndAction(() => mainTimer.TimerStart());
        startTimer.TimerStart();

        pauseButton.onClick.AddListener(OnPauseButtonClick);
        resumeButton.onClick.AddListener(OnResumeButtonClick);
        restartButton.onClick.AddListener(OnRestartButtonClick);
        quitButton.onClick.AddListener(OnQuitButtonClick);
    }

    private void OnPauseButtonClick()
    {
        pausePanel.SetActive(true);
        mainTimer.TimerPause();
    }

    private void OnResumeButtonClick()
    {
        pausePanel.SetActive(false);
        mainTimer.TimerResume();
    }

    private void OnRestartButtonClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnQuitButtonClick()
    {
        Debug.Log("Select Quit Button");
    }
}
