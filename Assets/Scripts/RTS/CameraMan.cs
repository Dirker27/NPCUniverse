using UnityEngine;
using System.Collections;

public class CameraMan : MonoBehaviour
{
    public GameObject aperture;
    public GameObject trackBall;
    public Vector3 offset = new Vector3(0, 50, -50);
    public float movementSpeed = 50f;

    private InputManager input;

    private bool staticCamera = false;

    void Start()
    {
        if (aperture == null || trackBall == null)
        {
            Debug.LogWarning("Required components unassigned. Camera will be static.");
            staticCamera = true;
            return;
        }

        // Crudely Inject Dependencies -------------------=
        //
        GameObject gm = RTSManager.GetGameManager();
        if (gm != null)
        {
            input = gm.GetComponent<InputManager>();
        }
        if (input == null)
        {
            Debug.LogWarning("No InputManager found. Camera will be static.");
            staticCamera = true;
            return;
        }

        // Initialize Offset to scene positioning.
        offset = aperture.transform.position - trackBall.transform.position;
    }

    void Update()
    {
        if (staticCamera)
        {
            return;
        }

        // Move Track-Ball
        Vector3 dir = GetMovementVector();
        trackBall.transform.Translate(dir * movementSpeed * Time.deltaTime);

        // Move camera relative to track ball
        aperture.transform.position = trackBall.transform.position + offset;
        aperture.transform.LookAt(trackBall.transform);
    }

    /**
     * Direction vector will be range [-1,-1,-1] --> [1, 1, 1]
     *   (not a unit vector, atm)
     * 
     * @return Direction Vector
     */
    private Vector3 GetMovementVector()
    {
        Vector3 delta = Vector3.zero;
        if (input.buttonDownNorth)
            delta.z += 1;
        if (input.buttonDownSouth)
            delta.z -= 1;
        if (input.buttonDownEast)
            delta.x += 1;
        if (input.buttonDownWest)
            delta.x -= 1;

        return delta;
    }
}
