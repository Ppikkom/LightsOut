using UnityEngine;
using UnityEngine.UI;

public class MuteUI : MonoBehaviour
{
    [SerializeField] private SoundType type;
    [SerializeField] private Toggle muteToggle;
    void Start()
    {
        InitToggleSprite();
    }

    void OnEnable()
    {
        muteToggle.onValueChanged.AddListener(ChangeMuteToggle);
    }

    void OnDisable()
    {
        muteToggle.onValueChanged.RemoveListener(ChangeMuteToggle);
    }

    private void InitToggleSprite()
    {
        int isMute = type == SoundType.BG ? DataManager.Instance.GetData(DataType.BGMute) : DataManager.Instance.GetData(DataType.SfxMute);
        muteToggle.isOn = System.Convert.ToBoolean(isMute);
    }

    private void ChangeMuteToggle(bool flag)
    {
        if(type == SoundType.BG) SoundManager.Instance.SetMuteBG(flag);
        else SoundManager.Instance.SetMuteSfx(flag);
    }
}