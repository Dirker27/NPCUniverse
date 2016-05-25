using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


class ArmorSmith : NonPlayableCharacter
{
    private Inventory inventory;
    private ArmorSmithOracle armorSmithOracle;
    private TradeOracle tradeOracle;

    public TradeCity baseCity;

    public Smithy destinationSmithy;
    public Foundry destinationFoundry;

    private bool debug = false;

    public bool destinationIsBaseCity = false;
    public bool destinationIsSmithy = false;
    public bool destinationIsFoundry = false;

    void Log(string s)
    {
        if (debug)
        {
            Debug.Log("WeaponSmith log <" + s + ">");
        }
    }
    void Start()
    {
        this.inventory = GetComponent<Inventory>();
        this.inventory.items = new Dictionary<Item, int>();
        this.tradeOracle = GameObject.FindGameObjectWithTag("GameManager").GetComponent<TradeOracle>();
        this.armorSmithOracle = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ArmorSmithOracle>();

        destinationIsBaseCity = true;
    }

    void Update()
    {
        if (! GetComponent<CharacterMovement>().isInTransit())
        {
            if (destinationIsBaseCity)
            {
                destinationIsBaseCity = false;

                FindArmorSmithAndSetDestination(this.armorSmithOracle);

                destinationIsFoundry = true;
                GetComponent<CharacterMovement>().destination = destinationFoundry.gameObject.GetComponent<NavigationWaypoint>();
            }
            else if (destinationIsSmithy)
            {
                destinationIsSmithy = false;

                ArmorSmithAction();

                destinationIsFoundry = true;
                GetComponent<CharacterMovement>().destination = destinationFoundry.gameObject.GetComponent<NavigationWaypoint>();
            }
            else if (destinationIsFoundry)
            {
                destinationIsFoundry = false;
                Inventory magazine = destinationFoundry.PeekContents();
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
                    destinationFoundry.Withdraw(bar);
                }

                destinationIsSmithy = true;
                GetComponent<CharacterMovement>().destination = destinationSmithy.gameObject.GetComponent<NavigationWaypoint>();
            }
        }
    }

    public void FindArmorSmithAndSetDestination(ArmorSmithOracle oracle)
    {
        Log("Start FindSmithAndSetDestination");
        
        destinationSmithy = oracle.WhereShouldISmith(baseCity);
        destinationFoundry = oracle.WhereShouldIShop(baseCity);

        Log("Destination smith:" + destinationSmithy);
        
        Log("End FindSmithAndSetDestination");
    }

    public void ArmorSmithAction()
    {
        Log("Start ArmorSmithAction at " + destinationSmithy);
        foreach (Item item in inventory.items.Keys)
        {
            if (item.Type == ItemType.BAR)
            {
                
                Item bar = item;

                ItemType result = destinationSmithy.WorkSmithyArmor(bar);
                Log("Item received is :" + result);

                Log("Items before removal:" + Item.ListToString(inventory.items));
                inventory.Remove(bar);
                Log("Items after removal:" + Item.ListToString(inventory.items));


                Item workedItem = GameObject.FindGameObjectWithTag("GameManager").AddComponent<Item>();

                workedItem.Type = result;
                workedItem.PurchasedPrice = 0;

                Log("Items before add:" + Item.ListToString(inventory.items));
                inventory.Add(workedItem);
                Log("Items after add:" + Item.ListToString(inventory.items));

                destinationSmithy.Deposit(workedItem);
                GetComponent<CharacterMovement>().destination = baseCity.gameObject.GetComponent<NavigationWaypoint>();
                Log("End WeaponSmithAction");

                return;
            }

        }        
    }
}
