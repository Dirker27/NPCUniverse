using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Item
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

    public static string ListToString(List<Item> items)
    {
        string output = "(";

        if (items != null)
        {
            foreach (Item item in items)
            {
                output += "[" + item.ToString() + " : " + item + " ";
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

        Item other = o as Item;
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

