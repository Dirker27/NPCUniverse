using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bakery : MonoBehaviour
{
    public ItemType produces;

    private Inventory inventory;

    private bool debug = false;

    void Log(string s)
    {
        if (debug)
        {
            Debug.Log("Bakery log <" + s + ">");
        }
    }

    void Start()
    {
        this.inventory = GetComponent<Inventory>();
        this.inventory.items = new Dictionary<TradeItem, int>();
    }

    public void Deposit(TradeItem deposit)
    {
        Log("Deposit before: " + inventory.ToString());
        inventory.Add(deposit);
        Log("Deposit after: " + inventory.ToString());
    }

    public void Withdraw(TradeItem toWithDraw)
    {
        Log("To withdraw" + toWithDraw.ToString());
        Log("Withdraw before: " + inventory.items.Count);
        inventory.Remove(toWithDraw);
        Log("Withdraw after: " + inventory.items.Count);
    }

    public Inventory PeekContents()
    {
        Inventory toReturn = GameObject.FindGameObjectWithTag("GameManager").AddComponent<Inventory>();
        toReturn.InventorySet(inventory);
        return toReturn;
    }

    public ItemType WorkBakery(TradeItem input)
    {
        return produces;
    }
}

