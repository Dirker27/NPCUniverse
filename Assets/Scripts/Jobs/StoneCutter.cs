﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


class StoneCutter : NonPlayableCharacter
{
    private Inventory inventory;
    private StoneCutterOracle stoneCutterOracle;
    private TradeOracle tradeOracle;

    public TradeCity baseCity;

    public Masonry destinationMasonry;
    public OreShop destinationOreShop;

    public Logger logger;
    private bool debug = false;

    public bool destinationIsBaseCity = false;
    public bool destinationIsMasonry = false;
    public bool destinationIsOreShop = false;

    void Start()
    {
        this.inventory = GetComponent<Inventory>();
        this.inventory.items = new Dictionary<Item, int>();
        this.tradeOracle = GameObject.FindGameObjectWithTag("GameManager").GetComponent<TradeOracle>();
        this.stoneCutterOracle = GameObject.FindGameObjectWithTag("GameManager").GetComponent<StoneCutterOracle>();
        this.logger = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Logger>();

        destinationIsBaseCity = true;
    }

    void Update()
    {
        if (! GetComponent<CharacterMovement>().isInTransit())
        {
            if (destinationIsBaseCity)
            {
                destinationIsBaseCity = false;

                FindMasonryAndSetDestination(this.stoneCutterOracle);

                destinationIsOreShop = true;
                GetComponent<CharacterMovement>().destination = destinationOreShop.gameObject.GetComponent<NavigationWaypoint>();
            }
            else if (destinationIsMasonry)
            {
                destinationIsMasonry = false;

                MasonryAction();

                destinationIsOreShop = true;
                GetComponent<CharacterMovement>().destination = destinationOreShop.gameObject.GetComponent<NavigationWaypoint>();
            }
            else if (destinationIsOreShop)
            {
                destinationIsOreShop = false;
                Inventory magazine = destinationOreShop.PeekContents();
                Dictionary<Item, int> contents = magazine.SeeContents();

                Item stone = GameObject.FindGameObjectWithTag("GameManager").AddComponent<Item>();
                bool foundStone = false;
                foreach(Item item in contents.Keys)
                {
                    if (item.Type == ItemType.STONE)
                    {
                        stone.Type = item.Type;
                        stone.PurchasedPrice = item.PurchasedPrice;
                        foundStone = true;
                    }
                }
                if (foundStone)
                {
                    inventory.Add(stone);
                    destinationOreShop.Withdraw(stone);
                    logger.Log(debug, "Added wheat to inventory" + inventory.items.Keys.Count);
                }

                destinationIsMasonry = true;
                GetComponent<CharacterMovement>().destination = destinationMasonry.gameObject.GetComponent<NavigationWaypoint>();
            }
        }
    }

    public void FindMasonryAndSetDestination(StoneCutterOracle oracle)
    {
        logger.Log(debug, "Start FindMasonryAndSetDestination");
        
        destinationMasonry = oracle.WhereShouldICut(baseCity);
        destinationOreShop = oracle.WhereShouldIShop(baseCity);

        logger.Log(debug, "Destination mill:" + destinationMasonry);

        logger.Log(debug, "End FindMasonryAndSetDestination");
    }

    public void MasonryAction()
    {
        logger.Log(debug, "Start MasonryAction at " + destinationMasonry);
        foreach (Item item in inventory.items.Keys)
        {
            if (item.Type == ItemType.STONE)
            {
                
                Item stone = item;

                ItemType result = destinationMasonry.CutStone(stone);
                logger.Log(debug, "Item received is :" + result);

                logger.Log(debug, "Items before removal:" + Item.ListToString(inventory.items));
                inventory.Remove(stone);
                logger.Log(debug, "Items after removal:" + Item.ListToString(inventory.items));


                Item workedItem = GameObject.FindGameObjectWithTag("GameManager").AddComponent<Item>();

                workedItem.Type = result;
                workedItem.PurchasedPrice = 0;

                logger.Log(debug, "Items before add:" + Item.ListToString(inventory.items));
                inventory.Add(workedItem);
                logger.Log(debug, "Items after add:" + Item.ListToString(inventory.items));

                inventory.Remove(workedItem);
                destinationMasonry.Deposit(workedItem);
                GetComponent<CharacterMovement>().destination = baseCity.gameObject.GetComponent<NavigationWaypoint>();
                
                return;
            }

        }
        logger.Log(debug, "End MasonryAction");
    }
}