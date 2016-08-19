using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


class StoneCutter : NonPlayableCharacter
{
    private StoneCutterOracle stoneCutterOracle;
    private TradeOracle tradeOracle;

    public Masonry destinationMasonry;
    public OreShop destinationOreShop;

    public bool destinationIsMasonry = false;
    public bool destinationIsOreShop = false;

    void Start()
    {
        base.Start();
        sheet.inventory = GetComponent<Inventory>();
        sheet.inventory.items = new Dictionary<Item, int>();
        this.tradeOracle = GameObject.FindGameObjectWithTag("GameManager").GetComponent<TradeOracle>();
        this.stoneCutterOracle = GameObject.FindGameObjectWithTag("GameManager").GetComponent<StoneCutterOracle>();
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
                    sheet.inventory.Add(stone);
                    destinationOreShop.Withdraw(stone);
                    logger.Log(debug, "Added wheat to inventory" + sheet.inventory.items.Keys.Count);
                }

                destinationIsMasonry = true;
                GetComponent<CharacterMovement>().destination = destinationMasonry.gameObject.GetComponent<NavigationWaypoint>();
            }
        }
    }

    public void FindMasonryAndSetDestination(StoneCutterOracle oracle)
    {
        logger.Log(debug, "Start FindMasonryAndSetDestination");

        destinationMasonry = oracle.WhereShouldICut(sheet.baseCity);
        destinationOreShop = oracle.WhereShouldIShop(sheet.baseCity);

        logger.Log(debug, "Destination mill:" + destinationMasonry);

        logger.Log(debug, "End FindMasonryAndSetDestination");
    }

    public void MasonryAction()
    {
        logger.Log(debug, "Start MasonryAction at " + destinationMasonry);
        foreach (Item item in sheet.inventory.items.Keys)
        {
            if (item.Type == ItemType.STONE)
            {
                
                Item stone = item;

                ItemType result = destinationMasonry.CutStone(stone);
                logger.Log(debug, "Item received is :" + result);

                logger.Log(debug, "Items before removal:" + Item.ListToString(sheet.inventory.items));
                sheet.inventory.Remove(stone);
                logger.Log(debug, "Items after removal:" + Item.ListToString(sheet.inventory.items));


                Item workedItem = GameObject.FindGameObjectWithTag("GameManager").AddComponent<Item>();

                workedItem.Type = result;
                workedItem.PurchasedPrice = 0;

                logger.Log(debug, "Items before add:" + Item.ListToString(sheet.inventory.items));
                sheet.inventory.Add(workedItem);
                logger.Log(debug, "Items after add:" + Item.ListToString(sheet.inventory.items));

                sheet.inventory.Remove(workedItem);
                destinationMasonry.Deposit(workedItem);
                GetComponent<CharacterMovement>().destination = sheet.baseCity.gameObject.GetComponent<NavigationWaypoint>();
                
                return;
            }

        }
        logger.Log(debug, "End MasonryAction");
    }
}
