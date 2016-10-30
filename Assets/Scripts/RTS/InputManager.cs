using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour
{
    public bool buttonDownCameraNorth { get; private set; }
    public bool buttonDownCameraSouth { get; private set; }
    public bool buttonDownCameraEast  { get; private set; }
    public bool buttonDownCameraWest  { get; private set; }

    public bool buttonDownSelectUnit { get; private set; }

    private void FixedUpdate()
    {
        buttonDownCameraNorth = Input.GetKey(KeyCode.UpArrow);
        buttonDownCameraSouth = Input.GetKey(KeyCode.DownArrow);
        buttonDownCameraEast  = Input.GetKey(KeyCode.LeftArrow);
        buttonDownCameraWest  = Input.GetKey(KeyCode.RightArrow);

        buttonDownSelectUnit = Input.GetKey(KeyCode.Space);
    }
}
