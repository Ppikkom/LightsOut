using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : BaseUI
{
    [SerializeField] private string text;

    public override void ShowUI()
    {
        objs.ShowUI();
        EditText(text);
        objs.SetGridCellSize(2);
        objs.ShowButton(UIButtonType.Restart);
        objs.ShowButton(UIButtonType.Quit);
        AddButtonEvent();
    }

    public override void HideUI()
    {
        objs.HideUI();
        objs.HideButton(UIButtonType.Restart);
        objs.HideButton(UIButtonType.Quit);
        RemoveButtonEvent();
    }

    protected override void AddButtonEvent()
    {
        buttons.OnRestartClicked += Restart;
        buttons.OnQuitClicked += Quit;
    }

    protected override void RemoveButtonEvent()
    {
        buttons.OnRestartClicked -= Restart;
        buttons.OnQuitClicked -= Quit;
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