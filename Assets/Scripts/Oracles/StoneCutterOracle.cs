using UnityEngine;
using System.Collections.Generic;

public class StoneCutterOracle : MonoBehaviour
{
    private bool debug = false;

    void Log(string s)
    {
        if (debug)
        {
            Debug.Log("StoneCutterOracle log <" + s + ">");
        }
    }

    public Masonry WhereShouldICut(TradeCity currentCity)
    {
        return currentCity.Masonries[0];
    }

    public OreShop WhereShouldIShop(TradeCity currentCity)
    {
        return currentCity.OreShops[0];
    }
}
