using UnityEngine;
using System.Collections.Generic;

public class ToolSmithOracle : MonoBehaviour
{
    private bool debug = false;

    void Log(string s)
    {
        if (debug)
        {
            Debug.Log("ToolSmithOracle log <" + s + ">");
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
