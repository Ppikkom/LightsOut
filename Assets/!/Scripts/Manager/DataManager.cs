using UnityEngine;

public class DataManager : MonoBehaviour
{
    #region SingleTon
    private static DataManager instance;
    public static DataManager Instance
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
        }
        else
            Destroy(gameObject);

        InitSubClass();
    }
    #endregion

    #region SubClass
    public DataService Data {get; private set;}
    public LevelLoader Level {get; private set;}
    #endregion

    #region Helper
    public int GetData(DataType type) => Data.GetData(type);
    public void SetData(DataType type, int value) => Data.SetData(type, value);
    public int[][] GetBasicLevelField(int level) => Level.GetBasicLevelGrid(level);
    public int GetBasicLevelFieldSize(int level) => Level.GetBasicLevelFieldSize(level);
    
    #endregion

    [SerializeField] private TextAsset basicLevels;

    private void InitSubClass()
    {
        Data = new DataService();
        Level = new LevelLoader();

        Data.Init();
        Level.Init(basicLevels);
    }
}
