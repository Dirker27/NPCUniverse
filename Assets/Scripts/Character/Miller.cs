using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


class Miller : NonPlayableCharacter
{
    private Inventory inventory;
    private MillOracle millOracle;
    private TradeOracle tradeOracle;

    public TradeCity baseCity;

    public Mill destinationMill;
    public Barn destinationBarn;

    private bool debug = false;

    public bool destinationIsBaseCity = false;
    public bool destinationIsMill = false;
    public bool destinationIsBarn = false;

    void Log(string s)
    {
        if (debug)
        {
            Debug.Log("Mill log <" + s + ">");
        }
    }
    void Start()
    {
        this.inventory = GetComponent<Inventory>();
        this.inventory.items = new Dictionary<TradeItem, int>();
        this.tradeOracle = GameObject.FindGameObjectWithTag("GameManager").GetComponent<TradeOracle>();
        this.millOracle = GameObject.FindGameObjectWithTag("GameManager").GetComponent<MillOracle>();

        destinationIsBaseCity = true;
    }

    void Update()
    {
        if (! GetComponent<CharacterMovement>().isInTransit())
        {
            if (destinationIsBaseCity)
            {
                destinationIsBaseCity = false;

                SellGoods(this.tradeOracle);
                FindMillAndSetDestination(this.millOracle);

                destinationIsBarn = true;
                GetComponent<CharacterMovement>().destination = destinationBarn.gameObject.GetComponent<NavigationWaypoint>();
            }
            else if (destinationIsMill)
            {
                destinationIsMill = false;

                MillAction();

                destinationIsBarn = true;
                GetComponent<CharacterMovement>().destination = destinationBarn.gameObject.GetComponent<NavigationWaypoint>();
            }
            else if (destinationIsBarn)
            {
                destinationIsBarn = false;
                Inventory magazine = destinationBarn.PeekContents();
                Dictionary<TradeItem, int> contents = magazine.SeeContents();

                TradeItem wheat = GameObject.FindGameObjectWithTag("GameManager").AddComponent<TradeItem>();
                bool foundWheat = false;
                foreach(TradeItem item in contents.Keys)
                {
                    if (item.Type == ItemType.WHEAT)
                    {
                        wheat.Type = item.Type;
                        wheat.PurchasedPrice = item.PurchasedPrice;
                        foundWheat = true;
                    }
                }
                if (foundWheat)
                {
                    inventory.Add(wheat);
                    destinationBarn.Withdraw(wheat);
                }

                destinationIsMill = true;
                GetComponent<CharacterMovement>().destination = destinationMill.gameObject.GetComponent<NavigationWaypoint>();
            }
        }
    }

    public void FindMillAndSetDestination(MillOracle oracle)
    {
        Log("Start FindMillAndSetDestination");
        
        destinationMill = oracle.WhereShouldIMill(baseCity);
        destinationBarn = oracle.WhereShouldIShop(baseCity);

        Log("Destination mill:" + destinationMill);
        
        Log("End FindMillAndSetDestination");
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

    public void MillAction()
    {
        Log("Start MillAction at " + destinationMill);
        foreach (TradeItem item in inventory.items.Keys)
        {
            if (item.Type == ItemType.WHEAT)
            {
                
                TradeItem wheat = item;

                ItemType result = destinationMill.WorkMill(wheat);
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

                GetComponent<CharacterMovement>().destination = baseCity.gameObject.GetComponent<NavigationWaypoint>();
                Log("End FoundryAction");

                return;
            }

        }        
    }
}
