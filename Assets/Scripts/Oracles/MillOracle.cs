using UnityEngine;
using System.Collections.Generic;

public class MillOracle : MonoBehaviour
{
    private bool debug = false;

    void Log(string s)
    {
        if (debug)
        {
            Debug.Log("MillOracle log <" + s + ">");
        }
    }

    public Mill WhereShouldIMill(TradeCity currentCity)
    {
        return currentCity.Mills[0];
    }

    public Barn WhereShouldIShop(TradeCity currentCity)
    {
        return currentCity.Barns[0];
    }
}
