using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.TradeEngine
{
    class TradeOracle
    {
        public TradeOracle()
        {

        }

        public TradeOrders WhatShouldIBuy(Inventory traderInventory, City currentCity, List<Route> avaliableTradeRoutes)
        {
            int bestProfit = 0;
            Item bestItem = new Item("Temp");
            int canAffordOfBestItem = 0;
            Route bestRoute = avaliableTradeRoutes[0];
            int purchasedPrice = 0;

            foreach(Route route in avaliableTradeRoutes)
            {
                City destination = route.CityOne;
                if (route.CityOne == currentCity)
                {
                    destination = route.CityTwo;
                }

                foreach(TradeData currentTradeData in currentCity.MarketPlace.TradeDataManifest)
                {
                    if (currentTradeData.CurrentCost() < traderInventory.Currency)
                    {
                        foreach (TradeData destinationTradeData in destination.MarketPlace.TradeDataManifest)
                        {
                            if (currentTradeData.Item == destinationTradeData.Item)
                            {
                                if (destinationTradeData.CurrentCost() - currentTradeData.CurrentCost() > bestProfit)
                                {
                                    bestProfit = destinationTradeData.CurrentCost() - currentTradeData.CurrentCost();
                                    bestItem = currentTradeData.Item;
                                    canAffordOfBestItem = currentTradeData.CurrentCost() / traderInventory.Currency;
                                    bestRoute = route;
                                    purchasedPrice = currentTradeData.CurrentCost();
                                }
                            }
                        }
                    }
                }
            }

            
            List<Item> manifest = new List<Item>();
            bestItem.PurchasedPrice = purchasedPrice;
            for (int i = 0; i < canAffordOfBestItem; i ++)
            {
                manifest.Add(bestItem);
            }

            TradeOrders tradeOrder = new TradeOrders(bestRoute, manifest);

            return tradeOrder;
        }

        public TradeOrders WhatShouldISell(City currentCity, List<Item> manifest)
        {
            List<Item> toSell = new List<Item>();

            foreach (TradeData data in currentCity.MarketPlace.TradeDataManifest)
            {
                foreach (Item item in manifest)
                {
                    if (item == data.Item)
                    {
                        if (data.CurrentCost() < item.PurchasedPrice)
                        {
                            toSell.Add(item);
                        }
                    }
                }
            }

            TradeOrders tradeOrder = new TradeOrders(null, toSell);

            return tradeOrder;
        }
    }
}
