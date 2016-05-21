using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Foundry : BaseBuilding
{
    public ItemType produces;

    public void Start()
    {
        base.Start();
        this.produces = ItemType.PROCESSEDGOOD;
        this.debug = false;
    }

    public ItemType WorkFoundry(TradeItem input)
    {
        return produces;
    }
        
}

