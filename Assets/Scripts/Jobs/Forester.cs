using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


class Forester : NonPlayableCharacter
{
    private ForesterOracle foresterOracle;

    public Forest destinationForest;

    public LogStore destinationLogStore;

    public bool destinationIsForest = false;
    public bool destinationIsLogStore = false;

    void Log(string s)
    {
        if (debug)
        {
            Debug.Log("Farmer log <" + s + ">");
        }
    }
    void Start()
    {
        base.Start();
        sheet.inventory = GetComponent<Inventory>();
        sheet.inventory.items = new Dictionary<Item, int>();
        this.foresterOracle = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ForesterOracle>();
        sheet.destinationIsBaseCity = true;
    }

    void Update()
    {
        if (!GetComponent<CharacterMovement>().isInTransit())
        {
            if (sheet.destinationIsBaseCity)
            {
                sheet.destinationIsBaseCity = false;
                FindForestAndSetDestination(this.foresterOracle);
                destinationIsForest = true;
            }
            else if (destinationIsForest)
            {
                destinationIsForest = false;

                ForestAction();

                GetComponent<CharacterMovement>().destination = destinationLogStore.gameObject.GetComponent<NavigationWaypoint>();
                destinationIsLogStore = true;
                return;
            }
            else if (destinationIsLogStore)
            {
                destinationIsLogStore = false;
                Dictionary<Item, int> peek = sheet.inventory.SeeContents();
                foreach(Item key in peek.Keys)
                {
                    if (key.Type == ItemType.LOG)
                    {
                        destinationLogStore.Deposit(key);
                        sheet.inventory.Remove(key);
                    }
                }
                destinationIsForest = true;
                GetComponent<CharacterMovement>().destination = destinationForest.gameObject.GetComponent<NavigationWaypoint>();
            }
        }
    }

    public void FindForestAndSetDestination(ForesterOracle oracle)
    {
        Log("Start FindForestAndSetDestination");

        destinationForest = oracle.WhereShouldICut(sheet.baseCity);
        destinationLogStore = oracle.WhereShouldIStore(sheet.baseCity);

        Log("Destination farm:" + destinationForest);
        Log("Destination barn:" + destinationLogStore);

        GetComponent<CharacterMovement>().destination = destinationForest.gameObject.GetComponent<NavigationWaypoint>();
        Log("End FindForestAndSetDestination");
    }

    public void ForestAction()
    {
        ItemType result = destinationForest.WorkForest();

        Item workedItem = GameObject.FindGameObjectWithTag("GameManager").AddComponent<Item>();

        workedItem.Type = result;
        workedItem.PurchasedPrice = 0;

        sheet.inventory.Add(workedItem);
    }
}
