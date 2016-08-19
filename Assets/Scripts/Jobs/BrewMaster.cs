using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


class BrewMaster : NonPlayableCharacter
{
    private BrewMasterOracle brewMasterOracle;

    public Brewhouse destinationBrewHouse;
    public Barn destinationBarn;

    public bool destinationIsBrewHouse = false;
    public bool destinationIsBarn = false;

    void Log(string s)
    {
        if (debug)
        {
            Debug.Log("BrewMaster log <" + s + ">");
        }
    }

    void Start()
    {
        base.Start();
        sheet.inventory = GetComponent<Inventory>();
        sheet.inventory.items = new Dictionary<Item, int>();
        sheet.tradeOracle = GameObject.FindGameObjectWithTag("GameManager").GetComponent<TradeOracle>();
        brewMasterOracle = GameObject.FindGameObjectWithTag("GameManager").GetComponent<BrewMasterOracle>();

        sheet.destinationIsBaseCity = true;
    }

    void Update()
    {
        if (!GetComponent<CharacterMovement>().isInTransit())
        {
            if (sheet.destinationIsBaseCity)
            {
                sheet.destinationIsBaseCity = false;

                FindBrewMasterAndSetDestination(this.brewMasterOracle);

                destinationIsBarn = true;
                GetComponent<CharacterMovement>().destination = destinationBarn.gameObject.GetComponent<NavigationWaypoint>();
            }
            else if (destinationIsBrewHouse)
            {
                destinationIsBrewHouse = false;

                BrewMasterAction();

                destinationIsBarn = true;
                GetComponent<CharacterMovement>().destination = destinationBarn.gameObject.GetComponent<NavigationWaypoint>();
            }
            else if (destinationIsBarn)
            {
                destinationIsBarn = false;
                Inventory magazine = destinationBarn.PeekContents();
                Dictionary<Item, int> contents = magazine.SeeContents();

                Item barley = GameObject.FindGameObjectWithTag("GameManager").AddComponent<Item>();
                bool foundBarley = false;
                foreach(Item item in contents.Keys)
                {
                    if (item.Type == ItemType.BARLEY)
                    {
                        barley.Type = item.Type;
                        barley.PurchasedPrice = item.PurchasedPrice;
                        foundBarley = true;
                    }
                }
                if (foundBarley)
                {
                    sheet.inventory.Add(barley);
                    destinationBarn.Withdraw(barley);
                }

                destinationIsBrewHouse = true;
                GetComponent<CharacterMovement>().destination = destinationBrewHouse.gameObject.GetComponent<NavigationWaypoint>();
            }
        }
    }

    public void FindBrewMasterAndSetDestination(BrewMasterOracle oracle)
    {
        Log("Start FindBrewMasterAndSetDestination");

        destinationBrewHouse = oracle.WhereShouldIBrew(sheet.baseCity);
        destinationBarn = oracle.WhereShouldIGather(sheet.baseCity);

        Log("Destination brewhouse:" + destinationBrewHouse);

        Log("End FindBrewMasterAndSetDestination");
    }

    public void BrewMasterAction()
    {
        Log("Start BrewMasterAction at " + destinationBrewHouse);
        foreach (Item item in sheet.inventory.items.Keys)
        {
            if (item.Type == ItemType.BARLEY)
            {
                
                Item barley = item;

                ItemType result = destinationBrewHouse.CraftBeer(barley);
                Log("Item received is :" + result);

                Log("Items before removal:" + Item.ListToString(sheet.inventory.items));
                sheet.inventory.Remove(barley);
                Log("Items after removal:" + Item.ListToString(sheet.inventory.items));


                Item workedItem = GameObject.FindGameObjectWithTag("GameManager").AddComponent<Item>();

                workedItem.Type = result;
                workedItem.PurchasedPrice = 0;

                Log("Items before add:" + Item.ListToString(sheet.inventory.items));
                sheet.inventory.Add(workedItem);
                Log("Items after add:" + Item.ListToString(sheet.inventory.items));

                destinationBrewHouse.Deposit(workedItem);
                GetComponent<CharacterMovement>().destination = sheet.baseCity.gameObject.GetComponent<NavigationWaypoint>();
                Log("End BrewMasterAction");

                return;
            }

        }        
    }
}
