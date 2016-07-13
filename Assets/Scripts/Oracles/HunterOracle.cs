using UnityEngine;
using System.Collections.Generic;

public class HunterOracle : MonoBehaviour
{
    private bool debug = false;

    void Log(string s)
    {
        if (debug)
        {
            Debug.Log("HunterOracle log <" + s + ">");
        }
    }

    public HuntingLodge WhereShouldIHunt(TradeCity currentCity)
    {
        return currentCity.HuntingLodges[0];
    }

    public BowShop WhereShouldIShop(TradeCity currentCity)
    {
        return currentCity.BowShops[0];
    }
}
