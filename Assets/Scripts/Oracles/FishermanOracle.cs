using UnityEngine;
using System.Collections.Generic;

public class FishermanOracle : MonoBehaviour
{
    private bool debug = false;

    void Log(string s)
    {
        if (debug)
        {
            Debug.Log("FishermanOracle log <" + s + ">");
        }
    }

    public Pond WhereShouldIFish(TradeCity currentCity)
    {
        return currentCity.Ponds[0];
    }

    public Barn WhereShouldIStore(TradeCity currentCity)
    {
        return currentCity.Barns[0];
    }
}
