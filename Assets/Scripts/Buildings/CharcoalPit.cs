using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharcoalPit : BaseBuilding
{
    public ItemType produces;

    public void Start()
    {
        base.Start();
        this.produces = ItemType.CHARCOAL;
        this.debug = true;
    }

    public ItemType WorkCharcoalHouse(TradeItem input)
    {
        logger.Log(debug, "Being worked");
        return this.produces;
    }
}

