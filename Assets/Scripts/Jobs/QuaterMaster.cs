using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


class QuaterMaster : NonPlayableCharacter
{
    private QuaterMasterOracle quaterMasterOracle;
    private TradeOracle tradeOracle;

    public GuildHall destinationHall;
    public Smithy destinationSmithy;

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
        base.Start();
        sheet.inventory = GetComponent<Inventory>();
        sheet.inventory.items = new Dictionary<Item, int>();
        this.tradeOracle = GameObject.FindGameObjectWithTag("GameManager").GetComponent<TradeOracle>();
        this.quaterMasterOracle = GameObject.FindGameObjectWithTag("GameManager").GetComponent<QuaterMasterOracle>();

        sheet.destinationIsBaseCity = true;
    }

    void Update()
    {
        if (! GetComponent<CharacterMovement>().isInTransit())
        {
            if (sheet.destinationIsBaseCity)
            {
                sheet.destinationIsBaseCity = false;

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
                    sheet.inventory.Add(weapon);
                    destinationSmithy.Withdraw(weapon);
                }

                if (foundArmor)
                {
                    sheet.inventory.Add(armor);
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

        destinationHall = oracle.WhereShouldIStore(sheet.baseCity);
        destinationSmithy = oracle.WhereShouldIShop(sheet.baseCity);

        Log("Destination smith:" + destinationHall);
        
        Log("End FindSmithAndSetDestination");
    }

    public void HallAction()
    {
        Log("Start ArmorSmithAction at " + destinationHall);

        Dictionary<Item, int> myItems = sheet.inventory.SeeContents();

        foreach (Item item in myItems.Keys)
        {
            if (item.Type == ItemType.WEAPON)
            {
                sheet.inventory.Remove(item);

                destinationHall.Deposit(item);
            }

            if (item.Type == ItemType.ARMOR)
            {
                sheet.inventory.Remove(item);

                destinationHall.Deposit(item);
            }
        }


        GetComponent<CharacterMovement>().destination = sheet.baseCity.gameObject.GetComponent<NavigationWaypoint>();
        Log("End WeaponSmithAction");

    }
}
