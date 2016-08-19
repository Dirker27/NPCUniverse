using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


class Fisherman : NonPlayableCharacter
{
    private FishermanOracle fishermanOracle;
    private TradeOracle tradeOracle;

    public Pond destinationPond;

    public Barn destinationBarn;

    public bool destinationIsPond = false;
    public bool destinationIsBarn = false;

    void Log(string s)
    {
        if (debug)
        {
            Debug.Log("Fisherman log <" + s + ">");
        }
    }
    void Start()
    {
        base.Start();
        sheet.inventory = GetComponent<Inventory>();
        sheet.inventory.items = new Dictionary<Item, int>();
        sheet.tradeOracle = GameObject.FindGameObjectWithTag("GameManager").GetComponent<TradeOracle>();
        this.fishermanOracle = GameObject.FindGameObjectWithTag("GameManager").GetComponent<FishermanOracle>();
        sheet.destinationIsBaseCity = true;
    }

    void Update()
    {
        if (!GetComponent<CharacterMovement>().isInTransit())
        {
            if (sheet.destinationIsBaseCity)
            {
                sheet.destinationIsBaseCity = false;
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
                Dictionary<Item, int> peek = sheet.inventory.SeeContents();
                foreach(Item key in peek.Keys)
                {
                    if (key.Type == ItemType.FISH)
                    {
                        destinationBarn.Deposit(key);
                        sheet.inventory.Remove(key);
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

        destinationPond = oracle.WhereShouldIFish(sheet.baseCity);
        destinationBarn = oracle.WhereShouldIStore(sheet.baseCity);

        Log("Destination pond:" + destinationPond);
        Log("Destination barn:" + destinationBarn);

        GetComponent<CharacterMovement>().destination = destinationPond.gameObject.GetComponent<NavigationWaypoint>();
        Log("End FindPondAndSetDestination");
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
        foreach(Item sold in orders.Manifests.Keys)
        {
            foreach (Item toRemove in sheet.inventory.items.Keys)
            {
                if (sold == toRemove)
                {
                    sheet.inventory.items.Remove(toRemove);
                    break;
                }
            }
        }
        Log("Items after sale:" + Item.ListToString(sheet.inventory.items));
        Log("End SellGoods");
    }

    public void PondAction()
    {
        ItemType result = destinationPond.FishPond();

        Item workedItem = GameObject.FindGameObjectWithTag("GameManager").AddComponent<Item>();

        workedItem.Type = result;
        workedItem.PurchasedPrice = 0;

        sheet.inventory.Add(workedItem);
    }
}
