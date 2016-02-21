using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.TradeEngine
{
    class TradeOrders
    {
        public Route Destination { get; set; }
        public List<Item> Manifests { get; set; }

        public TradeOrders(Route destination, List<Item> manifest)
        {
            Destination = destination;
            Manifests = manifest;
        }
    }
}
