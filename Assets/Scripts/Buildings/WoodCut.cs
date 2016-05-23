using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WoodCut : BaseBuilding
{
    public ItemType produces;

    public void Start()
    {
        base.Start();
        this.produces = ItemType.FIREWOOD;
        this.debug = false;
    }

    public ItemType WorkWoodCut(TradeItem input)
    {
        logger.Log(debug, "Being worked");
        return this.produces;
    }
}

