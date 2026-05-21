using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class BlockEvent : MonoBehaviour, IPointerDownHandler
{

    private Vector2Int _coord;
    private Action<Vector2Int> _onClick;
    private SpriteRenderer _sr;
    [SerializeField] private Sprite activeSprite;
    [SerializeField] private Sprite deactiveSprite;

    public void Init(Vector2Int coord, Action<Vector2Int> onClick)
    {
        _coord = coord;
        _onClick = onClick;
        _sr = GetComponent<SpriteRenderer>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _onClick?.Invoke(_coord);
    }
}