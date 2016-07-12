using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SawHouse : BaseBuilding
{
    public ItemType produces;

    public void Start()
    {
        base.Start();
        this.produces = ItemType.LUMBERPLANK;
        this.debug = false;
    }

    public ItemType MakePlanks(Item input)
    {
        logger.Log(debug, "Being worked");
        return this.produces;
    }
}

