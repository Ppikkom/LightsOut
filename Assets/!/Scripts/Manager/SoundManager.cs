using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    public static SoundManager Instance
    {
        get
        {
            if(instance == null) return null;
            return instance;
        }
    }

    [Header("BGM")]
    [SerializeField] private AudioSource bgAudioSource;
    [SerializeField] private AudioClip[] bgmList;
    private const string BgVolumeKey = "BgVolume";
    [Range(0, 1)] public float bgVolume;

    [Header("SFX")]
    [SerializeField] private GameObject sfxObject;
    private AudioSource[] _sfxAudioSources;
    [SerializeField] private AudioClip[] sfxList;
    private const string SfxVolumeKey = "SfxVolume";
    [Range(0, 1)] public float sfxVolume;

    public int channelIndex;
    private const string TitleKey = "Title";
    private const string PlayKey = "Play";
    private bool isInitialized = false;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            Initialize();
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void OnDestroy()
    {
        if(instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoadMusic;
            instance = null;
        }
        
    }

    private void Initialize()
    {
        if(isInitialized == true) return;

        _sfxAudioSources = sfxObject.GetComponents<AudioSource>();
        InitVolume();
        SceneManager.sceneLoaded -= OnSceneLoadMusic;
        SceneManager.sceneLoaded += OnSceneLoadMusic;
        isInitialized = true;
    }

    private void InitVolume()
    {
        if (PlayerPrefs.HasKey(BgVolumeKey) == false) // First
        {
            PlayerPrefs.SetFloat(BgVolumeKey, bgVolume);
            PlayerPrefs.SetFloat(SfxVolumeKey, sfxVolume);
        }
        else
        {
            bgVolume = PlayerPrefs.GetFloat(BgVolumeKey);
            sfxVolume = PlayerPrefs.GetFloat(SfxVolumeKey);
        }
        
        bgAudioSource.volume = bgVolume;
        foreach (var v in _sfxAudioSources) v.volume = sfxVolume;

    }

    private void OnSceneLoadMusic(Scene scene, LoadSceneMode mode)
    {
        StopBGM();
        if(scene.name == TitleKey) PlayBGM(BgmType.Title);
        else if(scene.name == PlayKey) PlaySfx(SfxType.CountDown);
    }

    public void PlaySfx(SfxType type, float t = 0) => StartCoroutine(Playsfx(type, t));

    public void PlayBGM(BgmType type)
    {
        bgAudioSource.clip = bgmList[(int)type];
        bgAudioSource.loop = true;
        bgAudioSource.Play();
    }

    public void ResumeBGM() => bgAudioSource.Play();
    
    public void StopBGM()
    {
        if (bgAudioSource == null || bgAudioSource.isPlaying == false) return;
        bgAudioSource.Stop();
    }

    public void SetBGVolume(float f)
    {
        bgVolume = Mathf.Clamp01(f);
        PlayerPrefs.SetFloat(BgVolumeKey, bgVolume);
        bgAudioSource.volume = bgVolume;
    }

    public void SetSFXVolume(float f)
    {
        sfxVolume = Mathf.Clamp01(f);
        PlayerPrefs.SetFloat(SfxVolumeKey, sfxVolume);
        foreach (var v in _sfxAudioSources) v.volume = sfxVolume;
    }

    public void SetMuteBG(bool flag)
    {
        bgAudioSource.mute = flag;
        DataManager.Instance.SetData(DataType.BGMute, System.Convert.ToInt16(flag));
    }
    public void SetMuteSfx(bool flag)
    {
        foreach(var sfx in _sfxAudioSources)
            sfx.mute = flag;
        DataManager.Instance.SetData(DataType.SfxMute, System.Convert.ToInt16(flag));
    }
    
    private IEnumerator Playsfx(SfxType type, float delay)
    {
        if(delay > 0f)
            yield return new WaitForSeconds(delay);

        int index = (int)type;
        
        if(sfxList == null || index < 0 || index >= sfxList.Length) yield break;

        AudioClip clip = sfxList[index];

        if(clip == null) yield break; 
        if(_sfxAudioSources == null || _sfxAudioSources.Length == 0) yield break;

        AudioSource source = _sfxAudioSources[channelIndex];
        source.PlayOneShot(clip);
        channelIndex = (channelIndex + 1) % _sfxAudioSources.Length;
    }
}
