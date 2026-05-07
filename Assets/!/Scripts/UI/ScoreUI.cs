using UnityEngine;
using TMPro;

public class ScoreUI : BaseUI
{
    [SerializeField] private TextMeshProUGUI scoreText;

    void Start()
    {
        CheckGameState();
    }

    public override void ShowUI()
    {
        base.ShowUI();
    }

    public override void HideUI()
    {
        base.HideUI();
    }
    private void CheckGameState()
    {
        GameState state = GameManager.Instance.GameState;
        Debug.Log(state);
        if(state == GameState.Basic)
            gameObject.SetActive(false);
        else if(state == GameState.Endless)
        {
            gameObject.SetActive(true);
            SetScoreText(0);
        }
    }

    public void SetScoreText(int score)
    {
        scoreText.text = $"Score : {score}";
    }
}
