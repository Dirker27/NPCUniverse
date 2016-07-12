using UnityEngine;
using System.Collections.Generic;

public class FletcherOracle : MonoBehaviour
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

    public BowShop WhereShouldIWork(TradeCity currentCity)
    {
        return currentCity.BowShops[0];
    }
}
