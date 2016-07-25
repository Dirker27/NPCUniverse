/**
 * Class:Bakery
 * Purpose:Provides the functionality of a Bakery for a Baker. Creates bread.
 * 
 * public fields:
 *  
 * public methods:
 *  void Start(): 
 *  ItemType BakeBread(Item): Takes one flour and returns one bread
 * 
 * @author: NvS 
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bakery : BaseBuilding
{
    public void Start()
    {
        base.Start();
        this.debug = false;
    }

    public ItemType BakeBread(Item input)
    {
        ItemType produced = ItemType.INVALID;
        if(input.Type == ItemType.FLOUR)
        {
            produced = ItemType.BREAD;
        }
        return produced;
    }
}

