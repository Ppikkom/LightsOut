using UnityEngine;

public static class BlockCoordHelper
{
    public static Vector3 GridToWorld( Vector2Int coord, Vector2 blockSpacing, float blockSize, int fieldSize)
    {
        Vector2Int centeredCoord = coord - Vector2Int.one * (fieldSize / 2);

        float x = centeredCoord.x * (blockSize + blockSpacing.x);
        float y = centeredCoord.y * (blockSize + blockSpacing.y);

        return new Vector3(x, y, 0f);
    }

    public static Vector2Int WorldToGrid(Vector3 worldPos, Vector2 blockSpacing, float blockSize, int fieldSize)
    {
        float cellWidth = blockSize + blockSpacing.x;
        float cellHeight = blockSize + blockSpacing.y;

        int x = Mathf.RoundToInt(worldPos.x / cellWidth);
        int y = Mathf.RoundToInt(worldPos.y / cellHeight);

        Vector2Int centeredCoord = new Vector2Int(x, y);
        return centeredCoord + Vector2Int.one * (fieldSize / 2);
    }
}