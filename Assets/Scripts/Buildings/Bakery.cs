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
        this.debug = false;
    }

    public ItemType WorkBakery(Item input)
    {
        logger.Log(debug, "Being worked");
        return this.produces;
    }
}

