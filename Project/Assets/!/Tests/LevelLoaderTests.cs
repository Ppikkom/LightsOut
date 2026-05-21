using NUnit.Framework;
using UnityEngine;

public class LevelLoaderTests
{
    [Test]
    public void Init_ParsesSingle3x3Level()
    {
        string csv =
            "1,,,,,,\n" +
            "1,0,1,,,,\n" +
            "0,1,0,,,,\n" +
            "1,0,1,,,,\n";

        var loader = new LevelLoader();

        loader.Init(new TextAsset(csv));

        Assert.AreEqual(3, loader.GetBasicLevelFieldSize(0));

        int[][] grid = loader.GetBasicLevelGrid(0);
        Assert.AreEqual(1, grid[0][0]);
        Assert.AreEqual(0, grid[0][1]);
        Assert.AreEqual(1, grid[0][2]);
    }

    [Test]
    public void Init_ParsesMultipleLevels_WithBlankDivider()
    {
        string csv =
            "1,,,,,,\n" +
            "1,0,1,,,,\n" +
            "0,1,0,,,,\n" +
            "1,0,1,,,,\n" +
            ",,,,,,\n" +
            "2,,,,,,\n" +
            "1,1,0,1,1,,\n" +
            "1,0,1,0,1,,\n" +
            "0,1,1,1,0,,\n" +
            "1,0,1,0,1,,\n" +
            "1,1,0,1,1,,\n";

        var loader = new LevelLoader();

        loader.Init(new TextAsset(csv));

        Assert.AreEqual(3, loader.GetBasicLevelFieldSize(0));
        Assert.AreEqual(5, loader.GetBasicLevelFieldSize(1));
    }

    [Test]
    public void Init_EmptyCells_AreParsedAsZero()
    {
        string csv =
            "1,,,,,,\n" +
            "1,,1,,,,\n" +
            ",1,,,,,\n" +
            "1,,1,,,,\n";

        var loader = new LevelLoader();

        loader.Init(new TextAsset(csv));

        int[][] grid = loader.GetBasicLevelGrid(0);

        Assert.AreEqual(1, grid[0][0]);
        Assert.AreEqual(0, grid[0][1]);
        Assert.AreEqual(1, grid[0][2]);
        Assert.AreEqual(0, grid[1][0]);
        Assert.AreEqual(1, grid[1][1]);
    }
}