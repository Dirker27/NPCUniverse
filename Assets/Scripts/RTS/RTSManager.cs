using UnityEngine;
using System.Collections;

public class RTSManager : MonoBehaviour {

    private static GameObject gameManager;
    private static TemplateFarm templateFarm;

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

        GameObject templateFarmObj = GameObject.FindGameObjectWithTag("Template");
        if (templateFarmObj != null) {
            templateFarm = templateFarmObj.GetComponent<TemplateFarm>();
        }
        if (templateFarm == null)
        {
            Debug.LogWarning("No object templates to be found... Spawning won't be a thing.");
        }
	}

    public static GameObject GetGameManager()
    {
        return gameManager;
    }

    public static RTSWaypoint SpawnWaypoint()
    {
        if (templateFarm == null || templateFarm.waypoint == null)
        {
            Debug.LogWarning("Spawn failure - No template found.");
            return null;
        }

        GameObject obj = GameObject.Instantiate(templateFarm.waypoint);
        RTSWaypoint waypoint = obj.GetComponent<RTSWaypoint>();

        if (waypoint == null)
        {
            GameObject.Destroy(obj);
            return null;
        }

        // Template objects may be inactive. Make sure the clones aren't.
        if (! obj.activeInHierarchy)
        {
            obj.SetActive(true);
        }
        return waypoint;
    }
}
