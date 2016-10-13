using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour
{
    public bool buttonDownNorth { get; protected set; }
    public bool buttonDownSouth { get; protected set; }
    public bool buttonDownEast  { get; protected set; }
    public bool buttonDownWest  { get; protected set; }

    private void FixedUpdate()
    {
        buttonDownNorth = Input.GetKey(KeyCode.UpArrow);
        buttonDownSouth = Input.GetKey(KeyCode.DownArrow);
        buttonDownEast  = Input.GetKey(KeyCode.LeftArrow);
        buttonDownWest  = Input.GetKey(KeyCode.RightArrow);
    }
}
