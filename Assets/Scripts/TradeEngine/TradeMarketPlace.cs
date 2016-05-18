using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TradeMarketPlace : MonoBehaviour
{
    public List<TradeData> TradeDataManifest;
    public List<TradeRoute> TradeRoutes;

    private bool debug = false;

    void Log(string s)
    {
        if (debug)
        {
            Debug.Log("TradeMarketPlace log <" + s + ">");
        }
    }

    public int BuyThese(Dictionary<TradeItem, int> manifest)
    {
        int cost = 0;
        Dictionary<TradeData, int> trades = new Dictionary<TradeData, int>();

        foreach(TradeItem good in manifest.Keys)
        {
            foreach(TradeData data in TradeDataManifest)
            {
                Log("data:" + data.Item);
                Log("good:" + good.Type);
                if (good.Type == data.Item)
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

        return cost;
    }

    public int SellThese(Dictionary<TradeItem, int> manifest)
    {
        int profit = 0;

        Dictionary<TradeData, int> trades = new Dictionary<TradeData, int>();

        foreach (TradeItem good in manifest.Keys)
        {
            foreach (TradeData data in TradeDataManifest)
            {
                if (good.Type == data.Item)
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
