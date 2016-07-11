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
     * 
     * Its what I do.
     * 
     * Note to add two functions one where should i farm wheat and one wehre should i farm barley.
     * When some one asks where I should farm the oracle will see how many farmers are registered at each farm 
     * and make a blanaced decision.  It can find out the type of farms by looking in the list of farms the city has.
     * I will need to add some way to know what farmers are where. Maybe the farm oracle rembers where it added farmers?
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
