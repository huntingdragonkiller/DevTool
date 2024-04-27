using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilities : MonoBehaviour
{
    public static Vector3Int OffsetToCube(Vector2Int offset)
    {
        var q = offset.x - (offset.y + (offset.y % 2)) / 2;
        var r = offset.y;
        return new Vector3Int(q, r, -q-r);
    }

    public static Vector3 GetPositionForHexFromCoordinate(int column, int row, float radius = 1f, bool isFlatTopped = false)
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

            xPosition = (column * (horDis)) + offset;
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
            yPosition = (row * verDis) - offset;
        }
        return new Vector3(xPosition, 0, -yPosition);
    }
}
