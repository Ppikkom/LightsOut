using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class BaseUIEffect : MonoBehaviour
{
    [SerializeField] protected Button button;
    [SerializeField] protected RectTransform rect;

    protected Sequence sequence;

    public virtual void OnPlay() { }

    protected bool IsTweenRunning() => sequence != null && sequence.IsActive() && sequence.IsPlaying();

    void Start()
    {
        button.onClick.AddListener(OnPlay);
    }

    void OnDestroy()
    {
        button.onClick.RemoveAllListeners();
    }
}
