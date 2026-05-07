using UnityEngine;

[System.Serializable]
public class GameEvent
{
    private GameProcessor _processor;
    [SerializeField] private ScoreUI scoreUI;
    [SerializeField] private ResultUI resultUI;
    [SerializeField] private TimerUI mainTimerUI;
    private int _score = 0;

    public void Init(GameProcessor processor)
    {
        _processor = processor;

        mainTimerUI.onTimerEnd += OnGameOverEvent;
    }

    public void OnGameClearEvent()
    {
        GameState state = GameManager.Instance.GameState;

        Debug.Log(state + " : 클리어 ");

        if(state == GameState.Basic)
        {
            // GameState 바꾸고
            // Clear화면 띄우기
        }
        else if(state == GameState.Endless)
        {
            _score += 1;
            scoreUI.SetScoreText(_score);
            DataManager.Instance.SetData(DataType.CurScore, _score);
            _processor.Effect.HideBlockEffect();
        }
    }

    public void OnGameOverEvent()
    {
        GameState state = GameManager.Instance.GameState;

        // 필요하다면 GameState 바꾸기
        if(state == GameState.Basic)
        {
            // 게임오버 화면 띄우기
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