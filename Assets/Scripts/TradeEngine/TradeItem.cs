using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TradeItem : MonoBehaviour
{

    public ItemType Type;
    public int PurchasedPrice;

    private bool debug = false;

    void Log(string s)
    {
        if (debug)
        {
            Debug.Log("TradeItem log <" + s + ">");
        }
    }

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
        string output = "[" + Type + " " + " " + PurchasedPrice + "]";

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

        Log("This: " + this.ToString());
        Log("That: " + other.ToString());
        Log("Result :" + ((this.Type == other.Type) && (this.PurchasedPrice == other.PurchasedPrice)));
        return (this.Type == other.Type) && (this.PurchasedPrice == other.PurchasedPrice);
    }

    public override int GetHashCode()
    {
        int hash = 13;
        hash = (hash * 7) + this.Type.GetHashCode();
        hash = (hash * 7) + this.PurchasedPrice.GetHashCode();
        return hash;
    }
}

