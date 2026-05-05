using System.Collections.Generic;
using UnityEngine;

public class GameProcessor : MonoBehaviour
{
    public Dictionary<Vector2Int, Block> field {get; private set;}
 
    [SerializeField] private GameEvent _event;
    [SerializeField] private BlockCreator _creator;
    [SerializeField] private BlockEffect _effect;

    public GameEvent Event => _event;
    public BlockCreator Creator => _creator;
    public BlockEffect Effect => _effect;


    void Start()
    {
        InitializeGame();
        StartGame();
    }


    private void InitializeGame()
    {
        _event = new GameEvent(this);
        _creator = new BlockCreator(this);
        _effect = new BlockEffect(this);

        field = new Dictionary<Vector2Int, Block>();

        DataManager.Instance.SetData(DataType.CurScore, 0);
        Creator.Init();
        Event.Init();
    }

    private void StartGame()
    {
        Effect.BlockDownEffect();
    }

    private void FinishGame()
    {
        
    }
}