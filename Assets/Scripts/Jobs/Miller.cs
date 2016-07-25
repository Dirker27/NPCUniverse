using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


class Miller : NonPlayableCharacter
{
    private Inventory inventory;
    private MillOracle millOracle;
    private TradeOracle tradeOracle;

    public TradeCity baseCity;

    public Mill destinationMill;
    public Barn destinationBarn;

    public Logger logger;
    private bool debug = false;

    public bool destinationIsBaseCity = false;
    public bool destinationIsMill = false;
    public bool destinationIsBarn = false;

    void Start()
    {
        this.inventory = GetComponent<Inventory>();
        this.inventory.items = new Dictionary<Item, int>();
        this.tradeOracle = GameObject.FindGameObjectWithTag("GameManager").GetComponent<TradeOracle>();
        this.millOracle = GameObject.FindGameObjectWithTag("GameManager").GetComponent<MillOracle>();
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

                FindMillAndSetDestination(this.millOracle);

                destinationIsBarn = true;
                GetComponent<CharacterMovement>().destination = destinationBarn.gameObject.GetComponent<NavigationWaypoint>();
            }
            else if (destinationIsMill)
            {
                destinationIsMill = false;

                MillAction();

                destinationIsBarn = true;
                GetComponent<CharacterMovement>().destination = destinationBarn.gameObject.GetComponent<NavigationWaypoint>();
            }
            else if (destinationIsBarn)
            {
                destinationIsBarn = false;
                Inventory magazine = destinationBarn.PeekContents();
                Dictionary<Item, int> contents = magazine.SeeContents();

                Item wheat = GameObject.FindGameObjectWithTag("GameManager").AddComponent<Item>();
                bool foundWheat = false;
                foreach(Item item in contents.Keys)
                {
                    if (item.Type == ItemType.WHEAT)
                    {
                        wheat.Type = item.Type;
                        wheat.PurchasedPrice = item.PurchasedPrice;
                        foundWheat = true;
                    }
                }
                if (foundWheat)
                {
                    inventory.Add(wheat);
                    destinationBarn.Withdraw(wheat);
                    logger.Log(debug, "Added wheat to inventory" + inventory.items.Keys.Count);
                }

                destinationIsMill = true;
                GetComponent<CharacterMovement>().destination = destinationMill.gameObject.GetComponent<NavigationWaypoint>();
            }
        }
    }

    public void FindMillAndSetDestination(MillOracle oracle)
    {
        logger.Log(debug, "Start FindMillAndSetDestination");
        
        destinationMill = oracle.WhereShouldIMill(baseCity);
        destinationBarn = oracle.WhereShouldIShop(baseCity);

        logger.Log(debug, "Destination mill:" + destinationMill);

        logger.Log(debug, "End FindMillAndSetDestination");
    }

    public void MillAction()
    {
        logger.Log(debug, "Start MillAction at " + destinationMill);
        foreach (Item item in inventory.items.Keys)
        {
            if (item.Type == ItemType.WHEAT)
            {
                
                Item wheat = item;

                ItemType result = destinationMill.MakeFlour(wheat);
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

                inventory.Remove(workedItem);
                destinationMill.Deposit(workedItem);
                GetComponent<CharacterMovement>().destination = baseCity.gameObject.GetComponent<NavigationWaypoint>();
                
                return;
            }

        }
        logger.Log(debug, "End MillerAction");
    }
}
