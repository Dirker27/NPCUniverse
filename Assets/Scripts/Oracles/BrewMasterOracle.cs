using UnityEngine;
using System.Collections.Generic;

public class BrewMasterOracle : MonoBehaviour
{
    private bool debug = false;

    void Log(string s)
    {
        if (debug)
        {
            Debug.Log("BrewMasterOracle log <" + s + ">");
        }
    }

    public Brewhouse WhereShouldIBrew(TradeCity currentCity)
    {
        return currentCity.Brewhouses[0];
    }

    public Barn WhereShouldIGather(TradeCity currentCity)
    {
        return currentCity.Barns[0];
    }
}
