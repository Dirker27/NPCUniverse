using UnityEngine;
using System.Collections.Generic;

public class SawWorkerOracle : MonoBehaviour
{
    private bool debug = false;

    void Log(string s)
    {
        if (debug)
        {
            Debug.Log("SawWorkerOracle log <" + s + ">");
        }
    }

    public LogStore WhereShouldIGather(TradeCity currentCity)
    {
        return currentCity.LogStores[0];
    }

    public SawHouse WhereShouldISaw(TradeCity currentCity)
    {
        return currentCity.SawHouses[0];
    }
}
