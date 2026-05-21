using UnityEngine;

public static class LightsOutHelper
{
    public static int[,] CreateRandomBlock(int size)
    {
        int[,] result = new int[size, size];
        for(int i = 0; i < size; i++)
        {
            for(int j = 0; j < size; j++)
            {
                result[i, j] = Random.Range(0, 2);
            }
        }
        return result;
    }

    public static int[,] ConvertTo2dArray(int[][] jaggedArray, int size)
    {
        int[,] result = new int[size, size];
        for(int i = 0; i < size; i++)
        {
            for(int j = 0; j < size; j++)
            {
                result[i, j] = jaggedArray[i][j];
            }
        }
        return result;
    }
}