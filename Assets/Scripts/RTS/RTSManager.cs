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
        //- Find Game Manager --------------------------------------=
        //
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        if (gameManager == null)
        {
            Debug.LogWarning("No management to be found... We've got total anarchy here.");
        }

        //- Find Template Farm -------------------------------------=
        //
        GameObject templateFarmObj = GameObject.FindGameObjectWithTag("Template");
        if (templateFarmObj != null) {
            templateFarm = templateFarmObj.GetComponent<TemplateFarm>();
        }
        if (templateFarm == null)
        {
            Debug.LogWarning("No object templates to be found... Spawning won't be a thing.");
        }
	}

    /**
     * Lookup Helper: Game Manager
     * 
     * A number of objects will need access to common utilities under the GM.
     *   By maintaining a static reference to the GM (linked @ Awake), we can
     *   simplify lookup and provide a performance boost at Start().
     */
    public static GameObject GetGameManager()
    {
        return gameManager;
    }

    /**
     * Spawn an RTS waypoint object based off our template.
     */
    public static RTSWaypoint SpawnWaypoint()
    {
        if (templateFarm == null || templateFarm.waypoint == null)
        {
            Debug.LogWarning("Spawn failure - No template found.");
            return null;
        }

        GameObject cloneWaypoint = GameObject.Instantiate(templateFarm.waypoint);
        RTSWaypoint waypoint = cloneWaypoint.GetComponent<RTSWaypoint>();

        if (waypoint == null)
        {
            Debug.LogWarning("Spawn failure - Template object is not a waypoint.");
            GameObject.Destroy(cloneWaypoint);
            return null;
        }

        // Template objects may be inactive. Make sure the clones aren't.
        if (! cloneWaypoint.activeInHierarchy)
        {
            cloneWaypoint.SetActive(true);
        }
        return waypoint;
    }
}
