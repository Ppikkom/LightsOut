using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections;

public class SizeUp : BaseUIEffect
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float duration;

    [Header("If has..")]
    [SerializeField] private SwipeUI swipeUI;

    public override void OnPlay()
    {
        if(IsTweenRunning() == true) return;
        if(rect == null || rect.gameObject.activeSelf == true) return;
  
        StartCoroutine(UIOpen());
        
        //sequence = DOTween.Sequence();
        //sequence.Join(rect.DOScale(Vector3.one, duration))
        //.Join(canvasGroup.DOFade(1, duration));
    }

    private IEnumerator UIOpen()
    {
        rect.gameObject.SetActive(true);
        yield return null;
        Canvas.ForceUpdateCanvases();
        LayoutRebuilder.ForceRebuildLayoutImmediate(rect);

        sequence = DOTween.Sequence();
        sequence.JoinCallback(IfHasSwipeUIComponent)
        .Join(rect.DOScale(Vector3.one, duration))
        .Join(canvasGroup.DOFade(1, duration));
        //.OnComplete(IfHasSwipeUIComponent);
    }

    private void IfHasSwipeUIComponent()
    {
        if(swipeUI == null) return;
        swipeUI.SetScrollBarValue(0);
    }
}
