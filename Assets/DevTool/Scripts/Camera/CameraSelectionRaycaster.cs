using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSelectionRaycaster : MonoBehaviour
{
    [SerializeField]
    Camera cam;
    public Highlight target;
    public LayerMask mask;

    public CubeTile cubeTile;

    void Start()
    {
        if (cam == null)
            cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        // Draw Ray
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 100f;
        mousePos = cam.ScreenToWorldPoint(mousePos);
        Debug.DrawRay(cam.transform.position, mousePos, Color.cyan);

        
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100f, mask))
        {
        Transform objectHit = hit.transform;
         //hit.transform.GetComponent<Renderer>().material.color = Color.red;

        // If the object has a selectable component on it, call it.
            if (objectHit.TryGetComponent(out target))
            {
                target.OnHighlightTile();
                if(Input.GetMouseButtonDown(0))
                {
                    target.OnSelectTile();
                }
            }
        }
     }

    public void HandleClick(Vector3 mousePosition)
    {
        GameObject result;
        if (FindTarget(mousePosition, out result))
        {
            Debug.Log("Target Found!");
            

            
            CubeTile selectedTile = result.GetComponent<CubeTile>();
            List<CubeTile> neighbors = cubeTile.neighbors;
            Debug.Log($"Neighbors for {selectedTile} are:");
            
            foreach(CubeTile neighborPos in neighbors)
            {
                Debug.Log(neighborPos);
            }
            
        }
    }

    private bool FindTarget(Vector3 mousePosition, out GameObject result)
    {
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(mousePosition);
        if(Physics.Raycast(ray, out hit, mask))
        {
            result = hit.collider.gameObject;
            return true;
        }
        result = null;
        return false;
    }
}
