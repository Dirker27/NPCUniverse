using UnityEngine;
using System.Collections.Generic;

public class FarmOracle : MonoBehaviour
{
    private bool debug = false;

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

    public TradeOrders WhatShouldISell(TradeCity currentCity, List<TradeItem> manifest)
    {
        List<TradeItem> toSell = new List<TradeItem>();

        foreach (TradeData data in currentCity.MarketPlace.TradeDataManifest)
        {
            foreach (TradeItem item in manifest)
            {
                if (item.Type == data.Item)
                {
                    if (data.CurrentCost() > item.PurchasedPrice)
                    {
                        toSell.Add(item);
                        Log("Decided to sell:" + item.Type + " at " + data.CurrentCost() + " bought it at " + item.PurchasedPrice + " for a profit of " + (item.PurchasedPrice - data.CurrentCost()));
                    }
                    else
                    {
                        Log("Decided not to sell:" + item.Type + " at " + data.CurrentCost() + " bought it at " + item.PurchasedPrice + " for a loss of " + (data.CurrentCost() - item.PurchasedPrice));
                    }
                }
            }
        }

        TradeOrders tradeOrder = new TradeOrders(null, toSell);

        return tradeOrder;
    }
}
