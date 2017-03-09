using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Animator))]
public class UnitController : MonoBehaviour {
    public float movementSpeed = 3f; // [m/s]
    public float turnSpeed = 25f;    // [deg/s]
    public float jumpHeight = 1f;    // [m]
    public float jumpDistance = 1f;  // [m]

    private float currentMoveSpeed = 0f;
    private float currentTurnSpeed = 0f;

    private Animator animator;
    private Rigidbody rigidBody;

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody>();

        animator = GetComponent<Animator>();
        animator.applyRootMotion = true;
    }

    public void ApplyForwardSpeed(float speed)
    {
        //if (Mathf.Abs(currentMoveSpeed - speed) < 0.1f) { return; } // noise gate
        animator.SetFloat("forwardSpeed", speed);
    }

    public void ApplyTurnSpeed(float speed)
    {
        //if (Mathf.Abs(currentTurnSpeed - speed) < 0.1f) { return; } // noise gate
        animator.SetFloat("turnSpeed", speed);
    }

    public bool Grounded()
    {
        return Mathf.Abs(Vector3.Dot(rigidBody.velocity, Vector3.up)) < 0.01;
    }

    public void Jump()
    {
        if (Grounded())
        {
            animator.SetTrigger("jump");
            // TODO: actually calculate (KVATS)
            rigidBody.velocity += Vector3.up * jumpHeight + transform.forward * jumpDistance;
        }
    }

    public void Duck()
    {
        animator.SetTrigger("duck");
    }
}
