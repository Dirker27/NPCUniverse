using UnityEngine;
using System.Collections.Generic;

public class QuaterMasterOracle : MonoBehaviour
{
    private bool debug = false;

    void Log(string s)
    {
        if (debug)
        {
            Debug.Log("QuaterMasterOracle log <" + s + ">");
        }
    }

    public GuildHall WhereShouldIStore(TradeCity currentCity)
    {
        return currentCity.GuildHalls[0];
    }

    public Smithy WhereShouldIShop(TradeCity currentCity)
    {
        return currentCity.Smithies[0];
    }
}
