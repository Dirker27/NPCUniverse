using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


class Hunter : NonPlayableCharacter
{
    private Inventory inventory;
    private HunterOracle hunterOracle;
    private TradeOracle tradeOracle;

    public TradeCity baseCity;

    public HuntingLodge destinationHuntingLodge;
    public BowShop destinationBowShop;

    private bool debug = false;

    public bool destinationIsBaseCity = false;
    public bool destinationIsHuntingLodge = false;
    public bool destinationIsBowShop = false;

    void Log(string s)
    {
        if (debug)
        {
            Debug.Log("Hunter log <" + s + ">");
        }
    }
    void Start()
    {
        this.inventory = GetComponent<Inventory>();
        this.inventory.items = new Dictionary<Item, int>();
        this.tradeOracle = GameObject.FindGameObjectWithTag("GameManager").GetComponent<TradeOracle>();
        this.hunterOracle = GameObject.FindGameObjectWithTag("GameManager").GetComponent<HunterOracle>();

        destinationIsBaseCity = true;
    }

    void Update()
    {
        if (! GetComponent<CharacterMovement>().isInTransit())
        {
            if (destinationIsBaseCity)
            {
                destinationIsBaseCity = false;

                FindHuntingLodgeAndSetDestination(this.hunterOracle);

                destinationIsBowShop = true;
                GetComponent<CharacterMovement>().destination = destinationBowShop.gameObject.GetComponent<NavigationWaypoint>();
            }
            else if (destinationIsHuntingLodge)
            {
                destinationIsHuntingLodge = false;

                HuntAction();

                destinationIsBowShop = true;
                GetComponent<CharacterMovement>().destination = destinationBowShop.gameObject.GetComponent<NavigationWaypoint>();
            }
            else if (destinationIsBowShop)
            {
                destinationIsBowShop = false;
                Inventory magazine = destinationBowShop.PeekContents();
                Dictionary<Item, int> contents = magazine.SeeContents();

                Item bar = GameObject.FindGameObjectWithTag("GameManager").AddComponent<Item>();
                bool foundBar = false;
                foreach(Item item in contents.Keys)
                {
                    if (item.Type == ItemType.BAR)
                    {
                        bar.Type = item.Type;
                        bar.PurchasedPrice = item.PurchasedPrice;
                        foundBar = true;
                    }
                }
                if (foundBar)
                {
                    inventory.Add(bar);
                    destinationBowShop.Withdraw(bar);
                }

                destinationIsHuntingLodge = true;
                GetComponent<CharacterMovement>().destination = destinationHuntingLodge.gameObject.GetComponent<NavigationWaypoint>();
            }
        }
    }

    public void FindHuntingLodgeAndSetDestination(HunterOracle oracle)
    {
        Log("Start FindHuntingLodgeAndSetDestination");
        
        destinationHuntingLodge = oracle.WhereShouldIHunt(baseCity);
        destinationBowShop = oracle.WhereShouldIShop(baseCity);

        Log("Destination lodge:" + destinationHuntingLodge);

        Log("End FindHuntingLodgeAndSetDestination");
    }

    public void HuntAction()
    {
        Log("Start HuntAction at " + destinationHuntingLodge);

        Item bow = null;
        Item arrow = null;

        foreach (Item item in inventory.items.Keys)
        {
            if (item.Type == ItemType.BOW)
            {
                bow = item;
            }
            else if (item.Type == ItemType.ARROW)
            {
                arrow = item;
            }
        }

        if (bow && arrow)
        {

            ItemType meat = destinationHuntingLodge.GatherMeat(bow, arrow);

            Log("Item received is :" + meat);

            Item workedItem = GameObject.FindGameObjectWithTag("GameManager").AddComponent<Item>();

            workedItem.Type = meat;
            workedItem.PurchasedPrice = 0;

            Log("Items before add:" + Item.ListToString(inventory.items));
            inventory.Add(workedItem);
            Log("Items after add:" + Item.ListToString(inventory.items));

            destinationHuntingLodge.Deposit(workedItem);

            ItemType leather = destinationHuntingLodge.GatherLeather(bow, arrow);

            Log("Item received is :" + leather);

            Log("Items before removal:" + Item.ListToString(inventory.items));
            inventory.Remove(bow);
            inventory.Remove(arrow);
            Log("Items after removal:" + Item.ListToString(inventory.items));


            Item workedItem2 = GameObject.FindGameObjectWithTag("GameManager").AddComponent<Item>();

            workedItem2.Type = leather;
            workedItem2.PurchasedPrice = 0;

            Log("Items before add:" + Item.ListToString(inventory.items));
            inventory.Add(workedItem2);
            Log("Items after add:" + Item.ListToString(inventory.items));

            destinationHuntingLodge.Deposit(workedItem2);

            GetComponent<CharacterMovement>().destination = baseCity.gameObject.GetComponent<NavigationWaypoint>();
            Log("End HuntAction");

            return;
        }
    }
}
