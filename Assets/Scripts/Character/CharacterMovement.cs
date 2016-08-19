using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour 
{
    public NavigationWaypoint location;
    public NavigationWaypoint destination;
    public float travelRate = 1; // [m/s]
    public bool twoDimensionalMovement = true;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if (destination != null)
        {
            Move();

            if (Vector3.Distance(destination.transform.position, transform.position) < 3f)
            {
                location = destination;
                destination = null;
            }
        }
	}

    private void Move()
    {
        Vector3 dPos = destination.transform.position;
        Vector3 myPos = transform.position;
        if (twoDimensionalMovement)
        {
            myPos.y = 0;
            dPos.y = 0;
        }
        Vector3 directionVector = (dPos - myPos) / Vector3.Distance(dPos, myPos);

        float rate = Time.deltaTime * travelRate;

        transform.localPosition += directionVector * rate;
    }

    public bool isInTransit()
    {
        return destination != null;
    }
}
