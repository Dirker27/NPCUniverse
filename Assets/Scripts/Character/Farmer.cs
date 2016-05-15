using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


class Farmer : NonPlayableCharacter
{
    private Inventory inventory;
    private FarmOracle farmOracle;
    private TradeOracle tradeOracle;

    public TradeCity baseCity;

    public Farm destinationFarm;

    private bool debug = false;

    public bool travelingToFarm = false;

    void Log(string s)
    {
        if (debug)
        {
            Debug.Log("Farmer log <" + s + ">");
        }
    }
    void Start()
    {
        this.inventory = GetComponent<Inventory>();
        this.inventory.items = new Dictionary<TradeItem, int>();
        this.tradeOracle = GameObject.FindGameObjectWithTag("GameManager").GetComponent<TradeOracle>();
        this.farmOracle = GameObject.FindGameObjectWithTag("GameManager").GetComponent<FarmOracle>();
    }

    void Update()
    {
        if (! GetComponent<CharacterMovement>().isInTransit())
        {
            if (! travelingToFarm)
            {
                SellGoods(this.tradeOracle);
                FindFarmAndSetDestination(this.farmOracle);
                travelingToFarm = true;
            }
            else
            {
                FarmAction();
                travelingToFarm = false;
            }
        }
    }

    public void FindFarmAndSetDestination(FarmOracle oracle)
    {
        Log("Start FindFarmAndSetDestination");
        
        destinationFarm = oracle.WhereShouldIFarm(baseCity);

        Log("Destination farm:" + destinationFarm);

        GetComponent<CharacterMovement>().destination = destinationFarm.gameObject.GetComponent<NavigationWaypoint>();
        Log("End FindFarmAndSetDestination");
    }

    public void SellGoods(TradeOracle oracle)
    {
        Log("Start SellGoods at " + baseCity);
        TradeOrders orders = oracle.WhatShouldISell(baseCity, inventory.items);

        Log("Before trade currency:" + inventory.currency);
        inventory.currency += baseCity.MarketPlace.SellThese(orders.Manifests);
        Log("After trade currency:" + inventory.currency);

        Log("Items before sale:" + TradeItem.ListToString(inventory.items));
        Log("Items to sell:" + TradeItem.ListToString(orders.Manifests));
        foreach(TradeItem sold in orders.Manifests.Keys)
        {
            foreach(TradeItem toRemove in inventory.items.Keys)
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

    public void FarmAction()
    {
        ItemType result = destinationFarm.WorkFarm();

        TradeItem workedItem = GameObject.FindGameObjectWithTag("GameManager").AddComponent<TradeItem>();

        workedItem.Type = result;
        workedItem.PurchasedPrice = 0;

        inventory.items.Add(workedItem, 1);

        GetComponent<CharacterMovement>().destination = baseCity.gameObject.GetComponent<NavigationWaypoint>();
    }
}
