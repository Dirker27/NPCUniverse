using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BowShop : BaseBuilding
{
    public ItemType produces1;
    public ItemType produces2;

    public void Start()
    {
        base.Start();
        this.produces1 = ItemType.BOW;
        this.produces1 = ItemType.ARROW;
        this.debug = false;
    }

    public ItemType CraftBow(Item input)
    {
        logger.Log(debug, "Being worked");
        return this.produces1;
    }

    public ItemType CraftArrow(Item input)
    {
        logger.Log(debug, "Being worked");
        return this.produces2;
    }
}

