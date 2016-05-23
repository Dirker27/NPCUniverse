using UnityEngine;
using System.Collections.Generic;

public class CollierOracle : MonoBehaviour
{
    private bool debug = false;

    void Log(string s)
    {
        if (debug)
        {
            Debug.Log("BakerOracle log <" + s + ">");
        }
    }

    public WoodCut WhereShouldIGather(TradeCity currentCity)
    {
        return currentCity.WoodCuts[0];
    }

    public CharcoalPit WhereShouldICook(TradeCity currentCity)
    {
        return currentCity.CharcoalPits[0];
    }
}
