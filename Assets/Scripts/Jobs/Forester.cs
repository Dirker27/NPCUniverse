using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


class Forester : NonPlayableCharacter
{
    private Inventory inventory;
    private ForesterOracle foresterOracle;
    private TradeOracle tradeOracle;

    public TradeCity baseCity;

    public Forest destinationForest;

    public LogStore destinationLogStore;

    private bool debug = false;

    public bool destinationIsForest = false;
    public bool destinationIsLogStore = false;
    public bool destinationIsCity = false;

    void Log(string s)
    {
        if (debug)
        {
            Debug.Log("Farmer log <" + s + ">");
        }
    }
    void Start()
    {
        this.inventory = GetComponent<Inventory>();
        this.inventory.items = new Dictionary<TradeItem, int>();
        this.tradeOracle = GameObject.FindGameObjectWithTag("GameManager").GetComponent<TradeOracle>();
        this.foresterOracle = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ForesterOracle>();
        destinationIsCity = true;
    }

    void Update()
    {
        if (!GetComponent<CharacterMovement>().isInTransit())
        {
            if (destinationIsCity)
            {
                destinationIsCity = false;
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
                Dictionary<TradeItem, int> peek = inventory.SeeContents();
                foreach(TradeItem key in peek.Keys)
                {
                    if (key.Type == ItemType.LOG)
                    {
                        destinationLogStore.Deposit(key);
                        inventory.Remove(key);
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
        
        destinationForest = oracle.WhereShouldICut(baseCity);
        destinationLogStore = oracle.WhereShouldIStore(baseCity);

        Log("Destination farm:" + destinationForest);
        Log("Destination barn:" + destinationLogStore);

        GetComponent<CharacterMovement>().destination = destinationForest.gameObject.GetComponent<NavigationWaypoint>();
        Log("End FindForestAndSetDestination");
    }

    public void ForestAction()
    {
        ItemType result = destinationForest.WorkForest();

        TradeItem workedItem = GameObject.FindGameObjectWithTag("GameManager").AddComponent<TradeItem>();

        workedItem.Type = result;
        workedItem.PurchasedPrice = 0;

        inventory.Add(workedItem);
    }
}
