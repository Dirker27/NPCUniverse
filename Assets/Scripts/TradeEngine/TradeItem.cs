using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TradeItem : MonoBehaviour
{

    public ItemType Type;
    public string Name;
    public int PurchasedPrice;

    public static string ListToString(Dictionary<TradeItem,int> items)
    {
        string output = "(";

        if (items != null)
        {
            foreach (TradeItem item in items.Keys)
            {
                output += "[" + item.ToString() + " : " + items[item] + " ";
            }
        }

        output += ")";
        return output;
    }

    public override string ToString() 
    {
        string output = "[" + Type + " " + Name + " " + PurchasedPrice + "]";

        return output;
    }

    public override bool Equals(object o)
    {
        if (o == null)
        {
            return false;
        }

        TradeItem other = o as TradeItem;
        if ((object)other == null)
        {
            return false;
        }

        return (this.Type == other.Type) && (this.Name == other.Name) && (this.PurchasedPrice == other.PurchasedPrice);
    }
}

