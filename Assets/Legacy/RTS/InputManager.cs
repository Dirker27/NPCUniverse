using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour
{
    // Camera Control
    public bool buttonDownCameraNorth { get; private set; }
    public bool buttonDownCameraSouth { get; private set; }
    public bool buttonDownCameraEast  { get; private set; }
    public bool buttonDownCameraWest  { get; private set; }

    // Unit Control
    public bool buttonDownUnitSelect { get; private set; }
    public bool buttonDownUnitMove   { get; private set; }

    private void FixedUpdate()
    {
        // Camera Control
        buttonDownCameraNorth = Input.GetKey(KeyCode.UpArrow);
        buttonDownCameraSouth = Input.GetKey(KeyCode.DownArrow);
        buttonDownCameraEast  = Input.GetKey(KeyCode.LeftArrow);
        buttonDownCameraWest  = Input.GetKey(KeyCode.RightArrow);

        // Unit Control
        buttonDownUnitSelect = Input.GetKey(KeyCode.Space);
        buttonDownUnitMove   = Input.GetKey(KeyCode.Return);
    }
}
