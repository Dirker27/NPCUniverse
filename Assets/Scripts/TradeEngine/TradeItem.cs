using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TradeItem : MonoBehaviour
{

    public ItemType Type;
    public string Name;
    public int PurchasedPrice;

    public static string ListToString(List<TradeItem> items)
    {
        string output = "(";

        foreach(TradeItem item in items)
        {
            output += item.ToString() + " ";
        }

        output += ")";
        return output;
    }

    public override string ToString() 
    {
        string output = "[" + Type + " " + Name + " " + PurchasedPrice + "]";

        return output;
    }
}

