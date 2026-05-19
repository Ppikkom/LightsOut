using System.Collections;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class LanguageUI : MonoBehaviour
{
    [SerializeField] private Button[] languageButtons;
    [SerializeField] private Image[] languageImage;
    [SerializeField] private Sprite disableImage;
    [SerializeField] private Sprite enableImage;

    void Start()
    {
        InitLocalize();
        InitButtonEvent();
        ChangeButtonSprite();
    }

    private void InitLocalize()
    {
        if(DataManager.Instance.HasData(DataType.Language))
            ChangeLanguage((LanguageType)DataManager.Instance.GetData(DataType.Language));
        else
            ChangeLanguage(LanguageType.Eng);
    }

    private void InitButtonEvent()
    {
        foreach (LanguageType language in System.Enum.GetValues(typeof(LanguageType)))
        {
            int index = (int)language;
            languageButtons[index].onClick.AddListener(() => ChangeLanguage(language));
        }
    }

    private void ChangeButtonSprite()
    {
        int index = GetCurrentLocaleIndex();
        for(int i = 0; i < languageButtons.Length; i++)
            languageImage[i].sprite = index == i ? enableImage : disableImage;
    }

    private int GetCurrentLocaleIndex()
    {
        Locale currentLocale = LocalizationSettings.SelectedLocale;
        var locales = LocalizationSettings.AvailableLocales.Locales;

        for (int i = 0; i < locales.Count; i++)
        {
            if (locales[i] == currentLocale)
            {
                return i;
            }
        }
        return -1;
    }

    private void ChangeLanguage(LanguageType type)
    {
        int value = (int)type;
        DataManager.Instance.SetData(DataType.Language, value);
        StartCoroutine(ChangeLocalCoroutine(value));
    }

    private IEnumerator ChangeLocalCoroutine(int idx)
    {
        yield return LocalizationSettings.InitializationOperation;

        Locale locale = LocalizationSettings.AvailableLocales.Locales[idx];
        LocalizationSettings.SelectedLocale = locale;
        ChangeButtonSprite();

        Debug.Log($"언어 변경 : {locale.LocaleName}");
    }
}