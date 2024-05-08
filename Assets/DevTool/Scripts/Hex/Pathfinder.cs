using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    // Creates a list of tiles for a path to the destination from the origin
    public static List<HexTile> FindPath(HexTile origin, HexTile destination)
    {
        // Dictionaries splitting between nodes evaluated and not evaluated
        Dictionary<HexTile, Node> nodesNotEvaluated = new Dictionary<HexTile, Node>();
        Dictionary<HexTile, Node> nodesAlreadyEvaluated = new Dictionary<HexTile, Node>();

        Node startNode = new Node(origin, origin, destination, 0);
        nodesNotEvaluated.Add(origin, startNode);

        bool gotPath = EvaluateNextNode(nodesNotEvaluated, nodesAlreadyEvaluated, origin, destination, out List<HexTile> path);

        while (!gotPath)
        {
            gotPath = EvaluateNextNode(nodesNotEvaluated, nodesAlreadyEvaluated, origin, destination, out path);
        }

        return path;
    }

    private static bool EvaluateNextNode(Dictionary<HexTile, Node> nodesNotEvaluated, Dictionary<HexTile, Node> nodesEvaluated,
        HexTile origin, HexTile destination, out List<HexTile> Path)
    {
        Node currentNode = GetCheapestNode(nodesNotEvaluated.Values.ToArray());

        if(currentNode == null) 
        {
            Path = new List<HexTile>();
            return false;
        }

        nodesNotEvaluated.Remove(currentNode.target);
        nodesEvaluated.Add(currentNode.target, currentNode);

        Path = new List<HexTile>();

        // If this is our destination then we're done
        if(currentNode.target == destination)
        {
            Path.Add(currentNode.target);
            while(currentNode.target != origin)
            {
                Path.Add(currentNode.parent.target);
                currentNode = currentNode.parent;
            }

            return true;
        }

        // Otherwise, add our neightbors to the list and try to traverse them
        List<Node> neighbors = new List<Node>();
        foreach(HexTile tile in currentNode.target.neighbors)
        {
            Node node = new Node(tile, origin, destination, currentNode.GetCost());

            // If the tile type isn't something we can traverse, then we make the cost really high.
            if(tile.tileType != HexTileGenerationSettings.TileType.Standard)
            {
                node.baseCost = 999999;
                // continue
            }

            neighbors.Add(node);
        }

        foreach(Node neighbor in neighbors)
        {
            // If the tile has already been evaluated fully we can ignore it
            if (nodesEvaluated.Keys.Contains(neighbor.target)) { continue; }

            // If the cost is lower, or if the tile isn't in the not evaluated pile
            if(neighbor.GetCost() < currentNode.GetCost() || !nodesNotEvaluated.Keys.Contains(neighbor.target))
            {
                neighbor.SetParent(currentNode);
                if(!nodesNotEvaluated.Keys.Contains(neighbor.target))
                {
                    nodesNotEvaluated.Add(neighbor.target, neighbor);
                }
            }
        }

        return false;
    }

    private static Node GetCheapestNode(Node[] nodesNotEvaluated)
    {
        if(nodesNotEvaluated.Length == 0 ){ return null; }

        Node selectedNode = nodesNotEvaluated[0];

        for(int i = 1; i < nodesNotEvaluated.Length; i++)
        {
            var currentNode = nodesNotEvaluated[i];
            if(currentNode.GetCost() < selectedNode.GetCost())
            {
                selectedNode = currentNode;
            }
            else if (currentNode.GetCost() == selectedNode.GetCost() && currentNode.costToDestination < selectedNode.costToDestination)
            {
                selectedNode = currentNode;
            }
        }
        return selectedNode;
    }
}
