﻿using UnityEngine;
using System.Collections.Generic;

public class BakerOracle : MonoBehaviour
{
    private bool debug = false;

    void Log(string s)
    {
        if (debug)
        {
            Debug.Log("BakerOracle log <" + s + ">");
        }
    }

    public Bakery WhereShouldIBake(TradeCity currentCity)
    {
        return currentCity.Bakeries[0];
    }

    public Mill WhereShouldIMill(TradeCity currentCity)
    {
        return currentCity.Mills[0];
    }
}
