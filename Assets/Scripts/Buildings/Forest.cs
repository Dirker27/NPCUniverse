using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Forest : BaseBuilding
{
    public ItemType produces;

    public void Start()
    {
        base.Start();
        this.produces = ItemType.LOG;
        this.debug = false;
    }

    public ItemType WorkForest()
    {
        logger.Log(debug, "Being worked");
        return this.produces;
    }
}

