using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Barn : MonoBehaviour
{
    private Inventory inventory;

    void Start()
    {
        this.inventory = GetComponent<Inventory>();
        this.inventory.items = new Dictionary<TradeItem, int>();
    }

    public void Deposit(TradeItem deposit)
    {
        inventory.Add(deposit);
    }

    public void Withdraw(TradeItem toWithDraw)
    {
        inventory.Remove(toWithDraw);
    }
}

