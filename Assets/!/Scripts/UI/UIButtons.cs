using System;
using UnityEngine;
using UnityEngine.UI;

public class UIButtons : MonoBehaviour
{
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button nextButton;

    public Action OnPauseClicked;
    public Action OnResumeClicked;
    public Action OnRestartClicked;
    public Action OnQuitClicked;
    public Action OnNextClicked;

    void OnEnable()
    {
        if(pauseButton != null) pauseButton.onClick.AddListener(PauseButtonClick);
        if(resumeButton != null) resumeButton.onClick.AddListener(ResumeButtonClick);
        if(restartButton != null) restartButton.onClick.AddListener(RestartButtonClick);
        if(quitButton != null) quitButton.onClick.AddListener(QuitButtonClick);
        if(nextButton != null) nextButton.onClick.AddListener(NextButtonClick);
    }

    void OnDisable()
    {
        if(pauseButton != null) pauseButton.onClick.RemoveListener(PauseButtonClick);
        if(resumeButton != null) resumeButton.onClick.RemoveListener(ResumeButtonClick);
        if(restartButton != null) restartButton.onClick.RemoveListener(RestartButtonClick);
        if(quitButton != null) quitButton.onClick.RemoveListener(QuitButtonClick);
        if(nextButton != null) nextButton.onClick.RemoveListener(NextButtonClick);
    }

    private void PauseButtonClick() => OnPauseClicked?.Invoke();
    private void ResumeButtonClick() => OnResumeClicked?.Invoke();
    private void RestartButtonClick() => OnRestartClicked?.Invoke();
    private void QuitButtonClick() => OnQuitClicked?.Invoke();
    private void NextButtonClick() => OnNextClicked?.Invoke();
}