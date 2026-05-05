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

    [SerializeField] private SerializedDictionary<UIType, GameObject> uiDict;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);

        uiDict = new SerializedDictionary<UIType, GameObject>();
    }

    public void ShowUI(UIType type)
    {
        uiDict[type].SetActive(true);
    }

    public void HideUI(UIType type)
    {
        uiDict[type].SetActive(false);
    }
}
