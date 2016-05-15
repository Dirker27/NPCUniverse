using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TradeOrders : MonoBehaviour
{
    public TradeRoute Destination;
    public Dictionary<TradeItem, int> Manifests;

    public TradeOrders(TradeRoute Destination, Dictionary<TradeItem, int> Manifests)
    {
        this.Destination = Destination;
        this.Manifests = Manifests;
    }
}
