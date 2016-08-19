using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


class WoodCuter : NonPlayableCharacter
{
    private WoodCuterOracle woodCuterOracle;
    private TradeOracle tradeOracle;

    public WoodCut destinationWoodCut;
    public LogStore destinationLogStore;

    public bool destinationIsWoodCut = false;
    public bool destinationIsLogStore = false;

    void Start()
    {
        base.Start();
        sheet.inventory = GetComponent<Inventory>();
        sheet.inventory.items = new Dictionary<Item, int>();
        this.tradeOracle = GameObject.FindGameObjectWithTag("GameManager").GetComponent<TradeOracle>();
        this.woodCuterOracle = GameObject.FindGameObjectWithTag("GameManager").GetComponent<WoodCuterOracle>();
        this.logger = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Logger>();

        sheet.destinationIsBaseCity = true;
    }

    void Update()
    {
        if (! GetComponent<CharacterMovement>().isInTransit())
        {
            if (sheet.destinationIsBaseCity)
            {
                sheet.destinationIsBaseCity = false;

                FindWoodCutAndSetDestination(this.woodCuterOracle);

                destinationIsLogStore = true;
                GetComponent<CharacterMovement>().destination = destinationLogStore.gameObject.GetComponent<NavigationWaypoint>();
            }
            else if (destinationIsWoodCut)
            {
                destinationIsWoodCut = false;

                WoodCutAction();

                destinationIsLogStore = true;
                GetComponent<CharacterMovement>().destination = destinationLogStore.gameObject.GetComponent<NavigationWaypoint>();
            }
            else if (destinationIsLogStore)
            {
                destinationIsLogStore = false;
                Inventory magazine = destinationLogStore.PeekContents();
                Dictionary<Item, int> contents = magazine.SeeContents();

                Item log = GameObject.FindGameObjectWithTag("GameManager").AddComponent<Item>();
                bool foundLog = false;
                foreach(Item item in contents.Keys)
                {
                    if (item.Type == ItemType.LOG)
                    {
                        log.Type = item.Type;
                        log.PurchasedPrice = item.PurchasedPrice;
                        foundLog = true;
                    }
                }
                if (foundLog)
                {
                    sheet.inventory.Add(log);
                    destinationLogStore.Withdraw(log);
                    logger.Log(debug, "Added wheat to inventory" + sheet.inventory.items.Keys.Count);
                }

                destinationIsWoodCut = true;
                GetComponent<CharacterMovement>().destination = destinationWoodCut.gameObject.GetComponent<NavigationWaypoint>();
            }
        }
    }

    public void FindWoodCutAndSetDestination(WoodCuterOracle oracle)
    {
        logger.Log(debug, "Start FindWoodCutAndSetDestination");

        destinationWoodCut = oracle.WhereShouldICut(sheet.baseCity);
        destinationLogStore = oracle.WhereShouldIGather(sheet.baseCity);

        logger.Log(debug, "Destination mill:" + destinationWoodCut);

        logger.Log(debug, "End FindWoodCutAndSetDestination");
    }

    public void WoodCutAction()
    {
        logger.Log(debug, "Start WoodCutAction at " + destinationWoodCut);
        foreach (Item item in sheet.inventory.items.Keys)
        {
            if (item.Type == ItemType.LOG)
            {
                
                Item log = item;

                ItemType result = destinationWoodCut.MakeFireWood(log);
                logger.Log(debug, "Item received is :" + result);

                logger.Log(debug, "Items before removal:" + Item.ListToString(sheet.inventory.items));
                sheet.inventory.Remove(log);
                logger.Log(debug, "Items after removal:" + Item.ListToString(sheet.inventory.items));


                Item workedItem = GameObject.FindGameObjectWithTag("GameManager").AddComponent<Item>();

                workedItem.Type = result;
                workedItem.PurchasedPrice = 0;

                logger.Log(debug, "Items before add:" + Item.ListToString(sheet.inventory.items));
                sheet.inventory.Add(workedItem);
                logger.Log(debug, "Items after add:" + Item.ListToString(sheet.inventory.items));

                sheet.inventory.Remove(workedItem);
                destinationWoodCut.Deposit(workedItem);
                GetComponent<CharacterMovement>().destination = sheet.baseCity.gameObject.GetComponent<NavigationWaypoint>();
                
                return;
            }

        }
        logger.Log(debug, "End WoodCutAction");
    }
}
