using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Farm : BaseBuilding
{
    public ItemType produces;

    public void Start()
    {
        base.Start();
        this.produces = ItemType.WHEAT;
        this.debug = false;
    }

    public ItemType WorkFarm()
    {
        logger.Log(debug, "Being worked");
        return this.produces;
    }
}

