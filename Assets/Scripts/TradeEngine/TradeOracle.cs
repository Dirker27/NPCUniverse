﻿using UnityEngine;
using System.Collections.Generic;

public class TradeOracle : MonoBehaviour
{
    private bool debug = false;

    void Log(string s)
    {
        if (debug)
        {
            Debug.Log("TradeOracle log <" + s + ">");
        }
    }

    public TradeOrders WhatShouldIBuy(Inventory traderInventory, TradeCity currentCity, List<TradeRoute> avaliableTradeRoutes)
    {
        int bestProfit = 0;

        TradeItem bestItem = GameObject.FindGameObjectWithTag("GameManager").AddComponent<TradeItem>();
        int canAffordOfBestItem = 0;
        TradeRoute bestRoute = avaliableTradeRoutes[0];
        int purchasedPrice = 0;

        foreach(TradeRoute route in avaliableTradeRoutes)
        {
            TradeCity destination = route.CityOne;
            if (route.CityOne == currentCity)
            {
                destination = route.CityToo;
            }

            Log("Assesing city:" + destination);

            foreach(TradeData currentTradeData in currentCity.MarketPlace.TradeDataManifest)
            {
                if (currentTradeData.CurrentCost() < traderInventory.currency)
                {
                    Log("Can Afford " + currentTradeData.ToString());
                    foreach (TradeData destinationTradeData in destination.MarketPlace.TradeDataManifest)
                    {
                        if (currentTradeData.Item == destinationTradeData.Item)
                        {
                            if (destinationTradeData.CurrentCost() - currentTradeData.CurrentCost() > bestProfit)
                            {
                                Log("Can make a new best profit at :" + (destinationTradeData.CurrentCost() - currentTradeData.CurrentCost()) + " better then :" + bestProfit);
                                bestProfit = destinationTradeData.CurrentCost() - currentTradeData.CurrentCost();
                                bestItem.Type = currentTradeData.Item;
                                canAffordOfBestItem = traderInventory.currency / currentTradeData.CurrentCost();
                                bestRoute = route;
                                purchasedPrice = currentTradeData.CurrentCost();
                            }
                            else
                            {
                                Log("Can not make a new best profit at :" + (destinationTradeData.CurrentCost() - currentTradeData.CurrentCost()) + " worse then :" + bestProfit);
                            }
                        }
                    }
                }
                else
                {
                    Log("Can Not Afford " + currentTradeData.ToString());
                }
            }
        }

            
        List<TradeItem> manifest = new List<TradeItem>();
        bestItem.PurchasedPrice = purchasedPrice;
        
        for (int i = 0; i < canAffordOfBestItem; i ++)
        {
            manifest.Add(bestItem);
        }
       

        TradeOrders tradeOrder = new TradeOrders(bestRoute, manifest);
        Log("Decided on " + canAffordOfBestItem + " of " + bestItem.Type + " at " + bestItem.PurchasedPrice + " for a gain of " + bestProfit);
        return tradeOrder;
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
