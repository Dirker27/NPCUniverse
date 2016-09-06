using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TradeOrders : MonoBehaviour
{
    public TradeRoute Destination;
    public List<Item> Manifests;

    public TradeOrders(TradeRoute Destination, List<Item> Manifests)
    {
        this.Destination = Destination;
        this.Manifests = Manifests;
    }
}
