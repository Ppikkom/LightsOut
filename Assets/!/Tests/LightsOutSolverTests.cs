using NUnit.Framework;

public class LightsOutSolverTests
{
    [Test]
    public void IsSolvable_AllZero3x3_ReturnsTrue()
    {
        int[,] board =
        {
            { 0, 0, 0 },
            { 0, 0, 0 },
            { 0, 0, 0 }
        };

        bool result = LightsOutSolver.IsSolvable(board);
        Assert.IsTrue(result);
    }

    [Test]
    public void IsSolvable_Sample3x3_ReturnsTrue()
    {
        int[,] board =
        {
            { 1, 0, 1 },
            { 1, 1, 1 },
            { 0, 1, 0 }
        };

        bool result = LightsOutSolver.IsSolvable(board);

        Assert.IsTrue(result);
    }

    [Test]
    public void IsSolvable_AllZero5x5_ReturnsTrue()
    {
        int[,] board =
        {
            { 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0 }
        };

        bool result = LightsOutSolver.IsSolvable(board);

        Assert.IsTrue(result);
    }

    [Test]
    public void IsSolvable_SingleCornerOn5x5_ReturnsFalse()
    {
        int[,] board =
        {
            { 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 1 }
        };

        bool result = LightsOutSolver.IsSolvable(board);

        Assert.IsFalse(result);
    }

    [Test]
    public void IsSolvable_AllZero7x7_ReturnsTrue()
    {
        int[,] board = new int[7, 7];

        bool result = LightsOutSolver.IsSolvable(board);

        Assert.IsTrue(result);
    }
}
