using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HuntingLodge : BaseBuilding
{
    public ItemType produces1;
    public ItemType produces2;

    public void Start()
    {
        base.Start();
        this.produces1 = ItemType.MEAT;
        this.produces2 = ItemType.LEATHER;
        this.debug = false;
    }

    public ItemType GatherMeat(Item input1, Item input2)
    {
        logger.Log(debug, "Being worked");
        return this.produces1;
    }

    public ItemType GatherLeather(Item input1, Item input2)
    {
        logger.Log(debug, "Being worked");
        return this.produces2;
    }
}

