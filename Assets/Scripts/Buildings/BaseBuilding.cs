using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BaseBuilding : MonoBehaviour
{
    public Inventory inventory;

    public Logger logger;
    public bool debug = false;


    public void Start()
    {
        this.logger = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Logger>();
        this.inventory = GetComponent<Inventory>();
        this.inventory.items = new Dictionary<Item, int>();
    }

    public void Deposit(Item deposit)
    {
        logger.Log(debug, "Deposit before: " + inventory.ToString());
        inventory.Add(deposit);
        logger.Log(debug, "Deposit after: " + inventory.ToString());
    }

    public void Withdraw(Item toWithDraw)
    {
        logger.Log(debug, "To withdraw" + toWithDraw.ToString());
        logger.Log(debug, "Withdraw before: " + inventory.ToString());
        inventory.Remove(toWithDraw);
        logger.Log(debug, "Withdraw after: " + inventory.ToString());
    }

    public Inventory PeekContents()
    {
        Inventory toReturn = GameObject.FindGameObjectWithTag("GameManager").AddComponent<Inventory>();
        toReturn.InventorySet(inventory);
        return toReturn;
    }
}

