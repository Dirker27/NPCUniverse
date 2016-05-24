﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

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
            Debug.Log("Trader log <" +s + ">");
        }
    }
    void Start()
    {
        this.inventory = GetComponent<Inventory>();
        this.inventory.items = new Dictionary<Item, int>();
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
        
        Log("Items before purchase:" + Item.ListToString(inventory.items));
        inventory.AddCollection(orders.Manifests);
        Log("Items after purchase:" + Item.ListToString(inventory.items));

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

        Log("Items before sale:" + Item.ListToString(inventory.items));
        Log("Items to sell:" + Item.ListToString(orders.Manifests));
        inventory.RemoveCollection(orders.Manifests);
        Log("Items after sale:" + Item.ListToString(inventory.items));
        Log("End SellGoods");
    }
}
