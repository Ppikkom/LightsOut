using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseUI : BaseUI
{
    [SerializeField] private TimerUI mainTimer;
    [SerializeField] private Button pauseButton;
    [SerializeField] private string text;
    private GameState tempState;

    void Start()
    {
        pauseButton.onClick.AddListener(ShowUI);
    }

    public override void ShowUI()
    {
        tempState = GameManager.Instance.GameState;
        objs.ShowUI();
        EditText(text);
        mainTimer.TimerPause();
        objs.SetGridCellSize(3);
        objs.ShowButton(UIButtonType.Resume);
        objs.ShowButton(UIButtonType.Restart);
        objs.ShowButton(UIButtonType.Quit);
        AddButtonEvnet();
    }

    public override void HideUI()
    {
        objs.HideUI();
        mainTimer.TimerResume();
        objs.HideButton(UIButtonType.Resume);
        objs.HideButton(UIButtonType.Restart);
        objs.HideButton(UIButtonType.Quit);
        RemoveButtonEvent();
    }

    protected override void AddButtonEvnet()
    {
        buttons.OnResumeClicked += Resume;
        buttons.OnRestartClicked += Restart;
        buttons.OnQuitClicked += Quit;
    }

    protected override void RemoveButtonEvent()
    {
        buttons.OnResumeClicked -= Resume;
        buttons.OnRestartClicked -= Restart;
        buttons.OnQuitClicked -= Quit;
    }

    private void Resume()
    {
        GameManager.Instance.SetGameState(tempState);
        HideUI();
    }

    private void Restart()
    {
        GameManager.Instance.SetGameState(tempState);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Quit()
    {
        GameManager.Instance.SetGameState(GameState.Title);
        SceneManager.LoadScene(0); // 0 -> Main
    }
}