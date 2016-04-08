using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TradeOrders : MonoBehaviour
{
    public TradeRoute Destination;
    public List<TradeItem> Manifests;

    public TradeOrders(TradeRoute Destination, List<TradeItem> Manifests)
    {
        this.Destination = Destination;
        this.Manifests = Manifests;
    }
}
