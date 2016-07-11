using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tavern : BaseBuilding
{
    public ItemType produces;

    public void Start()
    {
        base.Start();
        this.produces = ItemType.MEAL;
        this.debug = false;
    }

    public ItemType MakeMeal(Item inputbread, Item inputfish, Item inputbeer)
    {
        logger.Log(debug, "Being worked");
        return this.produces;
    }
}

