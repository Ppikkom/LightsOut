using Unity.Android.Gradle.Manifest;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageClearUI : BaseUI
{
    [SerializeField] private string text;

    public override void ShowUI()
    {
        objs.ShowUI();
        EditText(text);
        objs.SetGridCellSize(3);
        if(DataManager.Instance.IsLastStage() == false) objs.ShowButton(UIButtonType.Next);
        objs.ShowButton(UIButtonType.Restart);
        objs.ShowButton(UIButtonType.Quit);
        AddButtonEvnet();
    }

    public override void HideUI()
    {
        objs.HideUI();
        objs.HideButton(UIButtonType.Next);
        objs.HideButton(UIButtonType.Restart);
        objs.HideButton(UIButtonType.Quit);
        RemoveButtonEvent();
    }

    protected override void AddButtonEvnet()
    {
        buttons.OnNextClicked += Next;
        buttons.OnRestartClicked += Restart;
        buttons.OnQuitClicked += Quit;
    }

    protected override void RemoveButtonEvent()
    {
        buttons.OnNextClicked -= Next;
        buttons.OnRestartClicked -= Restart;
        buttons.OnQuitClicked -= Quit;
    }

    private void Next()
    {
        DataManager.Instance.NextStage();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Quit()
    {
        GameManager.Instance.SetGameState(GameState.Title);
        SceneManager.LoadScene(0); // 0 -> Main
    }
}