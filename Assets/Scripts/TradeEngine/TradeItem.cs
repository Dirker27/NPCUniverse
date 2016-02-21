using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.TradeEngine
{
    class TradeItem
    {

        public string Name { get; set; }
        public int PurchasedPrice { get; set; }
        public TradeItem(string name)
        {
            Name = name;
        }
    }
}
