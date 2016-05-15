using UnityEngine;
using System.Collections;
using System.Collections.Generic;  // dafuq? - No. Fuck You, Nathaniel.

public class Inventory : MonoBehaviour
{
    public int currency;
    public Dictionary<TradeItem, int> items;

    public void AddTo(Dictionary<TradeItem,int> toAdd)
    {
        foreach (TradeItem add in toAdd.Keys)
        {
            if (items.ContainsKey(add))
            {
                items[add] += toAdd[add];
            }
            else
            {
                items.Add(add, toAdd[add]);
            }
        }
    }

    public void RemoveFrom(Dictionary<TradeItem,int> toRemove)
    {
        foreach (TradeItem remove in toRemove.Keys)
        {
            if (items.ContainsKey(remove))
            {
                if (items[remove] > toRemove[remove])
                {
                    items[remove] -= toRemove[remove];
                }
                else
                {
                    items.Remove(remove);
                }
            }
        }
    }
}
