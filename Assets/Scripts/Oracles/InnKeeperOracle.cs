using UnityEngine;
using System.Collections.Generic;

public class InnKeeperOracle : MonoBehaviour
{
    private bool debug = false;

    void Log(string s)
    {
        if (debug)
        {
            Debug.Log("InnKeeperOracle log <" + s + ">");
        }
    }

    public Barn WhereShouldIGetBeerAndFish(TradeCity currentCity)
    {
        return currentCity.Barns[0];
    }

    public Bakery WhereShouldIGetBread(TradeCity currentCity)
    {
        return currentCity.Bakeries[0];
    }

    public Tavern WhereShouldIWork(TradeCity currentCity)
    {
        return currentCity.Taverns[0];
    }
}
