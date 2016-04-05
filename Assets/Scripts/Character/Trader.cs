using UnityEngine;
using System;
using System.Collections;


class Trader : NonPlayableCharacter
{
    private Inventory inventory;
    private TradeOracle oracle;

    public TradeCity currentCity;
    public TradeCity destinationCity;

    private bool debug = false;

    void Log(string s)
    {
        if (debug)
        {
            Debug.Log("Trade log <" +s + ">");
        }
    }
    void Start()
    {
        this.inventory = GetComponent<Inventory>();
        this.oracle = GameObject.FindGameObjectWithTag("GameManager").GetComponent<TradeOracle>();
    }

    void Update()
    {
        if (! GetComponent<CharacterMovement>().isInTransit())
        {
            currentCity = destinationCity;
            SellGoods(this.oracle);
            BuyGoodsAndSetDestination(this.oracle);
        }
    }

    public void BuyGoodsAndSetDestination(TradeOracle oracle)
    {
        Log("Start BuyGoodsAndSetDestination");
        TradeOrders orders = oracle.WhatShouldIBuy(inventory, currentCity, currentCity.MarketPlace.TradeRoutes);
        Log("Current city:" + currentCity);
        destinationCity = orders.Destination.CityOne;
        if (currentCity == orders.Destination.CityOne)
        {
            destinationCity = orders.Destination.CityToo;
        }
        Log("Destination city:" + destinationCity);

        Log("Starting currency:" + inventory.currency);
        inventory.currency -= currentCity.MarketPlace.BuyThese(orders.Manifests);
        Log("After trade currency:" + inventory.currency);
        
        Log("Items before purchase:" + TradeItem.ListToString(inventory.items));
        inventory.items.AddRange(orders.Manifests);
        Log("Items after purchase:" + TradeItem.ListToString(inventory.items));

        GetComponent<CharacterMovement>().destination = destinationCity.gameObject.GetComponent<NavigationWaypoint>();
        Log("End BuyGoodsAndSetDestination");
    }

    public void SellGoods(TradeOracle oracle)
    {
        Log("Start SellGoods at " + currentCity);
        TradeOrders orders = oracle.WhatShouldISell(currentCity, inventory.items);

        Log("Before trade currency:" + inventory.currency);
        inventory.currency += currentCity.MarketPlace.SellThese(orders.Manifests);
        Log("After trade currency:" + inventory.currency);

        Log("Items before sale:" + TradeItem.ListToString(inventory.items));
        Log("Items to sell:" + TradeItem.ListToString(orders.Manifests));
        foreach(TradeItem sold in orders.Manifests)
        {
            foreach(TradeItem toRemove in inventory.items)
            {
                if (sold == toRemove)
                {
                    inventory.items.Remove(toRemove);
                    break;
                }
            }
        }
        Log("Items after sale:" + TradeItem.ListToString(inventory.items));
        Log("End SellGoods");
    }
}
