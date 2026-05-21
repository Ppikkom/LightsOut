using UnityEngine;

public static class LightsOutSolver
{
    private static int matrixSize;
    private static int cMatrixSize; // coefficientMatrixSize

    public static bool IsSolvable(int[,] board)
    {
        matrixSize = board.GetLength(0);
        bool[,] A = BuildCoefficientMatrix(matrixSize);
        bool[] b = BuildStateVector(board);
        return HasSolution(A, b);
    }

    private static bool[,] BuildCoefficientMatrix(int size)
    {
        cMatrixSize = size * size;
        bool[,] matrix = new bool[cMatrixSize, cMatrixSize];

        for(int i = 0; i < cMatrixSize; i++)
        {
            int dx = i % size; int dy = i / size;
            AddToggle(matrix, i, dx, dy);
            AddToggle(matrix, i, dx - 1, dy);
            AddToggle(matrix, i, dx + 1, dy);
            AddToggle(matrix, i, dx, dy - 1);
            AddToggle(matrix, i, dx, dy + 1);
        }
        return matrix;
    }

    private static bool[] BuildStateVector(int[,] board)
    {
        bool[] b = new bool[cMatrixSize];

        for (int y = 0; y < matrixSize; y++)
        {
            for (int x = 0; x < matrixSize; x++)
            {
                int index = y * matrixSize + x;
                b[index] = board[y, x] == 1 ? true : false;
            }
        }

        return b;
    }

    private static void AddToggle(bool[,] matrix, int buttonIndex, int x, int y)
    {
        if(x < 0 || x >= matrixSize || y < 0 || y >= matrixSize) return;

        int row = y * matrixSize + x;
        matrix[row, buttonIndex] = true;
    }

    private static bool HasSolution(bool[,] A, bool[] b)
    {
        bool[,] aug = BuildAugmentedMatrix(A, b);

        int pivotRow = 0;

        for(int col = 0; col < cMatrixSize; col++)
        {
            int foundRow = FindPivot(aug, pivotRow, col);;

            if(foundRow == -1) continue;

            if(foundRow != pivotRow) 
                SwapRows(aug, foundRow, pivotRow);

            CalculateRows(aug, pivotRow, col);
            pivotRow++;

            if(pivotRow == cMatrixSize) break;
        }

        for(int row = 0; row < cMatrixSize; row++)
        {
            bool allZero = CheckContradiction(aug, row);

            if(allZero && aug[row, cMatrixSize] == true)
                return false;
        }
        return true;
    }

    private static void SwapRows(bool[,] matrix, int rowA, int rowB)
    {
        int cols = matrix.GetLength(1);

        for(int col = 0; col < cols; col++)
        {
            bool tmp = matrix[rowA, col];
            matrix[rowA, col] = matrix[rowB, col];
            matrix[rowB, col] = tmp;
        }
    }

    private static bool[,] BuildAugmentedMatrix(bool[,] A, bool[] b)
    {
        bool[,] matrix = new bool[cMatrixSize, cMatrixSize + 1];
        for(int row = 0; row < cMatrixSize; row++)
        {
            for(int col = 0; col < cMatrixSize; col++)
            {
                matrix[row, col] = A[row, col];
            }
            matrix[row, cMatrixSize] = b[row];
        }
        return matrix;
    }

    private static int FindPivot(bool[,] matrix, int pivotRow, int col)
    {
        for(int row = pivotRow; row < cMatrixSize; row++)
        {
            if(matrix[row, col] == true)
                return row;
        }
        return -1;
    }

    private static void CalculateRows(bool[,] matrix, int pivotRow, int col)
    {
        for(int row = 0; row < cMatrixSize; row++)
        {
            if(row == pivotRow) continue;

            if(matrix[row, col] == true)
            {
                for(int k = col; k < cMatrixSize + 1; k++)
                {
                    matrix[row, k] ^= matrix[pivotRow, k];
                }
            }
        }
    }

    private static bool CheckContradiction(bool[,] matrix, int row)
    {
        for(int col = 0; col < cMatrixSize; col++)
        {
            if(matrix[row, col] != false)
                return false;
        }
        return true;
    }
}