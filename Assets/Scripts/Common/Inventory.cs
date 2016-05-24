using UnityEngine;
using System.Collections;
using System.Collections.Generic;  // dafuq? - No. Fuck You, Nathaniel.

public class Inventory : MonoBehaviour
{
    public int currency;
    public Dictionary<Item, int> items;

    private bool debug = false;

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
        this.items = new Dictionary<Item, int>(other.items);
    }

    public void Add(Item toAdd, int amount)
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

    public void Add(Item toAdd)
    {
        Add(toAdd, 1);
    }

    public void AddCollection(Dictionary<Item,int> toAdd)
    {
        foreach (Item add in toAdd.Keys)
        {
            Add(add, toAdd[add]);
        }
    }

    public void Remove(Item toRemove, int amount)
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

    public void Remove(Item toRemove)
    {
        Remove(toRemove, 1);
    }

    public void RemoveCollection(Dictionary<Item,int> toRemove)
    {
        foreach (Item remove in toRemove.Keys)
        {
            if (items.ContainsKey(remove))
            {
                Remove(remove, toRemove[remove]);
            }
        }
    }

    public Dictionary<Item, int> SeeContents()
    {
        return new Dictionary<Item, int>(items);
    }

    public override string ToString()
    {
        string result = "Inventory [";

        foreach (Item item in items.Keys)
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
