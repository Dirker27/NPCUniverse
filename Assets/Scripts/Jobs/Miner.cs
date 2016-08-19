using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


class Miner : NonPlayableCharacter
{
    private MineOracle mineOracle;
    private TradeOracle tradeOracle;

    public Mine destinationMine;
    public OreShop destinationOreShop;

    public bool destinationIsMine = false;
    public bool destinationIsOreShop = false;

    private ItemType desiredOre = ItemType.INVALID;

    void Log(string s)
    {
        if (debug)
        {
            Debug.Log("Miner log <" + s + ">");
        }
    }
    void Start()
    {
        base.Start();
        sheet.inventory = GetComponent<Inventory>();
        sheet.inventory.items = new Dictionary<Item, int>();
        this.tradeOracle = GameObject.FindGameObjectWithTag("GameManager").GetComponent<TradeOracle>();
        this.mineOracle = GameObject.FindGameObjectWithTag("GameManager").GetComponent<MineOracle>();
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
                FindMineAndSetDestination(this.mineOracle);
                destinationIsMine = true;
            }
            else if (destinationIsMine)
            {
                destinationIsMine = false;
                MineAction();
                
                destinationIsOreShop= true;
                GetComponent<CharacterMovement>().destination = destinationOreShop.gameObject.GetComponent<NavigationWaypoint>();
            }
            else if (destinationIsOreShop)
            {
                destinationIsOreShop = false;

                Dictionary<Item, int> peek = sheet.inventory.SeeContents();
                foreach (Item key in peek.Keys)
                {
                    if (key.Type == ItemType.ORE)
                    {
                        destinationOreShop.Deposit(key);
                        sheet.inventory.Remove(key);
                    }
                }

                destinationIsMine = true;
                GetComponent<CharacterMovement>().destination = destinationMine.gameObject.GetComponent<NavigationWaypoint>();
            }
        }
    }

    public void FindMineAndSetDestination(MineOracle oracle)
    {
        Log("Start FindMineAndSetDestination");

        destinationMine = oracle.WhereShouldIMine(sheet.baseCity);
        destinationOreShop = oracle.WhereShouldIStore(sheet.baseCity);
        desiredOre = oracle.WhatShouldIMine();
        Log("Destination mine:" + destinationMine);

        GetComponent<CharacterMovement>().destination = destinationMine.gameObject.GetComponent<NavigationWaypoint>();
        Log("End FindMineAndSetDestination");
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

    public void MineAction()
    {
        ItemType result = destinationMine.WorkMine(desiredOre);

        Item workedItem = GameObject.FindGameObjectWithTag("GameManager").AddComponent<Item>();

        workedItem.Type = result;
        workedItem.PurchasedPrice = 0;

        sheet.inventory.Add(workedItem);
    }
}
