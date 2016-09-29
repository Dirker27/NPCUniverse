using UnityEngine;
using System.Collections;

public class RTSWaypoint : MonoBehaviour
{
    public float arrivalRadius = 1f;

    void Update()
    {
        // TODO: Visual Pulse
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }
}
