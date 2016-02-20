using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TradeEngine
{
    class Inventory
    {
        public int Currency { get; set; }
        public List<Item> Items { get; set; }

        public Inventory(int currency, List<Item> items)
        {
            Currency = currency;
            Items = items;
        }
    }
}
