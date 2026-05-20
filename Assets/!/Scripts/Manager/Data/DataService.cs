using UnityEngine;

public class DataService
{
    private int[] Datas;
    private const int MAXSTAGE = 27;
    public void Init()
    {
        Datas = new int[System.Enum.GetValues(typeof(DataType)).Length];
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
        Datas[(int)type] =  value;
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
        int level = Datas[(int)DataType.SelectLevel] + 1;
        SetData(DataType.SelectLevel, level);
    }

    public void ClearStage()
    {
        int clearStage = Datas[(int)DataType.SelectLevel];
        int lockLevel = GetData(DataType.Lock);

        //Debug.Log(clearStage + " " + lockLevel);

        if(clearStage + 1 != lockLevel || clearStage == MAXSTAGE) return;
        lockLevel += 1;
        //Debug.Log("현재 lock : " + lockLevel);
        SetData(DataType.Lock, lockLevel);
    }

    public bool IsLastStage() => GetData(DataType.SelectLevel) == MAXSTAGE;
}
