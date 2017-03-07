using UnityEngine;
using System.Collections.Generic;

public class UnitCommander : MonoBehaviour {
    public List<RTSUnit> unitRoster;       // Units we're allowed to control
    public List<RTSUnit> selectedUnits;    // Units we're currently directing

    public float selectionRadius = 5f;     // [m]

    private InputManager input;
    private GameObject cursor;

    void Start()
    {
        // Crudely Inject Dependencies -------------------=
        //
        // Input Manager
        GameObject gm = RTSManager.GetGameManager();
        if (gm != null)
        {
            input = gm.GetComponent<InputManager>();
        }
        if (input == null)
        {
            Debug.LogWarning("No InputManager found. Unit direction will be disabled.");
        }
        // Selection Cursor
        cursor = GameObject.FindGameObjectWithTag("Cursor");
        if (cursor == null)
        {
            Debug.LogWarning("No selection cursor tagged. Unit selection will be disabled.");
        }
    }

	void Update()
    {
        if (input == null || cursor == null)
        {
            return;
        }

        //- Select units in radius of cursor -----------------------=
        //
        if (input.buttonDownUnitSelect)
        {
            this.selectedUnits.Clear();

            // Nasty linear search
            foreach (RTSUnit unit in unitRoster)
            {
                Vector3 delta = unit.transform.position - cursor.transform.position;
                if (delta.magnitude < this.selectionRadius)
                {
                    this.selectedUnits.Add(unit);
                    Debug.Log("Unit [" + unit.name + "] selected.");
                }
            }
        }

        //- Move selected units ------------------------------------=
        //
        // ( is 'Count' efficient? )
        if (input.buttonDownUnitMove && this.selectedUnits.Count > 0)
        {
            Debug.Log("Placing waypoint at <" + cursor.transform.position + ">");

            RTSWaypoint waypoint = (RTSWaypoint) RTSManager.SpawnWaypoint();
            if (waypoint != null)
            {
                waypoint.transform.position = cursor.transform.position;
                foreach (RTSUnit unit in selectedUnits)
                {
                    unit.SetDestinationWaypoint(waypoint);
                }
            }
            else
            {
                Debug.LogWarning("Movement failure - Could not spawn waypoint.");
            }
        }
	}
}
