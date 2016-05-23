using UnityEngine;
using System.Collections.Generic;

public class WoodCuterOracle : MonoBehaviour
{
    private bool debug = false;

    void Log(string s)
    {
        if (debug)
        {
            Debug.Log("BakerOracle log <" + s + ">");
        }
    }

    public LogStore WhereShouldIGather(TradeCity currentCity)
    {
        return currentCity.LogStores[0];
    }

    public WoodCut WhereShouldICut(TradeCity currentCity)
    {
        return currentCity.WoodCuts[0];
    }
}
