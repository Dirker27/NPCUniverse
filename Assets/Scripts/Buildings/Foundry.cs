using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Foundry : BaseBuilding
{
    public ItemType produces;

    public void Start()
    {
        base.Start();
        this.produces = ItemType.BAR;
        this.debug = false;
    }

    public ItemType WorkFoundry(Item input)
    {
        return produces;
    }
        
}

