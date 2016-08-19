using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


class Smith : NonPlayableCharacter
{
    private FoundryOracle foundryOracle;
    private TradeOracle tradeOracle;


    public Foundry destinationFoundry;
    public OreShop destinationOreShop;

    public bool destinationIsFoundry = false;
    public bool destinationIsOreShop = false;

    void Log(string s)
    {
        if (debug)
        {
            Debug.Log("Smith log <" + s + ">");
        }
    }
    void Start()
    {
        base.Start();
        sheet.inventory = GetComponent<Inventory>();
        sheet.inventory.items = new Dictionary<Item, int>();
        this.tradeOracle = GameObject.FindGameObjectWithTag("GameManager").GetComponent<TradeOracle>();
        this.foundryOracle = GameObject.FindGameObjectWithTag("GameManager").GetComponent<FoundryOracle>();

        sheet.destinationIsBaseCity = true;
    }

    void Update()
    {
        if (! GetComponent<CharacterMovement>().isInTransit())
        {
            if (sheet.destinationIsBaseCity)
            {
                sheet.destinationIsBaseCity = false;

                SellGoods(this.tradeOracle);
                FindSmithAndSetDestination(this.foundryOracle);

                destinationIsOreShop = true;
                GetComponent<CharacterMovement>().destination = destinationOreShop.gameObject.GetComponent<NavigationWaypoint>();
            }
            else if (destinationIsFoundry)
            {
                destinationIsFoundry = false;

                FoundryAction();

                destinationIsOreShop = true;
                GetComponent<CharacterMovement>().destination = destinationOreShop.gameObject.GetComponent<NavigationWaypoint>();
            }
            else if (destinationIsOreShop)
            {
                destinationIsOreShop = false;
                Inventory magazine = destinationOreShop.PeekContents();
                Dictionary<Item, int> contents = magazine.SeeContents();

                Item ore = GameObject.FindGameObjectWithTag("GameManager").AddComponent<Item>();
                bool foundOre = false;
                foreach(Item item in contents.Keys)
                {
                    if (item.Type == ItemType.ORE)
                    {
                        ore.Type = item.Type;
                        ore.PurchasedPrice = item.PurchasedPrice;
                        foundOre = true;
                    }
                }
                if (foundOre)
                {
                    sheet.inventory.Add(ore);
                    destinationOreShop.Withdraw(ore);
                }

                destinationIsFoundry = true;
                GetComponent<CharacterMovement>().destination = destinationFoundry.gameObject.GetComponent<NavigationWaypoint>();
            }
        }
    }

    public void FindSmithAndSetDestination(FoundryOracle oracle)
    {
        Log("Start FindSmithAndSetDestination");

        destinationFoundry = oracle.WhereShouldISmith(sheet.baseCity);
        destinationOreShop = oracle.WhereShouldIShop(sheet.baseCity);

        Log("Destination smith:" + destinationFoundry);
        
        Log("End FindSmithAndSetDestination");
    }

    public void SellGoods(TradeOracle oracle)
    {
        Log("Start SellGoods at " + sheet.baseCity);
        TradeOrders orders = oracle.WhatShouldISell(sheet.baseCity, sheet.inventory.items);

        Log("Before trade currency:" + sheet.inventory.currency);
        sheet.inventory.currency += sheet.baseCity.MarketPlace.SellThese(orders.Manifests);
        Log("After trade currency:" + sheet.inventory.currency);

        Log("Items before sale:" + Item.ListToString(sheet.inventory.items));
        Log("Items to sell:" + Item.ListToString(orders.Manifests));
        foreach (Item sold in orders.Manifests.Keys)
        {
            foreach (Item toRemove in sheet.inventory.items.Keys)
            {
                if (sold == toRemove)
                {
                    sheet.inventory.Remove(toRemove);
                    break;
                }
            }
        }
        Log("Items after sale:" + Item.ListToString(sheet.inventory.items));
        Log("End SellGoods");
    }

    public void FoundryAction()
    {
        Log("Start FoundryAction at " + destinationFoundry);
        foreach (Item item in sheet.inventory.items.Keys)
        {
            if (item.Type == ItemType.ORE)
            {
                
                Item ore = item;

                ItemType result = destinationFoundry.WorkFoundry(ore);
                Log("Item received is :" + result);

                Log("Items before removal:" + Item.ListToString(sheet.inventory.items));
                sheet.inventory.Remove(ore);
                Log("Items after removal:" + Item.ListToString(sheet.inventory.items));


                Item workedItem = GameObject.FindGameObjectWithTag("GameManager").AddComponent<Item>();

                workedItem.Type = result;
                workedItem.PurchasedPrice = 0;

                Log("Items before add:" + Item.ListToString(sheet.inventory.items));
                sheet.inventory.Add(workedItem);
                Log("Items after add:" + Item.ListToString(sheet.inventory.items));

                destinationFoundry.Deposit(workedItem);
                GetComponent<CharacterMovement>().destination = sheet.baseCity.gameObject.GetComponent<NavigationWaypoint>();
                Log("End FoundryAction");

                return;
            }

        }        
    }
}
