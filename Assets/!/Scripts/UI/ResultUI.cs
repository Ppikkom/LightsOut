using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

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
        AddButtonEvnet();
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
        scoreText.text = $"Score : {DataManager.Instance.GetData(DataType.CurScore)}";
        highScoreText.text = $"HighScore : {DataManager.Instance.GetData(DataType.HighScore)}";
    }

    protected override void AddButtonEvnet()
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
        HideUI();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Quit()
    {
        HideUI();
        GameManager.Instance.SetGameState(GameState.Title);
        SceneManager.LoadScene(0); // 0 -> Main
    }


}
