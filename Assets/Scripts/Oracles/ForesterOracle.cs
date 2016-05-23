using UnityEngine;
using System.Collections.Generic;

public class ForesterOracle : MonoBehaviour
{
    private bool debug = false;

    void Log(string s)
    {
        if (debug)
        {
            Debug.Log("BakerOracle log <" + s + ">");
        }
    }

    public Forest WhereShouldICut(TradeCity currentCity)
    {
        return currentCity.Forests[0];
    }

    public LogStore WhereShouldIStore(TradeCity currentCity)
    {
        return currentCity.LogStores[0];
    }
}
