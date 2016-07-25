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

    public Barn destinationBarn;

    private bool debug = false;

    public bool destinationIsFarm = false;
    public bool destinationIsBarn = false;
    public bool destinationIsCity = false;

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
        this.inventory = GetComponent<Inventory>();
        this.inventory.items = new Dictionary<Item, int>();
        this.tradeOracle = GameObject.FindGameObjectWithTag("GameManager").GetComponent<TradeOracle>();
        this.farmOracle = GameObject.FindGameObjectWithTag("GameManager").GetComponent<FarmOracle>();
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
                Dictionary<Item, int> peek = inventory.SeeContents();
                foreach(Item key in peek.Keys)
                {
                    if (key.Type == ItemType.WHEAT)
                    {
                        destinationBarn.Deposit(key);
                        inventory.Remove(key);
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
        
        destinationFarm = oracle.WhereShouldIFarm(baseCity);
        destinationBarn = oracle.WhereShouldIBarn(baseCity);
        crop = oracle.WhatShouldIFarm();

        Log("Destination farm:" + destinationFarm);
        Log("Destination barn:" + destinationBarn);

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

    public void FarmAction()
    {
        ItemType result = destinationFarm.WorkFarm(crop);

        Item workedItem = GameObject.FindGameObjectWithTag("GameManager").AddComponent<Item>();

        workedItem.Type = result;
        workedItem.PurchasedPrice = 0;

        inventory.Add(workedItem);
    }
}
