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

    public Logger logger;
    private bool debug = false;

    public bool destinationIsBaseCity = false;
    public bool destinationIsBakery = false;
    public bool destinationIsMill = false;

    void Start()
    {
        this.logger = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Logger>();

        this.inventory = GetComponent<Inventory>();
        this.inventory.items = new Dictionary<Item, int>();
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
                Dictionary<Item, int> contents = magazine.SeeContents();

                Item flour = GameObject.FindGameObjectWithTag("GameManager").AddComponent<Item>();
                bool foundFlour = false;
                foreach(Item item in contents.Keys)
                {
                    if (item.Type == ItemType.FLOUR)
                    {
                        logger.Log(debug, "Found flour");
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
        logger.Log(debug, "Start FindBakeryAndSetDestination");
        
        destinationBakery = oracle.WhereShouldIBake(baseCity);
        destinationMill = oracle.WhereShouldIMill(baseCity);

        logger.Log(debug, "End FindBakeryAndSetDestination");
    }

    public void SellGoods(TradeOracle oracle)
    {
        logger.Log(debug, "Start SellGoods at " + baseCity);
        TradeOrders orders = oracle.WhatShouldISell(baseCity, inventory.items);

        logger.Log(debug, "Before trade currency:" + inventory.currency);
        inventory.currency += baseCity.MarketPlace.SellThese(orders.Manifests);
        logger.Log(debug, "After trade currency:" + inventory.currency);

        logger.Log(debug, "Items before sale:" + Item.ListToString(inventory.items));
        logger.Log(debug, "Items to sell:" + Item.ListToString(orders.Manifests));
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
        logger.Log(debug, "Items after sale:" + Item.ListToString(inventory.items));
        logger.Log(debug, "End SellGoods");
    }

    public void BakeAction()
    {
        logger.Log(debug, "Start BakeAction at " + destinationBakery);
        foreach (Item item in inventory.items.Keys)
        {
            logger.Log(debug, "Item is: " + item.Type);
            if (item.Type == ItemType.FLOUR)
            {
                
                Item wheat = item;

                ItemType result = destinationBakery.WorkBakery(wheat);
                logger.Log(debug, "Item received is :" + result);

                logger.Log(debug, "Items before removal:" + Item.ListToString(inventory.items));
                inventory.Remove(wheat);
                logger.Log(debug, "Items after removal:" + Item.ListToString(inventory.items));


                Item workedItem = GameObject.FindGameObjectWithTag("GameManager").AddComponent<Item>();

                workedItem.Type = result;
                workedItem.PurchasedPrice = 0;

                logger.Log(debug, "Items before add:" + Item.ListToString(inventory.items));
                inventory.Add(workedItem);
                logger.Log(debug, "Items after add:" + Item.ListToString(inventory.items));

                destinationBakery.Deposit(workedItem);

                GetComponent<CharacterMovement>().destination = destinationMill.gameObject.GetComponent<NavigationWaypoint>();
                

                return;
            }
            logger.Log(debug, "End BakeryAction");
        }        
    }
}
