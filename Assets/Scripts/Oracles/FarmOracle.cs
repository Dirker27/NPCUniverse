using UnityEngine;
using System.Collections.Generic;

public class FarmOracle : MonoBehaviour
{
    private bool debug = false;

    void Log(string s)
    {
        if (debug)
        {
            Debug.Log("FarmOracle log <" + s + ">");
        }
    }

    public Farm WhereShouldIFarm(TradeCity currentCity)
    {
        return currentCity.Farms[0];
    }

    public Barn WhereShouldIBarn(TradeCity currentCity)
    {
        return currentCity.Barns[0];
    }
}
