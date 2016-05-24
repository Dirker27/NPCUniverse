using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Mine : BaseBuilding
{
    public ItemType produces;

    public void Start()
    {
        base.Start();
        this.produces = ItemType.ORE;
        this.debug = false;
    }

    public ItemType WorkMine()
    {
        logger.Log(debug, "Being worked");
        return this.produces;
    }
}

