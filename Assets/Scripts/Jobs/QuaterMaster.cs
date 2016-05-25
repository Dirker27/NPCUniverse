using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


class QuaterMaster : NonPlayableCharacter
{
    private Inventory inventory;
    private QuaterMasterOracle quaterMasterOracle;
    private TradeOracle tradeOracle;

    public TradeCity baseCity;

    public GuildHall destinationHall;
    public Smithy destinationSmithy;

    private bool debug = false;

    public bool destinationIsBaseCity = false;
    public bool destinationIsHall = false;
    public bool destinationIsSmithy = false;

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
        this.quaterMasterOracle = GameObject.FindGameObjectWithTag("GameManager").GetComponent<QuaterMasterOracle>();

        destinationIsBaseCity = true;
    }

    void Update()
    {
        if (! GetComponent<CharacterMovement>().isInTransit())
        {
            if (destinationIsBaseCity)
            {
                destinationIsBaseCity = false;

                FindHallAndSetDestination(this.quaterMasterOracle);

                destinationIsSmithy = true;
                GetComponent<CharacterMovement>().destination = destinationSmithy.gameObject.GetComponent<NavigationWaypoint>();
            }
            else if (destinationIsHall)
            {
                destinationIsHall = false;

                HallAction();

                destinationIsSmithy = true;
                GetComponent<CharacterMovement>().destination = destinationSmithy.gameObject.GetComponent<NavigationWaypoint>();
            }
            else if (destinationIsSmithy)
            {
                destinationIsSmithy = false;
                Inventory magazine = destinationSmithy.PeekContents();
                Dictionary<Item, int> contents = magazine.SeeContents();

                Item weapon = GameObject.FindGameObjectWithTag("GameManager").AddComponent<Item>();
                bool foundWeapon = false;
                Item armor = GameObject.FindGameObjectWithTag("GameManager").AddComponent<Item>();
                bool foundArmor = false;
                foreach(Item item in contents.Keys)
                {
                    if (item.Type == ItemType.WEAPON)
                    {
                        weapon.Type = item.Type;
                        weapon.PurchasedPrice = item.PurchasedPrice;
                        foundWeapon = true;
                    }

                    if (item.Type == ItemType.ARMOR)
                    {
                        armor.Type = item.Type;
                        armor.PurchasedPrice = item.PurchasedPrice;
                        foundArmor = true;
                    }
                }
                if (foundWeapon)
                {
                    inventory.Add(weapon);
                    destinationSmithy.Withdraw(weapon);
                }

                if (foundArmor)
                {
                    inventory.Add(armor);
                    destinationSmithy.Withdraw(armor);
                }

                destinationIsHall = true;
                GetComponent<CharacterMovement>().destination = destinationHall.gameObject.GetComponent<NavigationWaypoint>();
            }
        }
    }

    public void FindHallAndSetDestination(QuaterMasterOracle oracle)
    {
        Log("Start FindSmithAndSetDestination");
        
        destinationHall = oracle.WhereShouldIStore(baseCity);
        destinationSmithy = oracle.WhereShouldIShop(baseCity);

        Log("Destination smith:" + destinationHall);
        
        Log("End FindSmithAndSetDestination");
    }

    public void HallAction()
    {
        Log("Start ArmorSmithAction at " + destinationHall);

        Dictionary<Item, int> myItems = inventory.SeeContents();

        foreach (Item item in myItems.Keys)
        {
            if (item.Type == ItemType.WEAPON)
            {
                inventory.Remove(item);

                destinationHall.Deposit(item);
            }

            if (item.Type == ItemType.ARMOR)
            {
                inventory.Remove(item);

                destinationHall.Deposit(item);
            }
        }


        GetComponent<CharacterMovement>().destination = baseCity.gameObject.GetComponent<NavigationWaypoint>();
        Log("End WeaponSmithAction");

    }
}
