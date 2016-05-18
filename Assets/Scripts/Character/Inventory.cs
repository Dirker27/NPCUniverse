using UnityEngine;
using System.Collections;
using System.Collections.Generic;  // dafuq? - No. Fuck You, Nathaniel.

public class Inventory : MonoBehaviour
{
    public int currency;
    public Dictionary<TradeItem, int> items;

    private bool debug = true;

    void Log(string s)
    {
        if (debug)
        {
            Debug.Log("Inventory log <" + s + ">");
        }
    }

    public void InventorySet(Inventory other)
    {
        this.currency = other.currency;
        this.items = new Dictionary<TradeItem, int>(other.items);
    }

    public void Add(TradeItem toAdd, int amount)
    {
        if (items.ContainsKey(toAdd))
        {
            items[toAdd] += 1;
        }
        else
        {
            items.Add(toAdd, 1);
        }
    }

    public void Add(TradeItem toAdd)
    {
        Add(toAdd, 1);
    }

    public void AddCollection(Dictionary<TradeItem,int> toAdd)
    {
        foreach (TradeItem add in toAdd.Keys)
        {
            Add(add, toAdd[add]);
        }
    }

    public void Remove(TradeItem toRemove, int amount)
    {
        Log("Remove:" + toRemove.ToString() + "From:" + ToString() + "Contains:" + items.ContainsKey(toRemove));
        if (items.ContainsKey(toRemove))
        {
            if (items[toRemove] > amount)
            {
                items[toRemove] -= amount;
            }
            else
            {
                items.Remove(toRemove);
            }
        }
    }

    public void Remove(TradeItem toRemove)
    {
        Remove(toRemove, 1);
    }

    public void RemoveCollection(Dictionary<TradeItem,int> toRemove)
    {
        foreach (TradeItem remove in toRemove.Keys)
        {
            if (items.ContainsKey(remove))
            {
                Remove(remove, toRemove[remove]);
            }
        }
    }

    public Dictionary<TradeItem, int> SeeContents()
    {
        return new Dictionary<TradeItem, int>(items);
    }

    public string ToString()
    {
        string result = "Inventory [";

        foreach (TradeItem item in items.Keys)
        {
            result += "(";

            result += item.ToString();
            result += " ";
            result += items[item];
            result += ")";
        }

        result += "]";

        return result;
    }
}
