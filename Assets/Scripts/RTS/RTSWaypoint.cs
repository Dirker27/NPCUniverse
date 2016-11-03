using UnityEngine;
using System.Collections.Generic;

public class RTSWaypoint : MonoBehaviour
{
    public float arrivalRadius = 1f;

    private List<RTSWaypointMovement> incomingUnits = new List<RTSWaypointMovement>();

    void Update()
    {
        // TODO: Visual Pulse

        //- Self-Destruct --------------------------------=
        //
        if (this.incomingUnits.Count == 0)
        {
            GameObject.Destroy(gameObject);
        }
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void UnitIncoming(RTSWaypointMovement unit)
    {
        incomingUnits.Add(unit);
    }

    public void UnitArrived(RTSWaypointMovement unit)
    {
        incomingUnits.Remove(unit);
    }
}
