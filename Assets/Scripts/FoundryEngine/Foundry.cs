using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Foundry : MonoBehaviour
{
    public ItemType produces;

    private Inventory inventory;

    private bool debug = false;

    void Log(string s)
    {
        if (debug)
        {
            Debug.Log("Foundry log <" + s + ">");
        }
    }

    void Start()
    {
        this.inventory = GetComponent<Inventory>();
        this.inventory.items = new Dictionary<TradeItem, int>();
    }

    public ItemType WorkFoundry(TradeItem input)
    {
        return produces;
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
}
