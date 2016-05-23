using UnityEngine;
using System.Collections.Generic;

public class FoundryOracle : MonoBehaviour
{
    private bool debug = false;

    void Log(string s)
    {
        if (debug)
        {
            Debug.Log("FoundryOracle log <" + s + ">");
        }
    }

    public Foundry WhereShouldISmith(TradeCity currentCity)
    {
        return currentCity.Foundries[0];
    }

    public OreShop WhereShouldIShop(TradeCity currentCity)
    {
        return currentCity.OreShops[0];
    }
}
