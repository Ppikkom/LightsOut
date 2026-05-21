
using NUnit.Framework;
using UnityEngine;

public class DataServiceTests
{
    [SetUp]
    public void SetUp()
    {
        PlayerPrefs.DeleteAll();
    }

    [TearDown]
    public void TearDown()
    {
        PlayerPrefs.DeleteAll();
    }

    [Test]
    public void SetData_StoresValueInPlayerPrefs()
    {
        var dataService = new DataService();
        dataService.Init();

        dataService.SetData(DataType.HighScore, 10);

        Assert.IsTrue(PlayerPrefs.HasKey(DataType.HighScore.ToString()));
        Assert.AreEqual(10, PlayerPrefs.GetInt(DataType.HighScore.ToString()));
    }

    [Test]
    public void GetData_ReturnsSavedValue()
    {
        var dataService = new DataService();
        dataService.Init();

        dataService.SetData(DataType.SelectLevel, 5);

        int result = dataService.GetData(DataType.SelectLevel);

        Assert.AreEqual(5, result);
    }

    [Test]
    public void ClearStage_WhenCurrentStageIsUnlockedNextStage_UnlocksNextLevel()
    {
        var dataService = new DataService();
        dataService.Init();

        dataService.SetData(DataType.SelectLevel, 1);
        dataService.SetData(DataType.Lock, 2);

        dataService.ClearStage();

        Assert.AreEqual(3, dataService.GetData(DataType.Lock));
    }

    [Test]
    public void ClearStage_WhenAlreadyLastStage_DoesNotUnlockBeyondMaxStage()
    {
        var dataService = new DataService();
        dataService.Init();

        dataService.SetData(DataType.SelectLevel, 27);
        dataService.SetData(DataType.Lock, 27);

        dataService.ClearStage();

        Assert.AreEqual(27, dataService.GetData(DataType.Lock));
    }

    [Test]
    public void NextStage_AfterReload_ShouldUsePersistedSelectLevel()
    {
        var first = new DataService();
        first.Init();
        first.SetData(DataType.SelectLevel, 3);

        var reloaded = new DataService();
        reloaded.Init();

        reloaded.NextStage();

        Assert.AreEqual(4, PlayerPrefs.GetInt(DataType.SelectLevel.ToString()));
    }
}