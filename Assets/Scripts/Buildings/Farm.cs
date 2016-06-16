using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Farm : BaseBuilding
{
    public ItemType produces;

    public void Start()
    {
        base.Start();
        this.debug = false;
    }

    public ItemType WorkFarm()
    {
        logger.Log(debug, "Being worked");
        return this.produces;
    }
}

