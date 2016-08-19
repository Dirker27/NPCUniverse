using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


class Baker : NonPlayableCharacter
{
    private BakerOracle bakerOracle;

    public Bakery destinationBakery;
    NavigationWaypoint bakery;
    public Mill destinationMill;
    NavigationWaypoint mill;
    
    public override void Start()
    {
        base.Start();
        this.bakerOracle = GameObject.FindGameObjectWithTag("GameManager").GetComponent<BakerOracle>();

        sheet.destinationIsBaseCity = true;
    }

    void Update()
    {
        NPCStates state = sheet.npcOracle.WhatShouldIDo(sheet.hunger, sheet.energy);
        NPCUpdate(state);
        if (state == NPCStates.WORK)
        {
            sheet.previousState = NPCStates.WORK;

            if (!GetComponent<CharacterMovement>().isInTransit())
            {
                if (sheet.destinationIsBaseCity)
                {
                    sheet.destinationIsBaseCity = false;

                    FindBakeryAndSetDestination(this.bakerOracle);

                    SetDestinationForWork(destinationMill.gameObject.GetComponent<NavigationWaypoint>());
                }
                else if (GetComponent<CharacterMovement>().location == bakery)
                {
                    BakeAction();

                    SetDestinationForWork(destinationMill.gameObject.GetComponent<NavigationWaypoint>());
                }
                else if (GetComponent<CharacterMovement>().location == mill)
                {
                    Inventory magazine = destinationMill.PeekContents();
                    Dictionary<Item, int> contents = magazine.SeeContents();

                    Item flour = GameObject.FindGameObjectWithTag("GameManager").AddComponent<Item>();
                    bool foundFlour = false;
                    foreach (Item item in contents.Keys)
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
                        sheet.inventory.Add(flour);
                        destinationMill.Withdraw(flour);
                    }

                    SetDestinationForWork(destinationBakery.gameObject.GetComponent<NavigationWaypoint>());
                }
            }
        }
        
    }

    public void FindBakeryAndSetDestination(BakerOracle oracle)
    {
        logger.Log(debug, "Start FindBakeryAndSetDestination");

        destinationBakery = oracle.WhereShouldIBake(sheet.baseCity);
        bakery = destinationBakery.gameObject.GetComponent<NavigationWaypoint>();
        destinationMill = oracle.WhereShouldIMill(sheet.baseCity);
        mill = destinationMill.gameObject.GetComponent<NavigationWaypoint>();

        logger.Log(debug, "End FindBakeryAndSetDestination");
    }

    public void BakeAction()
    {
        logger.Log(debug, "Start BakeAction at " + destinationBakery);
        foreach (Item item in sheet.inventory.items.Keys)
        {
            logger.Log(debug, "Item is: " + item.Type);
            if (item.Type == ItemType.FLOUR)
            {
                
                Item wheat = item;

                ItemType result = destinationBakery.BakeBread(wheat);
                logger.Log(debug, "Item received is :" + result);

                logger.Log(debug, "Items before removal:" + Item.ListToString(sheet.inventory.items));
                sheet.inventory.Remove(wheat);
                logger.Log(debug, "Items after removal:" + Item.ListToString(sheet.inventory.items));


                Item workedItem = GameObject.FindGameObjectWithTag("GameManager").AddComponent<Item>();

                workedItem.Type = result;
                workedItem.PurchasedPrice = 0;

                logger.Log(debug, "Items before add:" + Item.ListToString(sheet.inventory.items));
                sheet.inventory.Add(workedItem);
                logger.Log(debug, "Items after add:" + Item.ListToString(sheet.inventory.items));

                destinationBakery.Deposit(workedItem);
                return;
            }
            logger.Log(debug, "End BakeryAction");
        }        
    }
}
