using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.TradeEngine
{
    class TradeOrders
    {
        public TradeRoute Destination { get; set; }
        public List<TradeItem> Manifests { get; set; }

        public TradeOrders(TradeRoute destination, List<TradeItem> manifest)
        {
            Destination = destination;
            Manifests = manifest;
        }
    }
}
