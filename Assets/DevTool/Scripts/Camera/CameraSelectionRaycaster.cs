using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraSelectionRaycaster : MonoBehaviour
{
    Camera cam;
    public HexHighlight target;
    public LayerMask mask;

    void Start()
    {
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
                Debug.Log("Raycast Hit");
                Transform objectHit = hit.transform;
                //hit.transform.GetComponent<Renderer>().material.color = Color.red;

                // If the object has a selectable component on it, call it.
                if (objectHit.TryGetComponent<HexHighlight>(out target))
                {
                    target.OnHighlightTile();
                    if(Input.GetMouseButtonDown(0))
                    {
                        target.OnSelectTile();
                    }
                }
            }
        }
}
