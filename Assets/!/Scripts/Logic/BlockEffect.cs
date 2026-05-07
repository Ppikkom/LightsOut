using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
[System.Serializable]
public class BlockEffect
{
    private GameProcessor _processor;

    [SerializeField] private float blockDownDuration;

    private Sequence sequence;

    public void Init(GameProcessor processor)
    {
        _processor = processor;
    }

    public void BlockDownEffect()
    {
        int squareFieldSize = _processor.Creator.FieldSize * _processor.Creator.FieldSize;
        sequence = DOTween.Sequence();
        
        foreach(var v in _processor.field)
        {
            sequence.AppendCallback(() => v.Value.obj.SetActive(true));
            sequence.Append(v.Value.BlockDown(blockDownDuration / squareFieldSize));
        }
    }

    public void HideBlockEffect()
    {
        sequence = DOTween.Sequence();
        Transform trans = _processor.transform;
        sequence.Append(trans.DOScale(Vector3.zero, 1f))
        .OnComplete(() => { 
            trans.gameObject.SetActive(false);
            trans.localScale = Vector3.one;
            ChangeField();
        });
    }

    public void ShowBlockEffect()
    {
        Transform trans = _processor.transform;
        trans.localScale = Vector3.zero;
        sequence = DOTween.Sequence();
        
        sequence.AppendCallback(() => trans.gameObject.SetActive(true))
        .Append(trans.DOScale(Vector3.one, 1f));
    }

    private void ChangeField()
    {
        _processor.Creator.RebuildField();
        foreach(var v in _processor.field)
            v.Value.obj.SetActive(true);
        Debug.Log("Count Tile" + _processor.field.Count);
    }
}