using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class TradeOrders
{
    public TradeRoute Destination { get; set; }
    public List<Item> Manifests { get; set; }

    public TradeOrders(TradeRoute destination, List<Item> manifest)
    {
        Destination = destination;
        Manifests = manifest;
    }
}
