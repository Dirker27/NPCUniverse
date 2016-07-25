/**
 * Class:Masonry
 * Purpose:Provides the functionality of a Masonry for a Mason
 * 
 * public fields:
 *  
 * public methods:
 *  void Start(): 
 *  ItemType CutStone(Item): Takes one stone and returns one stoneblock
 * 
 * @author: NvS 
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Masonry : BaseBuilding
{
    public void Start()
    {
        base.Start();
        this.debug = false;
    }

    public ItemType CutStone(Item input)
    {
        ItemType produces = ItemType.INVALID;
        if (input.Type == ItemType.STONE)
        {
            produces = ItemType.STONEBLOCK;
        }
        return produces;
    }
}

