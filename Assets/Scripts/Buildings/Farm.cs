/**
 * Class:Farm
 * Purpose:Provides the functionality of a farm for a farmer. Creates barley and wheat.
 * 
 * public fields:
 *  
 * public methods:
 *  void Start(): 
 *  ItemType WorkFarm(Item): Produces one Item; TODO this should take in some kind of seed and grow a crop based on seed
 * 
 * @author: NvS 
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Farm : BaseBuilding
{

    public override void Start()
    {
        base.Start();
        this.debug = false;
    }

    public ItemType WorkFarm(ItemType type)
    {
        ItemType produces = ItemType.INVALID;
        if (type == ItemType.WHEAT ||
            type == ItemType.BARLEY)
        {
            produces = type;
        }
        return produces;
    }
}

