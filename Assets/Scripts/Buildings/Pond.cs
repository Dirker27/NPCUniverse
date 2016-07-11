using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pond : BaseBuilding
{
    public ItemType produces;

    public void Start()
    {
        base.Start();
        this.produces = ItemType.FISH;
        this.debug = false;
    }

    public ItemType FishPond()
    {
        logger.Log(debug, "Being worked");
        return this.produces;
    }
}

