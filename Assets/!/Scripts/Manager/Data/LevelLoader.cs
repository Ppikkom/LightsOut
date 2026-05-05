using System.Collections.Generic;
using UnityEngine;

public struct LevelData
{
    public int fieldSize;
    public int[][] grid;
    public LevelData(int size, int[][] grid)
    {
        fieldSize = size;
        this.grid = grid;
    }
}

public class LevelLoader
{
    //private List<int[][]> levelDatas = new List<int[][]>();
    private List<LevelData> levelDatas = new List<LevelData>();

    public void Init(TextAsset text)
    {
        levelDatas = ParseCSV(text);
    }

    public int[][] GetBasicLevelGrid(int level)
    {
        return levelDatas[level].grid;
    }

    public int GetBasicLevelFieldSize(int level)
    {
        return levelDatas[level].fieldSize;
    }

    private List<LevelData> ParseCSV(TextAsset levels)
    {
        List<LevelData> blocks = new List<LevelData>();

        string[] lines = levels.text.Split("\n");

        List<int[]> tempRows = new List<int[]>();

        foreach(var line in lines)
        {
            string trim = line.Trim();

            if(TryDivideBlock(trim, tempRows, blocks)) continue;

            if(!trim.Contains(",")) continue;

            string[] tokens = trim.Split(",");

            if(IsLevelIdentifier(tokens)) continue;
            
            int[] row = ParseRowData(tokens);

            tempRows.Add(row);
        }

        if(tempRows.Count > 0)
            AddBlockToLevels(tempRows, blocks);

        return blocks;
    }

    private bool TryDivideBlock(string trim, List<int[]> tempRows, List<LevelData> blocks)
    {
        if(string.IsNullOrEmpty(trim.Replace(",", "")))
        {
            if(tempRows.Count > 0)
            {
                AddBlockToLevels(tempRows, blocks);
            }
            return true;
        }
        return false;
    }

    private void AddBlockToLevels(List<int[]> tempRows, List<LevelData> blocks)
    {
        int size = tempRows.Count;

        int[][] cleanGrid = new int[size][];

        for(int i = 0; i < size; i++)
        {
            cleanGrid[i] = new int[size];
            for(int j = 0; j < size; j++)
            {
                cleanGrid[i][j] = tempRows[i][j];
            }
        }
        LevelData newLevel = new LevelData(size, cleanGrid);
        blocks.Add(newLevel);
        tempRows.Clear();
    }

    private bool IsLevelIdentifier(string[] tokens)
    {
        if (string.IsNullOrEmpty(tokens[0].Trim()) == false)
        {
            for(int i = 1; i < tokens.Length; i++)
            {
                if (string.IsNullOrEmpty(tokens[i].Trim()) == false)
                    return false;
            }
            return true;
        }
        return false;        
    }

    private int[] ParseRowData(string[] tokens)
    {
        int[] row = new int[tokens.Length];
        
        for(int i = 0; i < tokens.Length; i++)
        {
            string token = tokens[i].Trim();

            if(string.IsNullOrEmpty(token))
                row[i] = 0;
            else
                row[i] = int.Parse(token);
        }
        return row;
    }
}
