using UnityEngine;
using System.Collections.Generic;

public class WeaponSmithOracle : MonoBehaviour
{
    private bool debug = false;

    void Log(string s)
    {
        if (debug)
        {
            Debug.Log("WeaponSmithOracle log <" + s + ">");
        }
    }

    public Smithy WhereShouldISmith(TradeCity currentCity)
    {
        return currentCity.Smithies[0];
    }

    public Foundry WhereShouldIShop(TradeCity currentCity)
    {
        return currentCity.Foundries[0];
    }
}
