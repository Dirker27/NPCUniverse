using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


class Fletcher : NonPlayableCharacter
{
    private FletcherOracle fletcherOracle;
    private TradeOracle tradeOracle;

    public BowShop destinationBowShop;
    public LogStore destinationLogStore;

    public bool destinationIsBowShop = false;
    public bool destinationIsLogStore = false;

    void Start()
    {
        base.Start();
        sheet.inventory = GetComponent<Inventory>();
        sheet.inventory.items = new Dictionary<Item, int>();
        sheet.tradeOracle = GameObject.FindGameObjectWithTag("GameManager").GetComponent<TradeOracle>();
        this.fletcherOracle = GameObject.FindGameObjectWithTag("GameManager").GetComponent<FletcherOracle>();
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

                FindBowShopAndSetDestination(this.fletcherOracle);

                destinationIsLogStore = true;
                GetComponent<CharacterMovement>().destination = destinationLogStore.gameObject.GetComponent<NavigationWaypoint>();
            }
            else if (destinationIsBowShop)
            {
                destinationIsBowShop = false;

                CraftAction();

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

                destinationIsBowShop = true;
                GetComponent<CharacterMovement>().destination = destinationBowShop.gameObject.GetComponent<NavigationWaypoint>();
            }
        }
    }

    public void FindBowShopAndSetDestination(FletcherOracle oracle)
    {
        logger.Log(debug, "Start FindBowShopAndSetDestination");

        destinationBowShop = oracle.WhereShouldIWork(sheet.baseCity);
        destinationLogStore = oracle.WhereShouldIGather(sheet.baseCity);

        logger.Log(debug, "Destination bowshop:" + destinationBowShop);

        logger.Log(debug, "End FindBowShopAndSetDestination");
    }

    public void CraftAction()
    {
        logger.Log(debug, "Start CraftAction at " + destinationBowShop);
        foreach (Item item in sheet.inventory.items.Keys)
        {
            if (item.Type == ItemType.LOG)
            {
                
                Item log = item;

                ItemType result1 = destinationBowShop.CraftArrow(log);
                ItemType result2 = destinationBowShop.CraftBow(log);
                logger.Log(debug, "Item received is :" + result1);
                logger.Log(debug, "Item received is :" + result2);

                logger.Log(debug, "Items before removal:" + Item.ListToString(sheet.inventory.items));
                sheet.inventory.Remove(log);
                logger.Log(debug, "Items after removal:" + Item.ListToString(sheet.inventory.items));


                Item workedItem = GameObject.FindGameObjectWithTag("GameManager").AddComponent<Item>();

                workedItem.Type = result1;
                workedItem.PurchasedPrice = 0;

                logger.Log(debug, "Items before add:" + Item.ListToString(sheet.inventory.items));
                sheet.inventory.Add(workedItem);
                logger.Log(debug, "Items after add:" + Item.ListToString(sheet.inventory.items));

                Item workedItem2 = GameObject.FindGameObjectWithTag("GameManager").AddComponent<Item>();

                workedItem2.Type = result2;
                workedItem2.PurchasedPrice = 0;

                logger.Log(debug, "Items before add:" + Item.ListToString(sheet.inventory.items));
                sheet.inventory.Add(workedItem2);
                logger.Log(debug, "Items after add:" + Item.ListToString(sheet.inventory.items));

                sheet.inventory.Remove(workedItem);
                sheet.inventory.Remove(workedItem2);
                destinationBowShop.Deposit(workedItem);
                destinationBowShop.Deposit(workedItem2);
                GetComponent<CharacterMovement>().destination = sheet.baseCity.gameObject.GetComponent<NavigationWaypoint>();
                
                return;
            }

        }
        logger.Log(debug, "End CraftAction");
    }
}
