using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


class Collier : NonPlayableCharacter
{
    private Inventory inventory;
    private CollierOracle collierOracle;
    private TradeOracle tradeOracle;

    public TradeCity baseCity;

    public CharcoalPit destinationCharcoalPit;
    public WoodCut destinationWoodCut;

    public Logger logger;
    private bool debug = false;

    public bool destinationIsBaseCity = false;
    public bool destinationIsCharcoalPit = false;
    public bool destinationIsWoodCut = false;

    void Start()
    {
        this.inventory = GetComponent<Inventory>();
        this.inventory.items = new Dictionary<Item, int>();
        this.tradeOracle = GameObject.FindGameObjectWithTag("GameManager").GetComponent<TradeOracle>();
        this.collierOracle = GameObject.FindGameObjectWithTag("GameManager").GetComponent<CollierOracle>();
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

                FindCharcoalPitAndSetDestination(this.collierOracle);

                destinationIsWoodCut = true;
                GetComponent<CharacterMovement>().destination = destinationWoodCut.gameObject.GetComponent<NavigationWaypoint>();
            }
            else if (destinationIsCharcoalPit)
            {
                destinationIsCharcoalPit = false;

                CollierAction();

                destinationIsWoodCut = true;
                GetComponent<CharacterMovement>().destination = destinationWoodCut.gameObject.GetComponent<NavigationWaypoint>();
            }
            else if (destinationIsWoodCut)
            {
                destinationIsWoodCut = false;
                Inventory magazine = destinationWoodCut.PeekContents();
                Dictionary<Item, int> contents = magazine.SeeContents();

                Item fireWood = GameObject.FindGameObjectWithTag("GameManager").AddComponent<Item>();
                bool foundFireWood = false;
                foreach(Item item in contents.Keys)
                {
                    if (item.Type == ItemType.FIREWOOD)
                    {
                        fireWood.Type = item.Type;
                        fireWood.PurchasedPrice = item.PurchasedPrice;
                        foundFireWood = true;
                    }
                }
                if (foundFireWood)
                {
                    inventory.Add(fireWood);
                    destinationWoodCut.Withdraw(fireWood);
                    logger.Log(debug, "Added wheat to inventory" + inventory.items.Keys.Count);
                }

                destinationIsCharcoalPit = true;
                GetComponent<CharacterMovement>().destination = destinationCharcoalPit.gameObject.GetComponent<NavigationWaypoint>();
            }
        }
    }

    public void FindCharcoalPitAndSetDestination(CollierOracle oracle)
    {
        logger.Log(debug, "Start FindWoodCutAndSetDestination");
        
        destinationCharcoalPit = oracle.WhereShouldICook(baseCity);
        destinationWoodCut = oracle.WhereShouldIGather(baseCity);

        logger.Log(debug, "Destination mill:" + destinationCharcoalPit);

        logger.Log(debug, "End FindWoodCutAndSetDestination");
    }

    public void CollierAction()
    {
        logger.Log(debug, "Start WoodCutAction at " + destinationCharcoalPit);
        foreach (Item item in inventory.items.Keys)
        {
            if (item.Type == ItemType.FIREWOOD)
            {
                
                Item log = item;

                ItemType result = destinationCharcoalPit.WorkCharcoalHouse(log);
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
                destinationCharcoalPit.Deposit(workedItem);
                GetComponent<CharacterMovement>().destination = baseCity.gameObject.GetComponent<NavigationWaypoint>();
                
                return;
            }

        }
        logger.Log(debug, "End WoodCutAction");
    }
}
