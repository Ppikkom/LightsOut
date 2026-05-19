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

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            _sfxSounds = sfxObject.GetComponents<AudioSource>();
            InitVolume();
            SceneManager.sceneLoaded += OnSceneLoadMusic;
        }
        else
            Destroy(gameObject);
    }

    [Header("BGM")]
    [SerializeField] private AudioSource bgAudioSource;
    [SerializeField] private AudioClip[] bgmList;
    [Range(0, 1)] public float bgVolume;

    [Header("SFX")]
    [SerializeField] private GameObject sfxObject;
    private AudioSource[] _sfxSounds;
    [SerializeField] private AudioClip[] sfxList;
    [Range(0, 1)] public float sfxVolume;

    public int channels;
    public int channelIndex;

    private void InitVolume()
    {
        if (PlayerPrefs.HasKey("BgVolume") == false) // First
        {
            PlayerPrefs.SetFloat("BgVolume", bgVolume);
            PlayerPrefs.SetFloat("SfxVolume", sfxVolume);
        }
        else
        {
            bgVolume = PlayerPrefs.GetFloat("BgVolume");
            sfxVolume = PlayerPrefs.GetFloat("SfxVolume");
        }
        
        bgAudioSource.volume = bgVolume;
        foreach (var v in _sfxSounds) v.volume = sfxVolume;
    }

    private void OnSceneLoadMusic(Scene scene, LoadSceneMode mode)
    {
        StopBGM();
        if(scene.name == "SampleScene") PlayBGM(BgmType.Title);
        else if(scene.name == "Play") PlaySfx(SfxType.CountDown);
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
        PlayerPrefs.SetFloat("BgVolume", bgVolume);
        bgAudioSource.volume = bgVolume;
    }

    public void SetSFXVolume(float f)
    {
        sfxVolume = Mathf.Clamp01(f);
        PlayerPrefs.SetFloat("SfxVolume", sfxVolume);
        foreach (var v in _sfxSounds) v.volume = sfxVolume;
    }
    
    private IEnumerator Playsfx(SfxType type, float delay)
    {
        if(delay > 0f)
            yield return new WaitForSeconds(delay);

        int index = (int)type;
        
        if(sfxList == null || index < 0 || index >= sfxList.Length) yield break;

        AudioClip clip = sfxList[index];

        if(clip == null) yield break; 
        if(_sfxSounds == null || _sfxSounds.Length == 0) yield break;

        AudioSource source = _sfxSounds[channelIndex];
        source.PlayOneShot(clip);
        channelIndex = (channelIndex + 1) % _sfxSounds.Length;
    }
}
