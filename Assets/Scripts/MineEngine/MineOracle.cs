using UnityEngine;
using System.Collections.Generic;

public class MineOracle : MonoBehaviour
{
    public Logger logger;
    private bool debug = false;

    void Start()
    {
        this.logger = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Logger>();
    }

    public Mine WhereShouldIMine(TradeCity currentCity)
    {
        return currentCity.Mines[0];
    }

    public OreShop WhereShouldIStore(TradeCity currentCity)
    {
        return currentCity.OreShops[0];
    }
  
    public TradeOrders WhatShouldISell(TradeCity currentCity, Dictionary<TradeItem, int> manifest)
    {
        Dictionary<TradeItem, int> toSell = new Dictionary<TradeItem, int>();

        foreach (TradeData data in currentCity.MarketPlace.TradeDataManifest)
        {
            foreach (TradeItem item in manifest.Keys)
            {
                if (item.Type == data.Item)
                {
                    if (data.CurrentCost() > item.PurchasedPrice)
                    {
                        if (toSell.ContainsKey(item))
                        {
                            toSell[item] += 1;
                        }
                        else
                        {
                            toSell.Add(item, 1);
                        }
                        logger.Log(debug, "Decided to sell:" + item.Type + " at " + data.CurrentCost() + " bought it at " + item.PurchasedPrice + " for a profit of " + (item.PurchasedPrice - data.CurrentCost()));
                    }
                    else
                    {
                        logger.Log(debug, "Decided not to sell:" + item.Type + " at " + data.CurrentCost() + " bought it at " + item.PurchasedPrice + " for a loss of " + (data.CurrentCost() - item.PurchasedPrice));
                    }
                }
            }
        }

        TradeOrders tradeOrder = new TradeOrders(null, toSell);

        return tradeOrder;
    }
}
