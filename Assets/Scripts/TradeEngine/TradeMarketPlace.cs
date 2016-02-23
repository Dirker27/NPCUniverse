﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class TradeMarketPlace
{
    public List<TradeData> TradeDataManifest { get; set; }
    public List<TradeRoute> TradeRoutes { get; set; }
    public TradeMarketPlace(List<TradeData> tradeDataManifest, List<TradeRoute> tradeRoutes)
    {
        TradeDataManifest = tradeDataManifest;
        TradeRoutes = tradeRoutes;
    }

    public int BuyThese(List<Item> manifest)
    {
        int cost = 0;
        Dictionary<TradeData, int> trades = new Dictionary<TradeData, int>();

        foreach(Item good in manifest)
        {
            foreach(TradeData data in TradeDataManifest)
            {
                if (good == data.Item)
                {
                    cost += data.CurrentCost();
                    if(!trades.ContainsKey(data))
                    {
                        trades[data] = 1;
                    }
                    else
                    {
                        trades[data] += 1;
                    }
                }
            }
        }

        /*foreach (KeyValuePair<TradeData, int> kvp in trades)
        {
            kvp.Key.CurrentAmount -= kvp.Value;
        }*/

        return cost;
    }

    public int SellThese(List<Item> manifest)
    {
        int profit = 0;

        Dictionary<TradeData, int> trades = new Dictionary<TradeData, int>();

        foreach (Item good in manifest)
        {
            foreach (TradeData data in TradeDataManifest)
            {
                if (good == data.Item)
                {
                    profit += data.CurrentCost();
                    if (!trades.ContainsKey(data))
                    {
                        trades[data] = 1;
                    }
                    else
                    {
                        trades[data] += 1;
                    }
                }
            }
        }

        /*foreach (KeyValuePair<TradeData, int> kvp in trades)
        {
            kvp.Key.CurrentAmount += kvp.Value;
        }*/

        return profit;
    }
}
