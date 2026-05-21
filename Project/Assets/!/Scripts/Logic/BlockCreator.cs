using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlockCreator
{
    private GameProcessor _processor;

    [SerializeField] private GameObject blockPrefab;
    [SerializeField] private Vector2 blockSpacing;
    [SerializeField] private Vector3 blockOffset;
    [SerializeField] private Sprite activeSprite;
    [SerializeField] private Sprite disabledSprite;
    [SerializeField] private float blockSize;
    
    private int[,] field;
    private Dictionary<Vector2Int, Block> blocks;
    public int FieldSize {get; private set;}
    private readonly Vector2Int[] direction =
    {
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left,
        Vector2Int.up
    };


    public void Init(GameProcessor processor)
    {
        _processor = processor;
        blocks = new Dictionary<Vector2Int, Block>();

        GetField();
        SetCameraSize();
        SpiralAlgorithm();
    }

    public void RebuildField()
    {
        field = default;
        
        ClearField();
        GetField();
        SetCameraSize();
        SpiralAlgorithm();

        _processor.Effect.ShowBlockEffect();
        SoundManager.Instance.PlaySfx(SfxType.FadeIn);
    }

    private void GetField()
    {
        GameState state = GameManager.Instance.GameState;
        if(state == GameState.Basic)
            field = GetBasicLevelBlock();
        else if(state == GameState.Endless)
            field = CreateRandomBlock();
        else
        {
            Debug.LogError($"{state} 유효하지 않음.");
            return;
        }
    }

    private void SetCameraSize()
    {
        switch (FieldSize)
        {
            case 3:
                Camera.main.orthographicSize = 4f;
                break;
            case 5:
                Camera.main.orthographicSize = 5.5f;
                break;
            case 7:
                Camera.main.orthographicSize = 8f;
                break;
            default:
                Camera.main.orthographicSize = 10f;
                Debug.LogError("FieldSize Error");
                break;
        }
    }

    private int[,] CreateRandomBlock()
    {
        FieldSize = Random.Range(0, 2) * 2 + 3;

        int[,] field = default;
        do
        {
            field = LightsOutHelper.CreateRandomBlock(FieldSize);
        }while(LightsOutSolver.IsSolvable(field) == false || IsAllActiveBlock(field) == true);
        
        return field;
    }

    private bool IsAllActiveBlock(int[,] field)
    {
        for(int i = 0; i < FieldSize; i++)
            for(int j = 0; j < FieldSize; j++)
                if(field[i,j] == 0) return false;
        return true;
    }

    private int[,] GetBasicLevelBlock()
    {
        int level = DataManager.Instance.GetData(DataType.SelectLevel);
        FieldSize = DataManager.Instance.GetBasicLevelFieldSize(level - 1);

        int[][] field = DataManager.Instance.GetBasicLevelField(level - 1);
        return LightsOutHelper.ConvertTo2dArray(field, FieldSize);
    }

    private void SpiralAlgorithm()
    {
        Vector2Int pos = new Vector2Int( FieldSize / 2, FieldSize / 2);
        
        int stepLength = 1;
        int directionIndex = 0;

        int count = 1;
        CreateBlock(pos);

        while (count < FieldSize * FieldSize)
        {
            for(int repeat = 0; repeat < 2; repeat++)
            {
                for(int i = 0; i < stepLength; i++)
                {
                    pos += direction[directionIndex];
                    if(IsBound(pos) == true) return;

                    CreateBlock(pos);
                    count++;
                }
                directionIndex = (directionIndex + 1) % direction.Length;
            }
            stepLength++;
        }
    }

    private void CreateBlock(Vector2Int coord)
    {
        Block block;

        if(blocks.TryGetValue(coord, out Block b) == true)
        {
            block = b;
            Debug.Log(block.coord);
        }
        else
        {
            block = new Block(blockPrefab, coord);
            blocks.Add(coord, block);
            block.AddBlockClickEvent(OnClickBlock);
        }

        
        block.SetActiveFlag(field[coord.y, coord.x] == 1); // block.SetActiveFlag(field[coord.x, coord.y] == 1);
        block.SetTransform(_processor.transform, blockSize);
        block.SetPosition(FieldSize, blockSpacing, blockSize, blockOffset);
        block.SetSpriteRenderer(activeSprite, disabledSprite);
        
        _processor.field.Add(coord, block);
    }

    private bool IsBound(Vector2Int coord)
    {
        if(coord.x < 0 || coord.x == FieldSize || coord.y < 0 || coord.y == FieldSize) return true;
        return false;
    }

    private void OnClickBlock(Vector2Int coord)
    {
        
        AddToggle(coord.x, coord.y);
        AddToggle(coord.x + 1, coord.y);
        AddToggle(coord.x - 1, coord.y);
        AddToggle(coord.x, coord.y + 1);
        AddToggle(coord.x, coord.y - 1);

        PlaySfxSound(coord);

        if(AlreadyPuzzleClear() == true)
            _processor.Event.OnGameClearEvent();
    }

    private void PlaySfxSound(Vector2Int coord)
    {
        bool isActive = _processor.field[coord].isActive;
        if(isActive == true)
            SoundManager.Instance.PlaySfx(SfxType.SwitchOn);
        else
            SoundManager.Instance.PlaySfx(SfxType.SwitchOff);
    }

    private void AddToggle(int x, int y)
    {
        Vector2Int coord =new Vector2Int(x, y);
        if(IsBound(coord) == true) return;
        _processor.field[coord].OnBlockClicked();
    }

    private bool AlreadyPuzzleClear()
    {
        foreach(var v in _processor.field)
            if(v.Value.isActive == true) return false;
        
        return true;
    }

    private void ClearField()
    {
        foreach(var v in _processor.field)
            v.Value.obj.SetActive(false);
        _processor.field.Clear();
    }
}
