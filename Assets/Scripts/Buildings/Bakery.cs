using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bakery : BaseBuilding
{
    public ItemType produces;

    public void Start()
    {
        base.Start();
        this.produces = ItemType.BREAD;
        this.debug = true;
    }

    public ItemType WorkBakery(TradeItem input)
    {
        logger.Log(debug, "Being worked");
        return this.produces;
    }
}

