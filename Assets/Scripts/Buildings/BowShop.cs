/**
 * Class:BowShop
 * Purpose:Provides the functionality of a BowShop for a Fletcher. Creates bow and arrows.
 * 
 * public fields:
 *  
 * public methods:
 *  void Start():
 *  ItemType CraftBow(Item): Takes one log and returns one bow
 *  ItemType CraftArrow(Item): Takes one log and returns one arrow
 * 
 * @author: NvS 
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BowShop : BaseBuilding
{

    public void Start()
    {
        base.Start();
        this.debug = false;
    }

    public ItemType CraftBow(Item input)
    {
        ItemType produced = ItemType.INVALID;
        if (input.Type == ItemType.LOG)
        {
            produced = ItemType.BOW;
        }
        return produced;
    }

    public ItemType CraftArrow(Item input)
    {
        ItemType produced = ItemType.INVALID;
        if (input.Type == ItemType.LOG)
        {
            produced = ItemType.ARROW;
        }
        return produced;
    }
}

