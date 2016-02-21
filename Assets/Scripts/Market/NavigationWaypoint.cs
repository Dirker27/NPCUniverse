using UnityEngine;
using System.Collections;

public class NavigationWaypoint : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public NavigationWaypoint GetRandomDestination ()
    {
        GameObject[] marketplaces = GameObject.FindGameObjectsWithTag("Marketplace");

        if (marketplaces.Length > 0)
        {
            return marketplaces[Random.Range(0, marketplaces.Length - 1)].GetComponent<NavigationWaypoint>();
        }
        return null;
    }
}
