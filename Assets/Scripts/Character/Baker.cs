using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


class Baker : NonPlayableCharacter
{
    private Inventory inventory;
    private BakerOracle bakerOracle;
    private TradeOracle tradeOracle;

    public TradeCity baseCity;

    public Bakery destinationBakery;
    public Mill destinationMill;

    private bool debug = false;

    public bool destinationIsBaseCity = false;
    public bool destinationIsBakery = false;
    public bool destinationIsMill = false;

    void Log(string s)
    {
        if (debug)
        {
            Debug.Log("Baker log <" + s + ">");
        }
    }

    void Start()
    {
        this.inventory = GetComponent<Inventory>();
        this.inventory.items = new Dictionary<TradeItem, int>();
        this.tradeOracle = GameObject.FindGameObjectWithTag("GameManager").GetComponent<TradeOracle>();
        this.bakerOracle = GameObject.FindGameObjectWithTag("GameManager").GetComponent<BakerOracle>();

        destinationIsBaseCity = true;
    }

    void Update()
    {
        if (! GetComponent<CharacterMovement>().isInTransit())
        {
            if (destinationIsBaseCity)
            {
                destinationIsBaseCity = false;

                FindBakeryAndSetDestination(this.bakerOracle);

                destinationIsMill = true;
                GetComponent<CharacterMovement>().destination = destinationMill.gameObject.GetComponent<NavigationWaypoint>();
            }
            else if (destinationIsBakery)
            {
                destinationIsBakery = false;

                BakeAction();

                destinationIsMill = true;
                GetComponent<CharacterMovement>().destination = destinationMill.gameObject.GetComponent<NavigationWaypoint>();
            }
            else if (destinationIsMill)
            {
                destinationIsMill = false;
                Inventory magazine = destinationMill.PeekContents();
                Dictionary<TradeItem, int> contents = magazine.SeeContents();

                TradeItem flour = GameObject.FindGameObjectWithTag("GameManager").AddComponent<TradeItem>();
                bool foundFlour = false;
                foreach(TradeItem item in contents.Keys)
                {
                    if (item.Type == ItemType.FLOUR)
                    {
                        flour.Type = item.Type;
                        flour.PurchasedPrice = item.PurchasedPrice;
                        foundFlour = true;
                    }
                }
                if (foundFlour)
                {
                    inventory.Add(flour);
                    destinationMill.Withdraw(flour);
                }

                destinationIsBakery = true;
                GetComponent<CharacterMovement>().destination = destinationBakery.gameObject.GetComponent<NavigationWaypoint>();
            }
        }
    }

    public void FindBakeryAndSetDestination(BakerOracle oracle)
    {
        Log("Start FindBakeryAndSetDestination");
        
        destinationBakery = oracle.WhereShouldIBake(baseCity);
        destinationMill = oracle.WhereShouldIMill(baseCity);

        Log("Destination baker:" + destinationBakery);
        
        Log("End FindBakeryAndSetDestination");
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
                    inventory.Remove(toRemove);
                    break;
                }
            }
        }
        Log("Items after sale:" + TradeItem.ListToString(inventory.items));
        Log("End SellGoods");
    }

    public void BakeAction()
    {
        Log("Start BakeAction at " + destinationMill);
        foreach (TradeItem item in inventory.items.Keys)
        {
            if (item.Type == ItemType.WHEAT)
            {
                
                TradeItem wheat = item;

                ItemType result = destinationBakery.WorkBakery(wheat);
                Log("Item received is :" + result);

                Log("Items before removal:" + TradeItem.ListToString(inventory.items));
                inventory.Remove(wheat);
                Log("Items after removal:" + TradeItem.ListToString(inventory.items));


                TradeItem workedItem = GameObject.FindGameObjectWithTag("GameManager").AddComponent<TradeItem>();

                workedItem.Type = result;
                workedItem.PurchasedPrice = 0;

                Log("Items before add:" + TradeItem.ListToString(inventory.items));
                inventory.Add(workedItem);
                Log("Items after add:" + TradeItem.ListToString(inventory.items));

                destinationBakery.Deposit(workedItem);

                GetComponent<CharacterMovement>().destination = destinationMill.gameObject.GetComponent<NavigationWaypoint>();
                Log("End FoundryAction");

                return;
            }

        }        
    }
}
