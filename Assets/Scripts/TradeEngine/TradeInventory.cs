using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class TradeInventory
{
    public int Currency { get; set; }
    public List<Item> Items { get; set; }

    public TradeInventory(int currency, List<Item> items)
    {
        Currency = currency;
        Items = items;
    }
}
