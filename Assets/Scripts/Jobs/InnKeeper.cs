using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


class InnKeeper : NonPlayableCharacter
{
    private InnKeeperOracle innKeeperOracle;
    private TradeOracle tradeOracle;


    public Tavern destinationTavern;
    public Barn destinationBarn;
    public Bakery destinationBakery;

    public bool destinationIsTavern = false;
    public bool destinationIsBarn = false;
    public bool destinationIsBakery = false;

    void Start()
    {
        base.Start();
        sheet.inventory = GetComponent<Inventory>();
        sheet.inventory.items = new Dictionary<Item, int>();
        this.tradeOracle = GameObject.FindGameObjectWithTag("GameManager").GetComponent<TradeOracle>();
        this.innKeeperOracle = GameObject.FindGameObjectWithTag("GameManager").GetComponent<InnKeeperOracle>();
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

                FindTavernAndSetDestination(this.innKeeperOracle);

                destinationIsBarn = true;
                GetComponent<CharacterMovement>().destination = destinationBarn.gameObject.GetComponent<NavigationWaypoint>();
            }
            else if (destinationIsTavern)
            {
                destinationIsTavern = false;

                TavernAction();

                destinationIsBarn = true;
                GetComponent<CharacterMovement>().destination = destinationBarn.gameObject.GetComponent<NavigationWaypoint>();
            }
            else if (destinationIsBarn)
            {
                destinationIsBarn = false;
                Inventory magazine = destinationBarn.PeekContents();
                Dictionary<Item, int> contents = magazine.SeeContents();

                Item fish = GameObject.FindGameObjectWithTag("GameManager").AddComponent<Item>();
                bool foundFish = false;
                Item beer = GameObject.FindGameObjectWithTag("GameManager").AddComponent<Item>();
                bool foundBeer = false;
                foreach(Item item in contents.Keys)
                {
                    if (item.Type == ItemType.FISH)
                    {
                        fish.Type = item.Type;
                        fish.PurchasedPrice = item.PurchasedPrice;
                        foundFish = true;
                    }
                    else if (item.Type == ItemType.BEER)
                    {
                        beer.Type = item.Type;
                        beer.PurchasedPrice = item.PurchasedPrice;
                        foundBeer = true;
                    }
                }
                if (foundFish)
                {
                    sheet.inventory.Add(fish);
                    destinationBarn.Withdraw(fish);
                    logger.Log(debug, "Added fish to inventory" + sheet.inventory.items.Keys.Count);
                }
                else if (foundBeer)
                {
                    sheet.inventory.Add(beer);
                    destinationBarn.Withdraw(beer);
                    logger.Log(debug, "Added beer to inventory" + sheet.inventory.items.Keys.Count);
                }

                destinationIsBakery = true;
                GetComponent<CharacterMovement>().destination = destinationBakery.gameObject.GetComponent<NavigationWaypoint>();
            }
            else if (destinationIsBakery)
            {
                destinationIsBakery = false;
                Inventory magazine = destinationBarn.PeekContents();
                Dictionary<Item, int> contents = magazine.SeeContents();

                Item bread = GameObject.FindGameObjectWithTag("GameManager").AddComponent<Item>();
                bool foundBread = false;
                foreach (Item item in contents.Keys)
                {
                    if (item.Type == ItemType.FISH)
                    {
                        bread.Type = item.Type;
                        bread.PurchasedPrice = item.PurchasedPrice;
                        foundBread = true;
                    }
                }
                if (foundBread)
                {
                    sheet.inventory.Add(bread);
                    destinationBarn.Withdraw(bread);
                    logger.Log(debug, "Added bread to inventory" + sheet.inventory.items.Keys.Count);
                }

                destinationIsTavern = true;
                GetComponent<CharacterMovement>().destination = destinationTavern.gameObject.GetComponent<NavigationWaypoint>();
            }
        }
    }

    public void FindTavernAndSetDestination(InnKeeperOracle oracle)
    {
        logger.Log(debug, "Start FindTavernAndSetDestination");

        destinationTavern = oracle.WhereShouldIWork(sheet.baseCity);
        destinationBarn = oracle.WhereShouldIGetBeerAndFish(sheet.baseCity);
        destinationBakery = oracle.WhereShouldIGetBread(sheet.baseCity);

        logger.Log(debug, "Destination Tavern:" + destinationTavern);

        logger.Log(debug, "End FindTavernAndSetDestination");
    }

    public void TavernAction()
    {
        logger.Log(debug, "Start TavernAction at " + destinationTavern);
        Item fish = null;
        Item bread = null;
        Item beer = null;

        foreach (Item item in sheet.inventory.items.Keys)
        {
            if (item.Type == ItemType.FISH)
            {
                fish = item;
            }
            else if (item.Type == ItemType.BREAD)
            {
                bread = item;
            }
            else if (item.Type == ItemType.BEER)
            {
                beer = item;
            }
        }
        if (fish && bread && beer)
        {
            ItemType result = destinationTavern.MakeMeal(bread, fish, beer);
            sheet.inventory.Remove(fish);
            sheet.inventory.Remove(bread);
            sheet.inventory.Remove(beer);

            Item workedItem = GameObject.FindGameObjectWithTag("GameManager").AddComponent<Item>();

            workedItem.Type = result;
            workedItem.PurchasedPrice = 0;

            sheet.inventory.Add(workedItem);


            sheet.inventory.Remove(workedItem);
            destinationTavern.Deposit(workedItem);
        }

        logger.Log(debug, "End TavernAction");
    }
}
