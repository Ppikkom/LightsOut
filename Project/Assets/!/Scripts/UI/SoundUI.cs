using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class SoundUI : MonoBehaviour
{
    [SerializeField] private SoundType type;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private Slider slider;
    [SerializeField] private Image sliderImage;
    [SerializeField] private Toggle muteToggle;

    void OnEnable()
    {
        Init();
        inputField.onEndEdit.AddListener(OnInputConfirm);
        slider.onValueChanged.AddListener(OnSliderValueChanged);
        muteToggle.onValueChanged.AddListener(ChangeMuteToggle);
    }

    void OnDisable()
    {
        inputField.onEndEdit.RemoveListener(OnInputConfirm);
        slider.onValueChanged.RemoveListener(OnSliderValueChanged);
        muteToggle.onValueChanged.RemoveListener(ChangeMuteToggle);
    }

    private void Init()
    {
        float volume = type == SoundType.BG ? SoundManager.Instance.bgVolume : SoundManager.Instance.sfxVolume;
        ChangeSliderWithoutNotify(volume);

        int percent = Mathf.RoundToInt(volume * 100f);
        ChangeInputWithoutNotify(percent.ToString());

        int isMute = type == SoundType.BG ? DataManager.Instance.GetData(DataType.BGMute) : DataManager.Instance.GetData(DataType.SfxMute);
        muteToggle.isOn = System.Convert.ToBoolean(isMute);
        ChangeMuteToggle(muteToggle.isOn);
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

    private void ChangeMuteToggle(bool flag)
    {
        if(type == SoundType.BG) SoundManager.Instance.SetMuteBG(flag);
        else SoundManager.Instance.SetMuteSfx(flag);
    }


}