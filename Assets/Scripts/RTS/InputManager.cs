using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour
{
    public bool IsButtonDownNorth()
    {
        return Input.GetKeyDown(KeyCode.UpArrow);
    }
    public bool IsButtonDownSouth()
    {
        return Input.GetKeyDown(KeyCode.DownArrow);
    }
    public bool IsButtonDownWest()
    {
        return Input.GetKeyDown(KeyCode.LeftArrow);
    }
    public bool IsButtonDownEast()
    {
        return Input.GetKeyDown(KeyCode.RightArrow);
    }
}
