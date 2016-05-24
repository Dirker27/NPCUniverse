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
    public OreShop destinationOreShop;

    private bool debug = false;

    public bool destinationIsBaseCity = false;
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
        this.inventory = GetComponent<Inventory>();
        this.inventory.items = new Dictionary<Item, int>();
        this.tradeOracle = GameObject.FindGameObjectWithTag("GameManager").GetComponent<TradeOracle>();
        this.foundryOracle = GameObject.FindGameObjectWithTag("GameManager").GetComponent<FoundryOracle>();

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
                    inventory.Add(ore);
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
        
        destinationFoundry = oracle.WhereShouldISmith(baseCity);
        destinationOreShop = oracle.WhereShouldIShop(baseCity);

        Log("Destination smith:" + destinationFoundry);
        
        Log("End FindSmithAndSetDestination");
    }

    public void SellGoods(TradeOracle oracle)
    {
        Log("Start SellGoods at " + baseCity);
        TradeOrders orders = oracle.WhatShouldISell(baseCity, inventory.items);

        Log("Before trade currency:" + inventory.currency);
        inventory.currency += baseCity.MarketPlace.SellThese(orders.Manifests);
        Log("After trade currency:" + inventory.currency);

        Log("Items before sale:" + Item.ListToString(inventory.items));
        Log("Items to sell:" + Item.ListToString(orders.Manifests));
        foreach (Item sold in orders.Manifests.Keys)
        {
            foreach (Item toRemove in inventory.items.Keys)
            {
                if (sold == toRemove)
                {
                    inventory.Remove(toRemove);
                    break;
                }
            }
        }
        Log("Items after sale:" + Item.ListToString(inventory.items));
        Log("End SellGoods");
    }

    public void FoundryAction()
    {
        Log("Start FoundryAction at " + destinationFoundry);
        foreach (Item item in inventory.items.Keys)
        {
            if (item.Type == ItemType.ORE)
            {
                
                Item ore = item;

                ItemType result = destinationFoundry.WorkFoundry(ore);
                Log("Item received is :" + result);

                Log("Items before removal:" + Item.ListToString(inventory.items));
                inventory.Remove(ore);
                Log("Items after removal:" + Item.ListToString(inventory.items));


                Item workedItem = GameObject.FindGameObjectWithTag("GameManager").AddComponent<Item>();

                workedItem.Type = result;
                workedItem.PurchasedPrice = 0;

                Log("Items before add:" + Item.ListToString(inventory.items));
                inventory.Add(workedItem);
                Log("Items after add:" + Item.ListToString(inventory.items));

                destinationFoundry.Deposit(workedItem);
                GetComponent<CharacterMovement>().destination = baseCity.gameObject.GetComponent<NavigationWaypoint>();
                Log("End FoundryAction");

                return;
            }

        }        
    }
}
