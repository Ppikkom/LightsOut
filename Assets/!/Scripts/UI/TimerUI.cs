using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerUI : BaseUI
{
    private enum TimerType {Start, Main};
    [SerializeField] private TimerType timerType;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private Image timerBar;
    [SerializeField] private float timerSec;

    private Coroutine timerCoroutine;
    public System.Action onTimerEnd;
    private float timer = 0;
    
    public void TimerStart()
    {
        StopTimerCoroutine();
        ResetTimer();
        timerCoroutine = StartCoroutine(StartTimer());
    }

    public void TimerPause()
    {
        if(GameManager.Instance.GameState == GameState.Endless) return;
        StopTimerCoroutine();
    }

    private void StopTimerCoroutine()
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

    private void ResetTimer() => SetTimer(timerSec);

    private void CalculateFillAmount(float value)
    {
        if(timerType == TimerType.Start)
            timerBar.fillAmount = value - Mathf.Floor(value);
        else if(timerType == TimerType.Main)
            timerBar.fillAmount = value / timerSec;
    }

    private void TimerEnd()
    {
        if(timerType == TimerType.Start)
        {
            SoundManager.Instance.PlayBGM(BgmType.Ingame);
            gameObject.SetActive(false);
        }
        else if(timerType == TimerType.Main) { }
        
        if(buttons != null && GameManager.Instance.GameState == GameState.Endless) objs.HideUI();
        
        onTimerEnd?.Invoke();
    }

    private IEnumerator StartTimer()
    {
        int lastSecond = Mathf.CeilToInt(timerSec);

        while(timer < timerSec)
        {
            timer += Time.unscaledDeltaTime;
            float remainTime = Mathf.Max(0f, timerSec - timer);
            SetTimer(remainTime);

            if(timerType == TimerType.Start)
                PlayCountDownSfx(remainTime, ref lastSecond);
            
            yield return null;
        }

        if(timerType == TimerType.Start)
            SoundManager.Instance.PlaySfx(SfxType.CountDownComplete);

        TimerEnd();
    }

    private void PlayCountDownSfx(float remainTime, ref int lastSecond)
    {
        int curruentSecond = Mathf.CeilToInt(remainTime);

        if(curruentSecond < lastSecond && curruentSecond > 0)
        {
            SoundManager.Instance.PlaySfx(SfxType.CountDown);
            lastSecond = curruentSecond;
        }
    }
}
