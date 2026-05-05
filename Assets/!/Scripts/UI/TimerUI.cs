using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private Image timerBar;
    private System.Action onTimerEnd;

    private enum TimerType {Start, InGame};
    [SerializeField] private TimerType timerType;

    [SerializeField] private float timerSec;
    private float timer = 0;

    private Coroutine timerCoroutine;

    void Start()
    {
        
    }

    public void TimerStart()
    {
        
        if(timerType == TimerType.Start)
        {
            //GameManager.Instance.SetGameState(GameState.Pause);
        }
        else if(timerType == TimerType.InGame)
        {
            //GameManager.Instance.SetGameState(GameState.Level);
        }
        
        timerCoroutine = StartCoroutine(Timer());
    }

    public void TimerPause()
    {
        StopCoroutine(timerCoroutine);
        timerCoroutine = null;

        GameManager.Instance.SetGameState(GameState.Pause);
    }

    public void TimerResume()
    {
        TimerStart();
    }

    public void AddTimerEndAction(System.Action action)
    {
        onTimerEnd += action;
    }
    
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
            //GameManager.Instance.SetGameState(GameState.Basic);
            gameObject.SetActive(false);
        }
        else if(timerType == TimerType.InGame)
        {
            //GameManager.Instance.SetGameState(GameState.Pause);
            Debug.Log("종료");
        }
        onTimerEnd?.Invoke();
    }

    private IEnumerator Timer()
    {
        while(timer < timerSec)
        {
            timer += Time.deltaTime;
            SetTimer(timerSec - timer);
            yield return null;    
        }    
        TimerEnd();   
    }
}
