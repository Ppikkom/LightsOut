using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LoadingUI : MonoBehaviour
{
    private const float iAngle = 360f;
    [SerializeField] private Image[] loadingImage;
    [SerializeField] private RectTransform[] loadingRect;
    [SerializeField] private Color loadingMidColor;
    [SerializeField] private Color loadingEndColor;

    [SerializeField] private float defaultRadius;
    [SerializeField] private float duration;

    private Sequence moveSequnce;
    private Sequence resetSequnce;
    private Sequence playSequence;

    [SerializeField] private int currentCount;

    void Start()
    {
        currentCount = 3;
        OnPlay(currentCount);
    }

    private void OnPlay(int count)
    {
        ActiveLoadingCircleObject(count);

        Sequence expandSequence = CreateExpandSequence(count);
        Sequence resetSequence = CreateResetSequence(count);

        playSequence = DOTween.Sequence();
        playSequence.Append(expandSequence);
        playSequence.Append(resetSequence);
        playSequence.OnComplete(PlayNextStep);
    }

    private Sequence CreateExpandSequence(int count)
    {
        Sequence sequence = DOTween.Sequence();
        Sequence midSequence = DOTween.Sequence();
        Sequence endSequence = DOTween.Sequence();
        for(int i = 0; i < count; i++)
        {
            float angle = iAngle / count * i;
            float angle2 = angle + (iAngle / count / 2);

            Vector2 mid = PolarToCartesianCoord(defaultRadius / 2, angle2);
            Vector2 end = PolarToCartesianCoord(defaultRadius, angle);

            midSequence.Join(loadingImage[i].DOColor(loadingMidColor, duration * 0.5f));
            midSequence.Join(loadingRect[i].DOAnchorPos(mid, duration * 0.5f)); //.SetEase(Ease.OutQuad);

            endSequence.Join(loadingImage[i].DOColor(loadingEndColor, duration * 0.5f));
            endSequence.Join(loadingRect[i].DOAnchorPos(end, duration * 0.5f)); //.SetEase(Ease.InQuad);
            
        }

        sequence.Append(midSequence);
        sequence.Append(endSequence);

        return sequence;
    }

    private Sequence CreateResetSequence(int count)
    {
        Sequence sequence = DOTween.Sequence();
        for(int i = 0; i < count; i++)
        {
            sequence.Join(loadingRect[i].DOAnchorPos(Vector2.zero, duration));
            sequence.Join(loadingImage[i].DOFade(1f, duration));
            sequence.Join(loadingImage[i].DOColor(Color.white, duration));
        }
        return sequence;
    }

    private void ActiveLoadingCircleObject(int value)
    {
        for(int i = 0; i < loadingRect.Length; i++)
            loadingRect[i].gameObject.SetActive(i < value);
    }

    private void PlayNextStep()
    {
        currentCount = (++currentCount % 3) + 3; // 3 4 5
        OnPlay(currentCount);
    }

    private Vector2 PolarToCartesianCoord(float radius, float angle)
    {
        float rad = angle * Mathf.Deg2Rad;

        float x = radius * Mathf.Cos(rad);
        float y = radius * Mathf.Sin(rad);

        return new Vector2(y, x);
    }
}
