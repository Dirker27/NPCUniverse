using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TradeEngine
{
    class NPC
    {
        public Inventory Inventory { get; set; }

        public NPC(Inventory inventory)
        {
            Inventory = inventory;
        }
    }
}
