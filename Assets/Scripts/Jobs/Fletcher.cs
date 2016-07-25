using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


class Fletcher : NonPlayableCharacter
{
    private Inventory inventory;
    private FletcherOracle fletcherOracle;
    private TradeOracle tradeOracle;

    public TradeCity baseCity;

    public BowShop destinationBowShop;
    public LogStore destinationLogStore;

    public Logger logger;
    private bool debug = false;

    public bool destinationIsBaseCity = false;
    public bool destinationIsBowShop = false;
    public bool destinationIsLogStore = false;

    void Start()
    {
        this.inventory = GetComponent<Inventory>();
        this.inventory.items = new Dictionary<Item, int>();
        this.tradeOracle = GameObject.FindGameObjectWithTag("GameManager").GetComponent<TradeOracle>();
        this.fletcherOracle = GameObject.FindGameObjectWithTag("GameManager").GetComponent<FletcherOracle>();
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
                    inventory.Add(log);
                    destinationLogStore.Withdraw(log);
                    logger.Log(debug, "Added wheat to inventory" + inventory.items.Keys.Count);
                }

                destinationIsBowShop = true;
                GetComponent<CharacterMovement>().destination = destinationBowShop.gameObject.GetComponent<NavigationWaypoint>();
            }
        }
    }

    public void FindBowShopAndSetDestination(FletcherOracle oracle)
    {
        logger.Log(debug, "Start FindBowShopAndSetDestination");
        
        destinationBowShop = oracle.WhereShouldIWork(baseCity);
        destinationLogStore = oracle.WhereShouldIGather(baseCity);

        logger.Log(debug, "Destination bowshop:" + destinationBowShop);

        logger.Log(debug, "End FindBowShopAndSetDestination");
    }

    public void CraftAction()
    {
        logger.Log(debug, "Start CraftAction at " + destinationBowShop);
        foreach (Item item in inventory.items.Keys)
        {
            if (item.Type == ItemType.LOG)
            {
                
                Item log = item;

                ItemType result1 = destinationBowShop.CraftArrow(log);
                ItemType result2 = destinationBowShop.CraftBow(log);
                logger.Log(debug, "Item received is :" + result1);
                logger.Log(debug, "Item received is :" + result2);

                logger.Log(debug, "Items before removal:" + Item.ListToString(inventory.items));
                inventory.Remove(log);
                logger.Log(debug, "Items after removal:" + Item.ListToString(inventory.items));


                Item workedItem = GameObject.FindGameObjectWithTag("GameManager").AddComponent<Item>();

                workedItem.Type = result1;
                workedItem.PurchasedPrice = 0;

                logger.Log(debug, "Items before add:" + Item.ListToString(inventory.items));
                inventory.Add(workedItem);
                logger.Log(debug, "Items after add:" + Item.ListToString(inventory.items));

                Item workedItem2 = GameObject.FindGameObjectWithTag("GameManager").AddComponent<Item>();

                workedItem2.Type = result2;
                workedItem2.PurchasedPrice = 0;

                logger.Log(debug, "Items before add:" + Item.ListToString(inventory.items));
                inventory.Add(workedItem2);
                logger.Log(debug, "Items after add:" + Item.ListToString(inventory.items));

                inventory.Remove(workedItem);
                inventory.Remove(workedItem2);
                destinationBowShop.Deposit(workedItem);
                destinationBowShop.Deposit(workedItem2);
                GetComponent<CharacterMovement>().destination = baseCity.gameObject.GetComponent<NavigationWaypoint>();
                
                return;
            }

        }
        logger.Log(debug, "End CraftAction");
    }
}
