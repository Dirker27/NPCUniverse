using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


class ToolSmith : NonPlayableCharacter
{
    private ToolSmithOracle toolSmithOracle;
    private TradeOracle tradeOracle;

    public Smithy destinationSmithy;
    public Foundry destinationFoundry;

    public bool destinationIsSmithy = false;
    public bool destinationIsFoundry = false;

    void Log(string s)
    {
        if (debug)
        {
            Debug.Log("ToolSmith log <" + s + ">");
        }
    }
    void Start()
    {
        base.Start();
        sheet.inventory = GetComponent<Inventory>();
        sheet.inventory.items = new Dictionary<Item, int>();
        this.tradeOracle = GameObject.FindGameObjectWithTag("GameManager").GetComponent<TradeOracle>();
        this.toolSmithOracle = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ToolSmithOracle>();

        sheet.destinationIsBaseCity = true;
    }

    void Update()
    {
        if (! GetComponent<CharacterMovement>().isInTransit())
        {
            if (sheet.destinationIsBaseCity)
            {
                sheet.destinationIsBaseCity = false;

                FindToolSmithAndSetDestination(this.toolSmithOracle);

                destinationIsFoundry = true;
                GetComponent<CharacterMovement>().destination = destinationFoundry.gameObject.GetComponent<NavigationWaypoint>();
            }
            else if (destinationIsSmithy)
            {
                destinationIsSmithy = false;

                ToolSmithAction();

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
                    sheet.inventory.Add(bar);
                    destinationFoundry.Withdraw(bar);
                }

                destinationIsSmithy = true;
                GetComponent<CharacterMovement>().destination = destinationSmithy.gameObject.GetComponent<NavigationWaypoint>();
            }
        }
    }

    public void FindToolSmithAndSetDestination(ToolSmithOracle oracle)
    {
        Log("Start FindSmithAndSetDestination");

        destinationSmithy = oracle.WhereShouldISmith(sheet.baseCity);
        destinationFoundry = oracle.WhereShouldIShop(sheet.baseCity);

        Log("Destination smith:" + destinationSmithy);
        
        Log("End FindSmithAndSetDestination");
    }

    public void ToolSmithAction()
    {
        Log("Start ToolSmithAction at " + destinationSmithy);
        foreach (Item item in sheet.inventory.items.Keys)
        {
            if (item.Type == ItemType.BAR)
            {
                
                Item bar = item;

                ItemType result = destinationSmithy.WorkSmithyTool(bar);
                Log("Item received is :" + result);

                Log("Items before removal:" + Item.ListToString(sheet.inventory.items));
                sheet.inventory.Remove(bar);
                Log("Items after removal:" + Item.ListToString(sheet.inventory.items));


                Item workedItem = GameObject.FindGameObjectWithTag("GameManager").AddComponent<Item>();

                workedItem.Type = result;
                workedItem.PurchasedPrice = 0;

                Log("Items before add:" + Item.ListToString(sheet.inventory.items));
                sheet.inventory.Add(workedItem);
                Log("Items after add:" + Item.ListToString(sheet.inventory.items));

                destinationSmithy.Deposit(workedItem);
                GetComponent<CharacterMovement>().destination = sheet.baseCity.gameObject.GetComponent<NavigationWaypoint>();
                Log("End ToolSmithAction");

                return;
            }

        }        
    }
}
