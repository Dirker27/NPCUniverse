using UnityEngine;
using System.Collections;

public class RTSUnit : MonoBehaviour
{
    public float health = 100f;
    public bool playerSelected = false;

    private RTSWaypointMovement waypointMovement;

    void Start()
    {
        this.waypointMovement = gameObject.GetComponent<RTSWaypointMovement>();
        if (this.waypointMovement == null)
        {
            Debug.LogWarning("No movement component found for unit [" + gameObject.name + "]. Kinda stuck here...");
        }
    }

    public void SetDestinationWaypoint(RTSWaypoint waypoint)
    {
        if (this.waypointMovement != null)
        {
            this.waypointMovement.targetWaypoint = waypoint;
        }
    }
}
