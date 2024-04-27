using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Node parent;
    public HexTile target;
    public HexTile destination;
    public HexTile origin;

    public int baseCost;
    public int costFromOrigin;
    public int costToDestination;
    public int pathCost;

    public Node(HexTile current, HexTile origin, HexTile destination, int pathCost)
    {
        parent = null;
        this.target = current;
        this.destination = destination;
        this.origin = origin;

        baseCost = 1;
        costFromOrigin = (int)Vector3Int.Distance(current.cubeCoordinate, origin.cubeCoordinate);
        costToDestination = (int)Vector3Int.Distance(current.cubeCoordinate, destination.cubeCoordinate);
        this.pathCost = pathCost;
    }

    public int GetCost()
    {
        return pathCost + baseCost + costFromOrigin + costToDestination;
    }

    public void SetParent(Node node)
    {
        this.parent = node;
    }
}
