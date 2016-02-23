using UnityEngine;
using System;
using System.Collections;

class Trader : NonPlayableCharacter
{
    private Inventory inventory;
    private TradeOracle oracle;

    public TradeCity currentCity;
    public TradeCity destinationCity;

    void Start()
    {
        Console.Out.WriteLine("sup?");
        this.inventory = GetComponent<Inventory>();
        this.oracle = GameObject.FindGameObjectWithTag("GameManager").GetComponent<TradeOracle>();
        if (this.oracle == null)
        {
            Console.Out.WriteLine("FUUUUUUK");
            throw new Exception("Oh shit.");
        }
    }

    void Update()
    {
        Console.Out.WriteLine("bro?");
        if (! GetComponent<CharacterMovement>().isInTransit())
        {
            BuyGoodsAndSetDestination(oracle);
        }
    }

    public void BuyGoodsAndSetDestination(TradeOracle oracle)
    {
        TradeOrders orders = oracle.WhatShouldIBuy(inventory, currentCity, currentCity.MarketPlace.TradeRoutes);
            
        destinationCity = orders.Destination.CityOne;
        if (currentCity == orders.Destination.CityOne)
        {
            destinationCity = orders.Destination.CityTwo;
        }
        inventory.currency -= currentCity.MarketPlace.BuyThese(orders.Manifests);
        inventory.items.AddRange(orders.Manifests);


        GetComponent<CharacterMovement>().destination = destinationCity.gameObject.GetComponent<NavigationWaypoint>();
    }

    public void SellGoods(TradeOracle oracle)
    {
        TradeOrders orders = oracle.WhatShouldISell(currentCity, inventory.items);

        inventory.currency += currentCity.MarketPlace.SellThese(orders.Manifests);
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
