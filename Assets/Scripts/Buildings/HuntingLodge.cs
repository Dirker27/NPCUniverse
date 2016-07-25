/**
 * Class:HuntingLodge
 * Purpose:Provides the functionality of a HuntingLodge for a Hunter. Creates meat and leather.
 * 
 * public fields:
 *  
 * public methods:
 *  void Start(): 
 *  ItemType GatherMeat(Item): Takes bow and arrow and returns one meat
 *  ItemType GatherLeather(Item): Takes bow and arrow and returns one leather
 * 
 * @author: NvS 
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HuntingLodge : BaseBuilding
{
    public void Start()
    {
        base.Start();
        this.debug = false;
    }

    public ItemType GatherMeat(Item input1, Item input2)
    {
        ItemType produces = ItemType.INVALID;
        if (input1.Type == ItemType.BOW &&
            input2.Type == ItemType.ARROW)
        {
            produces = ItemType.MEAT;
        }
        return produces;
    }

    public ItemType GatherLeather(Item input1, Item input2)
    {
        ItemType produces = ItemType.INVALID;
        if (input1.Type == ItemType.BOW &&
            input2.Type == ItemType.ARROW)
        {
            produces = ItemType.LEATHER;
        }
        return produces;
    }
}

