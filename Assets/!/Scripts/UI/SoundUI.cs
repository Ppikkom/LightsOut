using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class SoundUI : MonoBehaviour
{
    private enum SoundType {BG, SFX}
    [SerializeField] private SoundType type;

    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private Slider slider;
    [SerializeField] private Image sliderImage;

    void OnEnable()
    {
        Init();
        inputField.onEndEdit.AddListener(OnInputConfirm);
        slider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    void OnDisable()
    {
        inputField.onEndEdit.RemoveListener(OnInputConfirm);
    }

    private void Init()
    {
        float volume = type == SoundType.BG ? SoundManager.Instance.bgVolume : SoundManager.Instance.sfxVolume;
        ChangeSliderWithoutNotify(volume);

        int percent = Mathf.RoundToInt(volume * 100f);
        ChangeInputWithoutNotify(percent.ToString());
    }

    private void SetVolume(float volume)
    {
        volume = Mathf.Clamp01(volume);

        if (type == SoundType.BG)
            SoundManager.Instance.SetBGVolume(volume);
        else
            SoundManager.Instance.SetSFXVolume(volume);
    }

    private void OnInputConfirm(string text)
    {
        if (int.TryParse(text, out int percent) == false)
        {
            ChangeInputWithoutNotify("0");
            SetVolume(0f);
            ChangeSliderWithoutNotify(0f);
            return;
        }

        percent = Mathf.Clamp(percent, 0, 100);
        float volume = percent * 0.01f;

        SetVolume(volume);
        ChangeInputWithoutNotify(percent.ToString());
        ChangeSliderWithoutNotify(volume);
    }

    private void OnSliderValueChanged(float volume)
    {
        SetVolume(volume);
        CheckSliderImageThreshold(volume);

        int percent = (int)(volume * 100);
        ChangeInputWithoutNotify(percent.ToString());
    }

    private void CheckSliderImageThreshold(float volume) => sliderImage.enabled = volume >= 0.01f;

    private void ChangeInputWithoutNotify(string text) => inputField.SetTextWithoutNotify(text);

    private void ChangeSliderWithoutNotify(float volume)
    {
        volume = Mathf.Clamp01(volume);
        CheckSliderImageThreshold(volume);
        slider.SetValueWithoutNotify(volume);
    }


}