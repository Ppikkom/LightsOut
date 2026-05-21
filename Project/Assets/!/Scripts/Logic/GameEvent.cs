using UnityEngine;

[System.Serializable]
public class GameEvent
{
    private GameProcessor _processor;
    [SerializeField] private ScoreUI scoreUI;
    [SerializeField] private ResultUI resultUI;
    [SerializeField] private StageClearUI stageClearUI;
    [SerializeField] private TimerUI mainTimerUI;
    [SerializeField] private GameOverUI gameOverUI;
    private int _score = 0;

    public void Init(GameProcessor processor)
    {
        _processor = processor;

        mainTimerUI.onTimerEnd += OnGameOverEvent;
    }

    public void OnGameClearEvent()
    {
        GameState state = GameManager.Instance.GameState;
        mainTimerUI.TimerPause();

        if(state == GameState.Basic)
        {
            DataManager.Instance.ClearStage();
            stageClearUI.ShowUI();
        }
        else if(state == GameState.Endless)
        {
            _score += 1;
            scoreUI.SetScoreText(_score);            
            _processor.Effect.HideBlockEffect();
            DataManager.Instance.SetData(DataType.CurScore, _score);
            SoundManager.Instance.PlaySfx(SfxType.FadeOut);
        }
    }

    public void OnGameOverEvent()
    {
        GameState state = GameManager.Instance.GameState;

        // 필요하다면 GameState 바꾸기
        if(state == GameState.Basic)
        {
            gameOverUI.ShowUI();
        }
        else if(state == GameState.Endless)
        {
            CheckHighScore();
            resultUI.ShowUI();
        }
    }

    private void CheckHighScore()
    {
        int highScore = DataManager.Instance.GetData(DataType.HighScore);
        if(_score > highScore) DataManager.Instance.SetData(DataType.HighScore, _score);
    }
}