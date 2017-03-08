using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Unit : MonoBehaviour {

    public float health = 100f;
    public float movementSpeed = 1f; // [m/s]
    public float turnSpeed = 1f;
    public float jumpHeight = 1f; // [m]

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
