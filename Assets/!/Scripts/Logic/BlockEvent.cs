using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class BlockEvent : MonoBehaviour, IPointerDownHandler
{

    private Vector2Int _coord;
    private Action<Vector2Int> _onClick;

    public void Init(Vector2Int coord, Action<Vector2Int> onClick)
    {
        _coord = coord;
        _onClick = onClick;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _onClick?.Invoke(_coord);
    }
}