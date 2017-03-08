using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Unit))]
[RequireComponent(typeof(NavMeshAgent))]
public class WaypointMovement : MonoBehaviour {

    public float arrivalRadius = 5f;
    public GameObject currentWaypoint;

    private NavMeshAgent agent;
    private Unit unit;

	// Use this for initialization
	void Start ()
    {
        unit = GetComponent<Unit>();
        agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
        if (currentWaypoint != null)
        {
            moveTowardsWaypoint();
        }
    }


    private void moveTowardsWaypoint()
    {
        //TODO: remove
        agent.destination = currentWaypoint.transform.position;

        Vector3 worldDelta = agent.nextPosition - transform.position;
        if (worldDelta.magnitude < arrivalRadius)
        {
            currentWaypoint = null;
            return;
        }

        /*
        float turnSpeed = Vector3.Dot(Vector3.up, Vector3.Cross(transform.forward, worldDelta));
        float forwardSpeed = Vector3.Dot(transform.forward, worldDelta);
        */

        Vector3 targetDirection = worldDelta / worldDelta.magnitude;
        transform.position += targetDirection * unit.movementSpeed * Time.deltaTime;
    }

    /*public void AssignWaypoint(GameObject waypoint)
    {
        currentWaypoint = waypoint;
        agent.SetDestination(waypoint.transform.position);
    }*/
}
