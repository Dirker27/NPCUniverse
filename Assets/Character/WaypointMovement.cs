using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(UnitController))]
[RequireComponent(typeof(NavMeshAgent))]
public class WaypointMovement : MonoBehaviour {

    public float arrivalRadius = 5f;
    public GameObject currentWaypoint;

    private NavMeshAgent agent;
    private UnitController controller;

	void Start ()
    {
        controller = GetComponent<UnitController>();

        agent = GetComponent<NavMeshAgent>();
        // If you want the controller to move the object, not the agent.
        //   See: https://docs.unity3d.com/Manual/nav-MixingComponents.html
        //agent.updatePosition = false;
        //agent.updateRotation = false;
        // Use controller's movement params.
        agent.speed = controller.movementSpeed;
        agent.angularSpeed = controller.turnSpeed;
	}
	
	void Update () {
        if (currentWaypoint != null)
        {
            moveTowardsWaypoint();
        }
    }


    private void moveTowardsWaypoint()
    {
        //- Waypoint Reached -----------------------------=
        //
        Vector3 destinationDelta = currentWaypoint.transform.position - transform.position;
        if (destinationDelta.magnitude < arrivalRadius)
        {
            currentWaypoint = null;

            agent.Stop();

            controller.ApplyForwardSpeed(0f);
            controller.ApplyTurnSpeed(0f);

            return;
        }

        //- Apply NavMesh Movement  ----------------------=
        //
        /*
        Vector3 navigationDelta = agent.nextPosition - transform.position;
        float turnSpeed = Vector3.Dot(Vector3.up, Vector3.Cross(transform.forward, navigationDelta));
        controller.ApplyTurnSpeed(turnSpeed);
        */
        //
        //float forwardSpeed = Vector3.Dot(transform.forward, navigationDelta);
        controller.ApplyForwardSpeed(agent.velocity.magnitude);
        //
        Debug.DrawLine(transform.position, agent.nextPosition, Color.red);
    }

    public void AssignWaypoint(GameObject waypoint)
    {
        currentWaypoint = waypoint;
        agent.SetDestination(waypoint.transform.position);
        agent.Resume();
    }
}
