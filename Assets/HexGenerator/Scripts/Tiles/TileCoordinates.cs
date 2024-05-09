using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileCoordinates : MonoBehaviour
{
    public static float xOffset = 2, yOffset = 1, zOffset = 1.74f;

    internal Vector3Int GetTileCoords()
        => offsetCoordinates;

    [Header("Offset coordinates")]
    [SerializeField]
    private Vector3Int offsetCoordinates;

    private void Awake()
    {
        offsetCoordinates = ConvertPositionToOffset(transform.position);
    }

    public static Vector3Int ConvertPositionToOffset(Vector3 position)
    {
        int x = Mathf.CeilToInt(position.x / xOffset);
        int y = Mathf.RoundToInt(position.y / yOffset);
        int z = Mathf.RoundToInt(position.z / zOffset);
        return new Vector3Int(x, y, z);
    }

    public static Vector3 GetPositionForHexFromCoordinate(int column, int row, float radius, bool isFlatTopped = false)
    {
        float width, height, xPosition, yPosition, horDis, verDis, offset;
        bool shouldOffset;
        float size = radius;

        if (!isFlatTopped)
        {
            shouldOffset = (row % 2) == 0;
            width = Mathf.Sqrt(3f) * size;
            height = 2f * size;

            horDis = width;
            verDis = height * (3f / 4f);

            offset = (shouldOffset) ? width / 2 : 0;

            xPosition = Mathf.RoundToInt((column * horDis) + (offset-1));
            yPosition = (row * verDis);
        }
        else
        {
            shouldOffset = (column % 2) == 0;
            width = 2f * size;
            height = Mathf.Sqrt(3f) * size;

            horDis = width * (3f / 4f);
            verDis = height;

            offset = (shouldOffset) ? height / 2 : 0;
            xPosition = (column * (horDis));
            yPosition = Mathf.RoundToInt((row * verDis) + (offset-1));
        }
        return new Vector3(xPosition, 0, yPosition);
    }
}