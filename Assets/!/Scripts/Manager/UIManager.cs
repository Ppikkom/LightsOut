using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance
    {
        get
        {
            if(instance == null) return null;
            return instance;
        }
    }

    [SerializeField] private SerializedDictionary<UIType, BaseUI> uiDict;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    public void ShowUI(UIType type)
    {
        uiDict[type].ShowUI();
    }

    public void HideUI(UIType type)
    {
        uiDict[type].HideUI();
    }
}
