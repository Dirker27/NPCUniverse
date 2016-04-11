using UnityEngine;
using System;
using System.Collections;


class Smith : NonPlayableCharacter
{
    private Inventory inventory;
    private FoundryOracle foundryOracle;
    private TradeOracle tradeOracle;

    public TradeCity baseCity;

    public Foundry destinationFoundry;

    private bool debug = false;

    public bool travelingToSmith = false;

    void Log(string s)
    {
        if (debug)
        {
            Debug.Log("Smith log <" + s + ">");
        }
    }
    void Start()
    {
        this.inventory = GetComponent<Inventory>();
        this.tradeOracle = GameObject.FindGameObjectWithTag("GameManager").GetComponent<TradeOracle>();
        this.foundryOracle = GameObject.FindGameObjectWithTag("GameManager").GetComponent<FoundryOracle>();
    }

    void Update()
    {
        if (! GetComponent<CharacterMovement>().isInTransit())
        {
            if (!travelingToSmith)
            {
                SellGoods(this.tradeOracle);
                FindSmithAndSetDestination(this.foundryOracle);
                travelingToSmith = true;
            }
            else
            {
                MineAction();
                travelingToSmith = false;
            }
        }
    }

    public void FindSmithAndSetDestination(FoundryOracle oracle)
    {
        Log("Start FindSmithAndSetDestination");
        
        destinationFoundry = oracle.WhereShouldISmith(baseCity);

        Log("Destination smith:" + destinationFoundry);

        GetComponent<CharacterMovement>().destination = destinationFoundry.gameObject.GetComponent<NavigationWaypoint>();
        Log("End FindSmithAndSetDestination");
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
        foreach (TradeItem sold in orders.Manifests)
        {
            foreach (TradeItem toRemove in inventory.items)
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

    public void MineAction()
    {
        ItemType result = destinationFoundry.WorkFoundry();

        TradeItem workedItem = GameObject.FindGameObjectWithTag("GameManager").AddComponent<TradeItem>();

        workedItem.Type = result;
        workedItem.PurchasedPrice = 0;

        inventory.items.Add(workedItem);

        GetComponent<CharacterMovement>().destination = baseCity.gameObject.GetComponent<NavigationWaypoint>();
    }
}
