using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

class Trader : NonPlayableCharacter
{
    private TradeOracle oracle;

    public TradeCity currentCity;
    public TradeCity destinationCity;

    void Log(string s)
    {
        if (debug)
        {
            Debug.Log("Trader log <" +s + ">");
        }
    }
    void Start()
    {
        sheet.inventory = new Inventory();
        sheet.inventory.items = new List<Item>();
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
        TradeOrders orders = oracle.WhatShouldIBuy(sheet.inventory, currentCity, currentCity.MarketPlace.TradeRoutes);
        Log("Current city:" + currentCity);
        destinationCity = orders.Destination.CityOne;
        if (currentCity == orders.Destination.CityOne)
        {
            destinationCity = orders.Destination.CityToo;
        }
        Log("Destination city:" + destinationCity);

        Log("Starting currency:" + sheet.inventory.currency);
        sheet.inventory.currency -= currentCity.MarketPlace.BuyThese(orders.Manifests);
        Log("After trade currency:" + sheet.inventory.currency);

        Log("Items before purchase:" + Item.ListToString(sheet.inventory.items));
        sheet.inventory.AddCollection(orders.Manifests);
        Log("Items after purchase:" + Item.ListToString(sheet.inventory.items));

        GetComponent<CharacterMovement>().destination = destinationCity.gameObject.GetComponent<NavigationWaypoint>();
        Log("End BuyGoodsAndSetDestination");
    }

    public void SellGoods(TradeOracle oracle)
    {
        //Log("Start SellGoods at " + currentCity);
        //TradeOrders orders = oracle.WhatShouldISell(currentCity, sheet.inventory.items);

        //Log("Before trade currency:" + sheet.inventory.currency);
        //sheet.inventory.currency += currentCity.MarketPlace.SellThese(orders.Manifests);
        //Log("After trade currency:" + sheet.inventory.currency);

        //Log("Items before sale:" + Item.ListToString(sheet.inventory.items));
        //Log("Items to sell:" + Item.ListToString(orders.Manifests));
        //sheet.inventory.RemoveCollection(orders.Manifests);
        //Log("Items after sale:" + Item.ListToString(sheet.inventory.items));
        //Log("End SellGoods");
    }
}
