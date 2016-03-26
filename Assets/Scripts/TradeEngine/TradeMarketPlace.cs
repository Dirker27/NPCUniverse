using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TradeMarketPlace : MonoBehaviour
{
    public List<TradeData> TradeDataManifest;
    public List<TradeRoute> TradeRoutes;

    public int BuyThese(List<TradeItem> manifest)
    {
        int cost = 0;
        Dictionary<TradeData, int> trades = new Dictionary<TradeData, int>();

        foreach(TradeItem good in manifest)
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

    public int SellThese(List<TradeItem> manifest)
    {
        int profit = 0;

        Dictionary<TradeData, int> trades = new Dictionary<TradeData, int>();

        foreach (TradeItem good in manifest)
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
