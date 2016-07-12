using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


class SawWorker : NonPlayableCharacter
{
    private Inventory inventory;
    private SawWorkerOracle sawWorkerOracle;
    private TradeOracle tradeOracle;

    public TradeCity baseCity;

    public SawHouse destinationSawHouse;
    public LogStore destinationLogStore;

    public Logger logger;
    private bool debug = false;

    public bool destinationIsBaseCity = false;
    public bool destinationIsSawHouse = false;
    public bool destinationIsLogStore = false;

    void Start()
    {
        this.inventory = GetComponent<Inventory>();
        this.inventory.items = new Dictionary<Item, int>();
        this.tradeOracle = GameObject.FindGameObjectWithTag("GameManager").GetComponent<TradeOracle>();
        this.sawWorkerOracle = GameObject.FindGameObjectWithTag("GameManager").GetComponent<SawWorkerOracle>();
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
                    inventory.Add(log);
                    destinationLogStore.Withdraw(log);
                    logger.Log(debug, "Added wheat to inventory" + inventory.items.Keys.Count);
                }

                destinationIsSawHouse = true;
                GetComponent<CharacterMovement>().destination = destinationSawHouse.gameObject.GetComponent<NavigationWaypoint>();
            }
        }
    }

    public void FindSawHouseAndSetDestination(SawWorkerOracle oracle)
    {
        logger.Log(debug, "Start FindSawHouseAndSetDestination");
        
        destinationSawHouse = oracle.WhereShouldISaw(baseCity);
        destinationLogStore = oracle.WhereShouldIGather(baseCity);

        logger.Log(debug, "Destination sawhouse:" + destinationSawHouse);

        logger.Log(debug, "End FindSawHouseAndSetDestination");
    }

    public void SawAction()
    {
        logger.Log(debug, "Start SawAction at " + destinationSawHouse);
        foreach (Item item in inventory.items.Keys)
        {
            if (item.Type == ItemType.LOG)
            {
                
                Item log = item;

                ItemType result = destinationSawHouse.MakePlanks(log);
                logger.Log(debug, "Item received is :" + result);

                logger.Log(debug, "Items before removal:" + Item.ListToString(inventory.items));
                inventory.Remove(log);
                logger.Log(debug, "Items after removal:" + Item.ListToString(inventory.items));


                Item workedItem = GameObject.FindGameObjectWithTag("GameManager").AddComponent<Item>();

                workedItem.Type = result;
                workedItem.PurchasedPrice = 0;

                logger.Log(debug, "Items before add:" + Item.ListToString(inventory.items));
                inventory.Add(workedItem);
                logger.Log(debug, "Items after add:" + Item.ListToString(inventory.items));

                inventory.Remove(workedItem);
                destinationSawHouse.Deposit(workedItem);
                GetComponent<CharacterMovement>().destination = baseCity.gameObject.GetComponent<NavigationWaypoint>();
                
                return;
            }

        }
        logger.Log(debug, "End SawAction");
    }
}
