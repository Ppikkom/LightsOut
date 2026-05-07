using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerUI : BaseUI
{
    private enum TimerType {Start, InGame};
    [SerializeField] private TimerType timerType;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private Image timerBar;
    [SerializeField] private float timerSec;

    private Coroutine timerCoroutine;
    public System.Action onTimerEnd;
    private float timer = 0;
    
    public void TimerStart() => timerCoroutine = StartCoroutine(Timer());

    public void TimerPause()
    {
        if(timerCoroutine == null) return;
        
        StopCoroutine(timerCoroutine);
        timerCoroutine = null;
    }

    public void TimerResume() => TimerStart();
    
    private void SetTimer(float value)
    {
        timerText.text = $"{(int)value}";
        CalculateFillAmount(value);
    }

    private void CalculateFillAmount(float value)
    {
        if(timerType == TimerType.Start)
            timerBar.fillAmount = value - Mathf.Floor(value);
        else if(timerType == TimerType.InGame)
            timerBar.fillAmount = value / timerSec;
    }

    private void TimerEnd()
    {
        if(timerType == TimerType.Start)
        {
            gameObject.SetActive(false);
        }
        else if(timerType == TimerType.InGame)
        {
            Debug.Log("종료");
        }
        onTimerEnd?.Invoke();
    }

    private IEnumerator Timer()
    {
        GameState state = GameManager.Instance.GameState;
        while(timer < timerSec)
        {
            if(state == GameState.Basic)
                timer += Time.deltaTime;
            else if(state == GameState.Endless)
                timer += Time.unscaledDeltaTime;

            SetTimer(timerSec - timer);
            yield return null;    
        }    
        TimerEnd();   
    }
}
