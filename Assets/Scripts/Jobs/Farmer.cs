using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


class Farmer : NonPlayableCharacter
{
    private FarmOracle farmOracle;
    private TradeOracle tradeOracle;


    public Farm destinationFarm;

    public Barn destinationBarn;

    public bool destinationIsFarm = false;
    public bool destinationIsBarn = false;

    ItemType crop = ItemType.INVALID;

    void Log(string s)
    {
        if (debug)
        {
            Debug.Log("Farmer log <" + s + ">");
        }
    }
    void Start()
    {
        base.Start();
        sheet.inventory = GetComponent<Inventory>();
        sheet.inventory.items = new Dictionary<Item, int>();
        sheet.tradeOracle = GameObject.FindGameObjectWithTag("GameManager").GetComponent<TradeOracle>();
        this.farmOracle = GameObject.FindGameObjectWithTag("GameManager").GetComponent<FarmOracle>();
        sheet.destinationIsBaseCity = true;
    }

    void Update()
    {
        if (!GetComponent<CharacterMovement>().isInTransit())
        {
            if (sheet.destinationIsBaseCity)
            {
                sheet.destinationIsBaseCity = false;
                FindFarmAndSetDestination(this.farmOracle);
                destinationIsFarm = true;
            }
            else if (destinationIsFarm)
            {
                destinationIsFarm = false;

                FarmAction();

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
                    if (key.Type == ItemType.WHEAT)
                    {
                        destinationBarn.Deposit(key);
                        sheet.inventory.Remove(key);
                    }
                }
                destinationIsFarm = true;
                GetComponent<CharacterMovement>().destination = destinationFarm.gameObject.GetComponent<NavigationWaypoint>();
            }
        }
    }

    public void FindFarmAndSetDestination(FarmOracle oracle)
    {
        Log("Start FindFarmAndSetDestination");

        destinationFarm = oracle.WhereShouldIFarm(sheet.baseCity);
        destinationBarn = oracle.WhereShouldIBarn(sheet.baseCity);
        crop = oracle.WhatShouldIFarm();

        Log("Destination farm:" + destinationFarm);
        Log("Destination barn:" + destinationBarn);

        GetComponent<CharacterMovement>().destination = destinationFarm.gameObject.GetComponent<NavigationWaypoint>();
        Log("End FindFarmAndSetDestination");
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

    public void FarmAction()
    {
        ItemType result = destinationFarm.WorkFarm(crop);

        Item workedItem = GameObject.FindGameObjectWithTag("GameManager").AddComponent<Item>();

        workedItem.Type = result;
        workedItem.PurchasedPrice = 0;

        sheet.inventory.Add(workedItem);
    }
}
