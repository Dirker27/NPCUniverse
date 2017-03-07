using UnityEngine;
using System;
using System.Collections.Generic;

public class TradeData : MonoBehaviour
{
    public ItemType Item;
    public int BaseCost;
    public int DesiredAmount;
    public int CurrentAmount;

    public int CurrentCost()
    {
        // TODO: Transforms

        return BaseCost;
    }
}