using UnityEngine;
using System.Collections;

public class RTSWaypointMovement : MonoBehaviour
{
    public RTSWaypoint targetWaypoint;
    private Vector3 destination;

    public float travelRate = 1; // [m/s]
    public bool twoDimensionalMovement = true;

    void Update()
    {
        //- Determine Destination ------------------------=
        //
        // Always move toward the waypoint if there's one set.
        if (targetWaypoint != null)
        {
            destination = targetWaypoint.GetPosition();

            // 2-D movement: lock destination to y-plane
            if (twoDimensionalMovement)
            {
                destination.y = transform.position.y;
            }
        }

        //- Apply Movement -------------------------------=
        //
        Move();

        //- Update Waypoint ------------------------------=
        //
        // Uses above assertion that destination is the target waypoint when set.
        if (targetWaypoint != null)
        {
            float delta = Vector3.Distance(destination, transform.position);
            if (delta < targetWaypoint.arrivalRadius)
            {
                targetWaypoint = null;
                destination = transform.position;
            }
        }
    }

    private void Move()
    {
        Vector3 direction = DirectionUnitVector();
        float speed = travelRate * Time.deltaTime;   // [m/s] * d[s] == d[m]

        this.transform.position += speed * direction;
    }

    /**
     * Generate Unit Vector for traversal on this frame.
     * 
     * @return [0,0,0] if nowhere to go.
     */
    private Vector3 DirectionUnitVector()
    {
        if (targetWaypoint == null)
        {
            return Vector3.zero;
        }

        Vector3 myPos = transform.position;

        float distance = Vector3.Distance(destination, myPos);
        if (distance == 0) 
        {
            return Vector3.zero;
        }
        
        return (destination - myPos) / distance;
    }
}
