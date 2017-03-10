using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnitSelection))]
public class UnitCommander : MonoBehaviour {

    public GameObject waypointTemplate;

    private Camera selectionCamera;
    private UnitSelection unitSelection;

    private void Start()
    {
        unitSelection = GetComponent<UnitSelection>();

        selectionCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        if (selectionCamera == null) { Debug.LogError("Please define a Main Camera."); }
    }

    void Update () {
        List<Unit> selectedUnits = unitSelection.selectedUnits;

        //- Command Selected -----------------------------=
        //
        if (Input.GetMouseButtonDown(1) && selectedUnits.Count > 0)
        {
            Ray r = selectionCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(r, out hit))
            {
                GameObject waypoint = GameObject.Instantiate(waypointTemplate);
                waypoint.transform.position = hit.point;

                foreach (Unit u in selectedUnits)
                {
                    WaypointMovement wm = u.GetComponent<WaypointMovement>();
                    if (wm != null)
                    {
                        wm.AssignWaypoint(waypoint);
                    }
                }
            }
        }
    }
}
