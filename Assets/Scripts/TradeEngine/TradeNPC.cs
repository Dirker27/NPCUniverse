using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.TradeEngine
{
    class TradeNPC
    {
        public TradeInventory Inventory { get; set; }
        public TradeCity CurrentCity { get; set; }
        public TradeCity DestinationCity { get; set; }
        public TradeNPC(TradeInventory inventory, TradeCity currentCity, TradeCity destinationCity)
        {
            Inventory = inventory;
            CurrentCity = currentCity;
            DestinationCity = destinationCity;
        }

        public void BuyGoodsAndSetDestination(TradeOracle oracle)
        {
            TradeOrders orders = oracle.WhatShouldIBuy(Inventory, CurrentCity, CurrentCity.MarketPlace.TradeRoutes);
            
            DestinationCity = orders.Destination.CityOne;
            if (CurrentCity == orders.Destination.CityOne)
            {
                DestinationCity = orders.Destination.CityTwo;
            }
            Inventory.Currency -= CurrentCity.MarketPlace.BuyThese(orders.Manifests);
            Inventory.Items.AddRange(orders.Manifests);
            
        }

        public void SellGoods(TradeOracle oracle)
        {
            TradeOrders orders = oracle.WhatShouldISell(CurrentCity, Inventory.Items);

            Inventory.Currency += CurrentCity.MarketPlace.SellThese(orders.Manifests);
            foreach(TradeItem sold in orders.Manifests)
            {
                foreach(TradeItem toRemove in Inventory.Items)
                {
                    if (sold == toRemove)
                    {
                        Inventory.Items.Remove(toRemove);
                        break;
                    }
                }
            }
        }
    }
}
