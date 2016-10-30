using UnityEngine;
using System.Collections;

public class RTSManager : MonoBehaviour {

    private static GameObject gameManager;

   /**
    * Called first in object initialization cycle
    *   see: https://docs.unity3d.com/Manual/ExecutionOrder.html
    */
	void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        if (gameManager == null)
        {
            Debug.LogWarning("No management to be found... We've got total anarchy here.");
        }
	}

    public static GameObject GetGameManager()
    {
        return gameManager;
    }
}
