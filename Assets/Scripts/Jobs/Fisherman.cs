using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


class Fisherman : NonPlayableCharacter
{
    private Inventory inventory;
    private FishermanOracle fishermanOracle;
    private TradeOracle tradeOracle;

    public TradeCity baseCity;

    public Pond destinationPond;

    public Barn destinationBarn;

    private bool debug = false;

    public bool destinationIsPond = false;
    public bool destinationIsBarn = false;
    public bool destinationIsCity = false;

    void Log(string s)
    {
        if (debug)
        {
            Debug.Log("Fisherman log <" + s + ">");
        }
    }
    void Start()
    {
        this.inventory = GetComponent<Inventory>();
        this.inventory.items = new Dictionary<Item, int>();
        this.tradeOracle = GameObject.FindGameObjectWithTag("GameManager").GetComponent<TradeOracle>();
        this.fishermanOracle = GameObject.FindGameObjectWithTag("GameManager").GetComponent<FishermanOracle>();
        destinationIsCity = true;
    }

    void Update()
    {
        if (!GetComponent<CharacterMovement>().isInTransit())
        {
            if (destinationIsCity)
            {
                destinationIsCity = false;
                SellGoods(this.tradeOracle);
                FindPondAndSetDestination(this.fishermanOracle);
                destinationIsPond = true;
            }
            else if (destinationIsPond)
            {
                destinationIsPond = false;

                PondAction();

                GetComponent<CharacterMovement>().destination = destinationBarn.gameObject.GetComponent<NavigationWaypoint>();
                destinationIsBarn = true;
                return;
            }
            else if (destinationIsBarn)
            {
                destinationIsBarn = false;
                Dictionary<Item, int> peek = inventory.SeeContents();
                foreach(Item key in peek.Keys)
                {
                    if (key.Type == ItemType.FISH)
                    {
                        destinationBarn.Deposit(key);
                        inventory.Remove(key);
                    }
                }
                destinationIsPond = true;
                GetComponent<CharacterMovement>().destination = destinationPond.gameObject.GetComponent<NavigationWaypoint>();
            }
        }
    }

    public void FindPondAndSetDestination(FishermanOracle oracle)
    {
        Log("Start FindPondAndSetDestination");
        
        destinationPond = oracle.WhereShouldIFish(baseCity);
        destinationBarn = oracle.WhereShouldIStore(baseCity);

        Log("Destination pond:" + destinationPond);
        Log("Destination barn:" + destinationBarn);

        GetComponent<CharacterMovement>().destination = destinationPond.gameObject.GetComponent<NavigationWaypoint>();
        Log("End FindPondAndSetDestination");
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
        foreach(Item sold in orders.Manifests.Keys)
        {
            foreach(Item toRemove in inventory.items.Keys)
            {
                if (sold == toRemove)
                {
                    inventory.items.Remove(toRemove);
                    break;
                }
            }
        }
        Log("Items after sale:" + Item.ListToString(inventory.items));
        Log("End SellGoods");
    }

    public void PondAction()
    {
        ItemType result = destinationPond.FishPond();

        Item workedItem = GameObject.FindGameObjectWithTag("GameManager").AddComponent<Item>();

        workedItem.Type = result;
        workedItem.PurchasedPrice = 0;

        inventory.Add(workedItem);
    }
}
