using UnityEngine;
using DG.Tweening;

public class Shrink : BaseUIEffect
{
    [SerializeField] private CanvasGroup canvasGroup;

    [SerializeField] private float sizeUpOffset;
    [SerializeField] private float sizeUpDuration;
    [SerializeField] private float shrinkDuration;

    public override void OnPlay()
    {
        if(IsTweenRunning() == true) return;
        if(rect == null) return;

        sequence = DOTween.Sequence();
        sequence.Append(rect.DOScale(rect.localScale + rect.localScale * sizeUpOffset, sizeUpDuration))
        .Append(rect.DOScale(Vector3.zero, shrinkDuration))
        .Join(canvasGroup.DOFade(0, shrinkDuration))
        .OnComplete(() => rect.gameObject.SetActive(false));
    }
}
