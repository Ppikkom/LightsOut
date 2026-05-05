using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    [SerializeField] private Button[] basicLevels;
    [SerializeField] private Button endlessButton;
    [SerializeField] private TextMeshProUGUI[] texts;

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
            texts[i].text = $"{value}";
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
