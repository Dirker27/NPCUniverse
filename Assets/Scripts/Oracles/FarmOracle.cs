using UnityEngine;
using System.Collections.Generic;

public class FarmOracle : MonoBehaviour
{
    private bool debug = false;

    private bool Wheat = true;
    private bool Barley = false;

    void Log(string s)
    {
        if (debug)
        {
            Debug.Log("FarmOracle log <" + s + ">");
        }
    }
    public Farm WhereShouldIFarm(TradeCity currentCity)
    {
        return currentCity.Farms[0];
    }

    public Barn WhereShouldIBarn(TradeCity currentCity)
    {
        return currentCity.Barns[0];
    }

    public ItemType WhatShouldIFarm()
    {
        if (Wheat)
        {
            Wheat = false;
            Barley = true;
            return ItemType.WHEAT;
        }
        else if (Barley)
        {
            Barley = false;
            Wheat = true;
            return ItemType.BARLEY;
        }
        return ItemType.INVALID;
    }
}
