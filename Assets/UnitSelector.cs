using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelector : MonoBehaviour {
    private const int SELECTED_LAYER_MASK = 1 << 8; // Ignore all layers except #8

    public List<Unit> selectedUnits;

    private Camera selectionCamera;

    void Start()
    {
        selectionCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        if (selectionCamera == null) { Debug.LogError("Please define a Main Camera."); }
    }
	
	void Update ()
    {
        //- Single-Unit Select ---------------------------=
        //
        if (Input.GetMouseButtonDown(0))
        {
            selectedUnits.Clear();

            Ray r = selectionCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(r, out hit, SELECTED_LAYER_MASK))
            {
                Unit u = hit.collider.gameObject.GetComponent<Unit>();
                if (u != null)
                {
                    selectedUnits.Add(u);
                    Debug.Log("Selected Unit: " + u.name);
                }
            }
        }

        //- Command Selected -----------------------------=
        //
        /*if (Input.GetMouseButtonDown(1))
        {
            Ray r = selectionCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(r, out hit))
            {
                foreach (Unit u in selectedUnits)
                {
                    WaypointMovement wm = u.GetComponent<WaypointMovement>();
                    if (wm != null)
                    {
                        wm.AssignWaypoint()
                    }
                }
            }
        }*/
	}
}
