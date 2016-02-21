using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.TradeEngine
{
    class TradeInventory
    {
        public int Currency { get; set; }
        public List<TradeItem> Items { get; set; }

        public TradeInventory(int currency, List<TradeItem> items)
        {
            Currency = currency;
            Items = items;
        }
    }
}
