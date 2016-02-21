using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Trader : NonPlayableCharacter
{
    private Inventory inventory;
    public TradeCity CurrentCity;
    public TradeCity DestinationCity;

    void Start()
    {
        this.inventory = GetComponent<Inventory>();
    }

    public void BuyGoodsAndSetDestination(TradeOracle oracle)
    {
        TradeOrders orders = oracle.WhatShouldIBuy(inventory, CurrentCity, CurrentCity.MarketPlace.TradeRoutes);
            
        DestinationCity = orders.Destination.CityOne;
        if (CurrentCity == orders.Destination.CityOne)
        {
            DestinationCity = orders.Destination.CityTwo;
        }
        inventory.currency -= CurrentCity.MarketPlace.BuyThese(orders.Manifests);
        inventory.items.AddRange(orders.Manifests);
            
    }

    public void SellGoods(TradeOracle oracle)
    {
        TradeOrders orders = oracle.WhatShouldISell(CurrentCity, inventory.items);

        inventory.currency += CurrentCity.MarketPlace.SellThese(orders.Manifests);
        foreach(Item sold in orders.Manifests)
        {
            foreach(Item toRemove in inventory.items)
            {
                if (sold == toRemove)
                {
                    inventory.items.Remove(toRemove);
                    break;
                }
            }
        }
    }
}
