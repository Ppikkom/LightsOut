using UnityEngine;
using DG.Tweening;
public class Block
{
    public Vector2Int coord;
    public bool isActive {get; private set;}

    public GameObject obj;
    public Transform trans;
    public SpriteRenderer sr;
    
    
    private Color activeColor;
    private Color disabledColor; 

    private BlockEvent blockEvent;
    private Vector3 pos;
    public Block(GameObject prefab, Vector2Int coord)
    {
        CreateBlock(prefab);
        this.coord = coord;
    }

    public void SetActiveFlag(bool flag)
    {
        isActive = flag;
    }

    public void SetPosition(int fieldSize, Vector2 blockSpacing, float blockSize)
    {
        Vector2Int v = coord - Vector2Int.one * (fieldSize / 2);
        pos = BlockCoordHelper.GridToWorld(coord, blockSpacing, blockSize, fieldSize);
        trans.position = pos;
    }

    private void CreateBlock(GameObject prefab)
    {
        obj = Object.Instantiate(prefab, Vector3.zero, Quaternion.identity);
        obj.name = $"Block{coord}";
        obj.SetActive(false);
    }

    public void SetTransform(Transform parent, float blockSize)
    {
        trans = obj.GetComponent<Transform>();
        trans.SetParent(parent);
        trans.localScale = Vector3.one * blockSize;
    }

    public void SetSpriteRenderer(Color aColor, Color dColor)
    {
        sr = obj.GetComponent<SpriteRenderer>();
        sr.color = isActive ? aColor : dColor;
        activeColor = aColor;
        disabledColor = dColor;
    }

    public Tween BlockDown(float duration)
    {
        trans.position = pos + Vector3.up * 0.5f;
        return trans.DOMove(pos, duration);
    }

    public void AddBlockClickEvent(System.Action<Vector2Int> onClick)
    {
        blockEvent = obj.GetComponent<BlockEvent>();
        blockEvent.Init(coord, onClick);
    }

    public void OnBlockClicked()
    {
        isActive = !isActive;
        sr.color = isActive ? activeColor : disabledColor;
    }
}