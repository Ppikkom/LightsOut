using UnityEngine;

public class DataService
{
    private int[] Datas;
    public void Init()
    {
        Datas = new int[System.Enum.GetValues(typeof(DataType)).Length];
    }

    public void SetData(DataType type, int value)
    {
        Datas[(int)type] =  value;
        PlayerPrefs.SetInt(type.ToString(), value);
    }

    public int GetData(DataType type)
    {
        if (PlayerPrefs.HasKey(type.ToString()))
            return PlayerPrefs.GetInt(type.ToString());
        
        Debug.LogWarning("Not Found Key");
        return default;
    }
}
