using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Brewhouse : BaseBuilding
{
    public ItemType produces;

    public void Start()
    {
        base.Start();
        this.produces = ItemType.BEER;
        this.debug = false;
    }

    public ItemType WorkBrewhouse(Item input)
    {
        logger.Log(debug, "Being worked");
        return this.produces;
    }
}

