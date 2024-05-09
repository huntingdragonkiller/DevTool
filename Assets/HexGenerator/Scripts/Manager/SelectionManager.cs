using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SelectionManager : MonoBehaviour
{
    [SerializeField]
    Camera cam;
    public LayerMask mask;

    //private TileGrid tileGrid;

    //List<Vector3Int> neighbors = new List<Vector3Int>();
    

    public UnityEvent<GameObject> OnUnitSelected;
    public UnityEvent<GameObject> TerrainSelected;


    void Start()
    {
        if (cam == null)
            cam = Camera.main;
        //tileGrid = GameObject.FindGameObjectWithTag("TileGrid").GetComponent<TileGrid>();
        //unit = GameObject.FindGameObjectWithTag("Target").GetComponent<Unit>();
    }

    public void HandleClick(Vector3 mousePosition)
    {
        GameObject result;
        if (FindTarget(mousePosition, out result))
        {
            
            Tile selectedTile = result.GetComponent<Tile>();
            /*
            Debug.Log($"{selectedTile.tileCoords}");
            neighbors = tileGrid.GetNeighboursFor(selectedTile.tileCoords);
            Debug.Log($"Neighbors for {selectedTile.tileCoords} are:");
            foreach (Vector3Int neighborPos in neighbors)
            {
                Debug.Log(neighborPos);
            }
            */
            
            if(UnitSelected(result))
            {
                OnUnitSelected?.Invoke(result);
                Debug.Log("Unit Selected");
            }
            else
            {
                TerrainSelected?.Invoke(result);
                Debug.Log("Terrain Selected");
            }
            
            
            /*
            foreach (Vector3Int neighbor in neighbors)
            {
                tileGrid.GetTileAt(neighbor).DisableHighlight();
            }
            
            BFSResult bfsResult = GraphSearch.BFSGetRange(tileGrid, selectedTile.tileCoords, 20);
            neighbors = new List<Vector3Int>(bfsResult.GetRangePositions());
            Debug.Log("BFS Works");
            
            foreach (Vector3Int neighbor in neighbors)
            {
                tileGrid.GetTileAt(neighbor).EnableHighlight();
            }
            selectedTile.DisableHighlight();
            */
        }
    }

    private bool UnitSelected(GameObject result)
    {
        return result.GetComponent<Unit>() != null;
    }

    private bool FindTarget(Vector3 mousePosition, out GameObject result)
    {
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(ray, out hit, mask))
        {
            result = hit.collider.gameObject;
            return true;
        }
        result = null;
        return false;
    }
}
