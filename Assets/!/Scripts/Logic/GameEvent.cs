using UnityEngine;

[System.Serializable]
public class GameEvent
{
    private GameProcessor _processor;
    [SerializeField] private ScoreUI _scoreUI;
    private int _score = 0;

    public GameEvent(GameProcessor processor)
    {
        _processor = processor;
    }

    public void Init()
    {
        
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
            _scoreUI.SetScoreText(++_score);
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
            // 게임종료 화면 띄우기
        }
    }
}