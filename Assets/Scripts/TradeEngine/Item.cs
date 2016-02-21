using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.TradeEngine
{
    class Item
    {

        public string Name { get; set; }
        public int PurchasedPrice { get; set; }
        public Item(string name)
        {
            Name = name;
        }
    }
}
