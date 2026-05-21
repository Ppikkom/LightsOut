using UnityEngine;

public class DataService
{
    private int[] dataCache;
    private const int MAXSTAGE = 27;
    public void Init()
    {
        dataCache = new int[System.Enum.GetValues(typeof(DataType)).Length];
        #if UNITY_ANDROID && !UNITY_EDITOR
        if(HasData(DataType.Lock) == false) SetData(DataType.Lock, 2);
        if(HasData(DataType.BGMute) == false) SetData(DataType.BGMute, 0);
        if(HasData(DataType.SfxMute) == false) SetData(DataType.SfxMute, 0);
        #elif UNITY_EDITOR
        SetData(DataType.Lock, MAXSTAGE);
        #endif
    }

    public void SetData(DataType type, int value)
    {
        dataCache[(int)type] =  value;
        PlayerPrefs.SetInt(type.ToString(), value);
    }

    public int GetData(DataType type)
    {
        if (HasData(type) == true)
            return PlayerPrefs.GetInt(type.ToString());
        
        Debug.LogWarning("Not Found Key");
        return default;
    }

    public bool HasData(DataType type) => PlayerPrefs.HasKey(type.ToString());

    public void NextStage()
    {
        int level = GetData(DataType.SelectLevel) + 1;
        SetData(DataType.SelectLevel, level);
    }

    public void ClearStage()
    {
        int clearStage = GetData(DataType.SelectLevel);
        int lockLevel = GetData(DataType.Lock);

        if(clearStage + 1 != lockLevel || clearStage == MAXSTAGE) return;
        SetData(DataType.Lock, lockLevel + 1);
    }

    public bool IsLastStage() => GetData(DataType.SelectLevel) == MAXSTAGE;
}
