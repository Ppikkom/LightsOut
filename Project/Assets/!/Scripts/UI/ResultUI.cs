using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Localization.Settings;

public class ResultUI : BaseUI
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;

    public override void ShowUI()
    {
        SetScoreText();
        objs.ShowUI(false);
        objs.SetGridCellSize(2, false);
        objs.ShowButton(UIButtonType.Restart, false);
        objs.ShowButton(UIButtonType.Quit, false);
        AddButtonEvent();
    }

    public override void HideUI()
    {
        objs.HideUI(false);
        objs.HideButton(UIButtonType.Restart);
        objs.HideButton(UIButtonType.Quit);
        RemoveButtonEvent();
        gameObject.SetActive(false);
    }

    public void SetScoreText()
    {
        string scoreLabel = LocalizationSettings.StringDatabase.GetLocalizedString(TableName, "Score");
        string highScoreLabel = LocalizationSettings.StringDatabase.GetLocalizedString(TableName, "HighScore");

        scoreText.text = $"{scoreLabel} : {DataManager.Instance.GetData(DataType.CurScore)}";
        highScoreText.text = $"{highScoreLabel} : {DataManager.Instance.GetData(DataType.HighScore)}";
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
