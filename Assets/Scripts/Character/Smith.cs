using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


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
        this.inventory.items = new Dictionary<TradeItem, int>();
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
                FoundryAction();
                travelingToSmith = false;
            }
        }
    }

    public void FindSmithAndSetDestination(FoundryOracle oracle)
    {
        Log("Start FindSmithAndSetDestination");
        
        destinationFoundry = oracle.WhereShouldISmith(baseCity);

        Log("Destination smith:" + destinationFoundry);

        Dictionary<TradeItem, int> oreToWork = new Dictionary<TradeItem,int>();
        TradeItem ore = GameObject.FindGameObjectWithTag("GameManager").AddComponent<TradeItem>();
        ore.Type = ItemType.RAWGOOD;

        oreToWork.Add(ore, 1);

        Log("Starting currency:" + inventory.currency);
        inventory.currency -= baseCity.MarketPlace.BuyThese(oreToWork);
        Log("After trade currency:" + inventory.currency);

        
        Log("Items before purchase:" + TradeItem.ListToString(inventory.items));
        inventory.items.Add(ore, 1);
        Log("Items after purchase:" + TradeItem.ListToString(inventory.items));

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
        foreach (TradeItem sold in orders.Manifests.Keys)
        {
            foreach (TradeItem toRemove in inventory.items.Keys)
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

    public void FoundryAction()
    {
        Log("Start FoundryAction at " + destinationFoundry);
        foreach (TradeItem item in inventory.items.Keys)
        {
            if (item.Type == ItemType.RAWGOOD)
            {
                
                TradeItem ore = item;

                ItemType result = destinationFoundry.WorkFoundry(ore);
                Log("Item received is :" + result);

                Log("Items before removal:" + TradeItem.ListToString(inventory.items));
                inventory.items.Remove(ore);
                Log("Items after removal:" + TradeItem.ListToString(inventory.items));


                TradeItem workedItem = GameObject.FindGameObjectWithTag("GameManager").AddComponent<TradeItem>();

                workedItem.Type = result;
                workedItem.PurchasedPrice = 0;

                Log("Items before add:" + TradeItem.ListToString(inventory.items));
                inventory.items.Add(workedItem, 1);
                Log("Items after add:" + TradeItem.ListToString(inventory.items));

                GetComponent<CharacterMovement>().destination = baseCity.gameObject.GetComponent<NavigationWaypoint>();
                Log("End FoundryAction");

                return;
            }

        }        
    }
}
