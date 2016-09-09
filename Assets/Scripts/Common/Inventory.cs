using UnityEngine;
using System.Collections;
using System.Collections.Generic;  // dafuq? - No. Fuck You, Nathaniel.

public class Inventory
{
    public int currency;
    public List<Item> items;

    public void InventorySet(Inventory other)
    {
        this.currency = other.currency;
        this.items = new List<Item>(other.items);
    }

    public void Add(Item toAdd)
    {
        items.Add(toAdd);
    }

    public void AddCollection(List<Item> toAdd)
    {
        foreach (Item add in toAdd)
        {
            Add(add);
        }
    }

    public void Remove(Item toRemove)
    {
        items.Remove(toRemove);
    }

    public void RemoveCollection(List<Item> toRemove)
    {
        foreach (Item remove in toRemove)
        {
            items.Remove(remove);
        }
    }

    public override string ToString()
    {
        string result = "Inventory [";

        foreach (Item item in items)
        {
            result += "(";

            result += item.ToString();
            result += " ";
            result += item;
            result += ")";
        }

        result += "]";

        return result;
    }
}
