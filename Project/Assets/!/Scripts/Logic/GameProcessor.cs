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
        field = new Dictionary<Vector2Int, Block>();

        DataManager.Instance.SetData(DataType.CurScore, 0);
        
        _creator.Init(this);
        _event.Init(this);
        _effect.Init(this);
    }

    private void StartGame()
    {
        _effect.BlockDownEffect();
    }

    private void FinishGame()
    {
        
    }
}