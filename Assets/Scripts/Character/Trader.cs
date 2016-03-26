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
        Debug.Log("sup?");
        this.inventory = GetComponent<Inventory>();
        this.oracle = GameObject.FindGameObjectWithTag("GameManager").GetComponent<TradeOracle>();
    }

    void Update()
    {
        Debug.Log("bro?");
        if (! GetComponent<CharacterMovement>().isInTransit())
        {
            Debug.Log("broseph?");
            BuyGoodsAndSetDestination(this.oracle);
        }
    }

    public void BuyGoodsAndSetDestination(TradeOracle oracle)
    {
        if (oracle == null)
        {
            Debug.Log("No Trade Oracle - FUUUUUUK");
        }
        TradeOrders orders = oracle.WhatShouldIBuy(inventory, currentCity, currentCity.MarketPlace.TradeRoutes);
            
        destinationCity = orders.Destination.CityOne;
        if (currentCity == orders.Destination.CityOne)
        {
            destinationCity = orders.Destination.CityToo;
        }
        inventory.currency -= currentCity.MarketPlace.BuyThese(orders.Manifests);
        inventory.items.AddRange(orders.Manifests);


        GetComponent<CharacterMovement>().destination = destinationCity.gameObject.GetComponent<NavigationWaypoint>();
    }

    public void SellGoods(TradeOracle oracle)
    {
        TradeOrders orders = oracle.WhatShouldISell(currentCity, inventory.items);

        inventory.currency += currentCity.MarketPlace.SellThese(orders.Manifests);
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
    }
}
