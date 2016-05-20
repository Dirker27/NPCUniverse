using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Mill : BaseBuilding
{
    public ItemType produces;

    public void Start()
    {
        base.Start();
        this.produces = ItemType.FLOUR;
        this.debug = false;
    }

    public ItemType WorkMill(TradeItem input)
    {
        logger.Log(debug, "Being worked");
        return this.produces;
    }
}

