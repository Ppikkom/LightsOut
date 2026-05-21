using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    [SerializeField] private Button[] basicLevels;
    [SerializeField] private Image[] images;
    [SerializeField] private Button endlessButton;
    [SerializeField] private TextMeshProUGUI[] texts;
    [SerializeField] private Sprite locked;


    void Start()
    {
        AddEndlessButtonEvent();
        EditBasicLevelButton();
    }

    private void EditBasicLevelButton()
    {
        for(int i = 0; i < basicLevels.Length; i++)
        {
            int value = i + 1;
            basicLevels[i].onClick.AddListener(() => OnBasicLevelButtonClick(value));
            ChangeLockStageSprite(value);
        }
    }

    private void ChangeLockStageSprite(int idx)
    {
        int lockStage = DataManager.Instance.GetData(DataType.Lock);

        if(idx < lockStage) // UnLock
        {
            texts[idx - 1].text = $"{idx}";
            basicLevels[idx - 1].interactable = true;
        }
        else // Lock
        {
            texts[idx - 1].text = $"";
            images[idx - 1].sprite = locked;
            basicLevels[idx - 1].interactable = false;
        }
        
    }

    private void AddEndlessButtonEvent()
    {
        endlessButton.onClick.AddListener(OnEndlessButtonClick);
    }

    private void OnBasicLevelButtonClick(int idx)
    {
        GameManager.Instance.SetGameState(GameState.Basic);
        DataManager.Instance.SetData(DataType.SelectLevel, idx);
        SceneManager.LoadScene(1);
    } 

    private void OnEndlessButtonClick()
    {
        GameManager.Instance.SetGameState(GameState.Endless);
        SceneManager.LoadScene(1);
    }
}
