using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCameraController : MonoBehaviour {

    public int horizontalThreshold = 20;
    public int verticalThreshold = 15;

    public float transitSpeed = 5f;

	void Update () {
        // Get at each Update to handle screen scaling
        int screenWidth = Screen.width;
        int screenHeight = Screen.height;

        float mouseX = Input.mousePosition.x;
        float mouseY = Input.mousePosition.y;

        //- Verify Mouse In-Bounds -----------------------=
        //
        Rect screenRect = new Rect(0, 0, screenWidth, screenHeight);
        if (!screenRect.Contains(Input.mousePosition))
        {
            return;
        }

        //- Horizontal Tracking --------------------------=
        //
        if (mouseX < 0 + horizontalThreshold)
        {
            transform.position += Vector3.left * transitSpeed * Time.deltaTime;
        }
        if (mouseX > screenWidth - horizontalThreshold)
        {
            transform.position += Vector3.right * transitSpeed * Time.deltaTime;
        }

        //- Vertical Tracking ----------------------------=
        //
        if (mouseY > screenHeight - verticalThreshold)
        {
            transform.position += Vector3.forward * transitSpeed * Time.deltaTime;
        }
        if (mouseY < 0 + verticalThreshold)
        {
            transform.position += Vector3.back * transitSpeed * Time.deltaTime;
        }
	}
}
