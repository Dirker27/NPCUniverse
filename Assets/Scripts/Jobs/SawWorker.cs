using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


class SawWorker : NonPlayableCharacter
{
    private SawWorkerOracle sawWorkerOracle;

    public SawHouse destinationSawHouse;
    public LogStore destinationLogStore;

    public bool destinationIsSawHouse = false;
    public bool destinationIsLogStore = false;

    void Start()
    {
        base.Start();
        sheet.inventory = GetComponent<Inventory>();
        sheet.inventory.items = new Dictionary<Item, int>();
        this.sawWorkerOracle = GameObject.FindGameObjectWithTag("GameManager").GetComponent<SawWorkerOracle>();
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

                FindSawHouseAndSetDestination(this.sawWorkerOracle);

                destinationIsLogStore = true;
                GetComponent<CharacterMovement>().destination = destinationLogStore.gameObject.GetComponent<NavigationWaypoint>();
            }
            else if (destinationIsSawHouse)
            {
                destinationIsSawHouse = false;

                SawAction();

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

                destinationIsSawHouse = true;
                GetComponent<CharacterMovement>().destination = destinationSawHouse.gameObject.GetComponent<NavigationWaypoint>();
            }
        }
    }

    public void FindSawHouseAndSetDestination(SawWorkerOracle oracle)
    {
        logger.Log(debug, "Start FindSawHouseAndSetDestination");

        destinationSawHouse = oracle.WhereShouldISaw(sheet.baseCity);
        destinationLogStore = oracle.WhereShouldIGather(sheet.baseCity);

        logger.Log(debug, "Destination sawhouse:" + destinationSawHouse);

        logger.Log(debug, "End FindSawHouseAndSetDestination");
    }

    public void SawAction()
    {
        logger.Log(debug, "Start SawAction at " + destinationSawHouse);
        foreach (Item item in sheet.inventory.items.Keys)
        {
            if (item.Type == ItemType.LOG)
            {
                
                Item log = item;

                ItemType result = destinationSawHouse.MakePlanks(log);
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
                destinationSawHouse.Deposit(workedItem);
                GetComponent<CharacterMovement>().destination = sheet.baseCity.gameObject.GetComponent<NavigationWaypoint>();
                
                return;
            }

        }
        logger.Log(debug, "End SawAction");
    }
}
