using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


class ArmorSmith : NonPlayableCharacter
{
    private ArmorSmithOracle armorSmithOracle;

    public Smithy destinationSmithy;
    NavigationWaypoint smithy;
    public Foundry destinationFoundry;
    NavigationWaypoint foundry;

    

    public override void Start()
    {
        base.Start();
        this.armorSmithOracle = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ArmorSmithOracle>();
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

                    FindArmorSmithAndSetDestination(this.armorSmithOracle);

                    SetDestinationForWork(destinationFoundry.gameObject.GetComponent<NavigationWaypoint>());
                }
                else if (GetComponent<CharacterMovement>().location == smithy)
                {
                    ArmorSmithAction();

                    SetDestinationForWork(destinationFoundry.gameObject.GetComponent<NavigationWaypoint>());
                }
                else if (GetComponent<CharacterMovement>().location == foundry)
                {
                    Inventory magazine = destinationFoundry.PeekContents();
                    Dictionary<Item, int> contents = magazine.SeeContents();

                    Item bar = GameObject.FindGameObjectWithTag("GameManager").AddComponent<Item>();
                    bool foundBar = false;
                    foreach (Item item in contents.Keys)
                    {
                        if (item.Type == ItemType.BAR)
                        {
                            bar.Type = item.Type;
                            bar.PurchasedPrice = item.PurchasedPrice;
                            foundBar = true;
                        }
                    }
                    if (foundBar)
                    {
                        sheet.inventory.Add(bar);
                        destinationFoundry.Withdraw(bar);
                    }

                    SetDestinationForWork(destinationSmithy.gameObject.GetComponent<NavigationWaypoint>());
                }
            }
        }
    }

    public void FindArmorSmithAndSetDestination(ArmorSmithOracle oracle)
    {
        logger.Log(debug, "Start FindSmithAndSetDestination");

        destinationSmithy = oracle.WhereShouldISmith(sheet.baseCity);
        smithy = destinationSmithy.gameObject.GetComponent<NavigationWaypoint>();
        destinationFoundry = oracle.WhereShouldIShop(sheet.baseCity);
        foundry = destinationFoundry.gameObject.GetComponent<NavigationWaypoint>();

        logger.Log(debug, "Destination smith:" + destinationSmithy);

        logger.Log(debug, "End FindSmithAndSetDestination");
    }

    public void ArmorSmithAction()
    {
        logger.Log(debug, "Start ArmorSmithAction at " + destinationSmithy);
        foreach (Item item in sheet.inventory.items.Keys)
        {
            if (item.Type == ItemType.BAR)
            {
                
                Item bar = item;

                ItemType result = destinationSmithy.WorkSmithyArmor(bar);
                logger.Log(debug, "Item received is :" + result);

                logger.Log(debug, "Items before removal:" + Item.ListToString(sheet.inventory.items));
                sheet.inventory.Remove(bar);
                logger.Log(debug, "Items after removal:" + Item.ListToString(sheet.inventory.items));


                Item workedItem = GameObject.FindGameObjectWithTag("GameManager").AddComponent<Item>();

                workedItem.Type = result;
                workedItem.PurchasedPrice = 0;

                logger.Log(debug, "Items before add:" + Item.ListToString(sheet.inventory.items));
                sheet.inventory.Add(workedItem);
                logger.Log(debug, "Items after add:" + Item.ListToString(sheet.inventory.items));

                destinationSmithy.Deposit(workedItem);
                logger.Log(debug, "End WeaponSmithAction");

                return;
            }

        }        
    }
}
