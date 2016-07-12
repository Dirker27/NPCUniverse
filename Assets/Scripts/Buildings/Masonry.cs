using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Masonry : BaseBuilding
{
    public ItemType produces;

    public void Start()
    {
        base.Start();
        this.produces = ItemType.STONEBLOCK;
        this.debug = false;
    }

    public ItemType CutStone(Item input)
    {
        logger.Log(debug, "Being worked");
        return this.produces;
    }
}

