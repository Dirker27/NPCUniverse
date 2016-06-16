using UnityEngine;
using System.Collections.Generic;

public class FarmOracle : MonoBehaviour
{
    private bool debug = false;

    private bool ToWheat = true;

    void Log(string s)
    {
        if (debug)
        {
            Debug.Log("FarmOracle log <" + s + ">");
        }
    }

    /**
     * Nathaniel Hates context and happiness
     * 
     * The Wheat farm is at [0] in Fort Kickass' Farm pool.
     * The Barley farm is at [1] in Fort Kickass' Farm pool.
     * 
     * APPARENTLY this is our system.
     * 
     * ...wtf?
     */
    public Farm WhereShouldIFarm(TradeCity currentCity)
    {
        if (ToWheat)
        {
            ToWheat = false;
            return currentCity.Farms[0];
        }
        ToWheat = true;
        return currentCity.Farms[1];
    }

    public Barn WhereShouldIBarn(TradeCity currentCity)
    {
        return currentCity.Barns[0];
    }
}
