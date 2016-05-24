using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TradeOrders : MonoBehaviour
{
    public TradeRoute Destination;
    public Dictionary<Item, int> Manifests;

    public TradeOrders(TradeRoute Destination, Dictionary<Item, int> Manifests)
    {
        this.Destination = Destination;
        this.Manifests = Manifests;
    }
}
