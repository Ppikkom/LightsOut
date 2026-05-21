using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if(instance == null) return null;
            return instance;
        }
    }

    public GameState GameState { get; private set;}

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
            

        Application.runInBackground = true;
        GameState = GameState.Title;
    }

    public void SetGameState(GameState state) => GameState = state;
}
