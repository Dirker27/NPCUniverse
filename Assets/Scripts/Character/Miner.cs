using UnityEngine;
using System;
using System.Collections;


class Miner : NonPlayableCharacter
{
    private Inventory inventory;
    private MineOracle mineOracle;
    private TradeOracle tradeOracle;

    public TradeCity baseCity;

    public Mine destinationMine;

    private bool debug = false;

    public bool travelingToMine = false;

    void Log(string s)
    {
        if (debug)
        {
            Debug.Log("Miner log <" + s + ">");
        }
    }
    void Start()
    {
        this.inventory = GetComponent<Inventory>();
        this.tradeOracle = GameObject.FindGameObjectWithTag("GameManager").GetComponent<TradeOracle>();
        this.mineOracle = GameObject.FindGameObjectWithTag("GameManager").GetComponent<MineOracle>();
    }

    void Update()
    {
        if (! GetComponent<CharacterMovement>().isInTransit())
        {
            if (!travelingToMine)
            {
                SellGoods(this.tradeOracle);
                FindMineAndSetDestination(this.mineOracle);
                travelingToMine = true;
            }
            else
            {
                MineAction();
                travelingToMine = false;
            }
        }
    }

    public void FindMineAndSetDestination(MineOracle oracle)
    {
        Log("Start FindMineAndSetDestination");
        
        destinationMine = oracle.WhereShouldIMine(baseCity);

        Log("Destination mine:" + destinationMine);

        GetComponent<CharacterMovement>().destination = destinationMine.gameObject.GetComponent<NavigationWaypoint>();
        Log("End FindMineAndSetDestination");
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
        ItemType result = destinationMine.WorkMine();

        TradeItem workedItem = GameObject.FindGameObjectWithTag("GameManager").AddComponent<TradeItem>();

        workedItem.Type = result;
        workedItem.PurchasedPrice = 0;

        inventory.items.Add(workedItem);

        GetComponent<CharacterMovement>().destination = baseCity.gameObject.GetComponent<NavigationWaypoint>();
    }
}
