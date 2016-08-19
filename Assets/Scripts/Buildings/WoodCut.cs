/**
 * Class:WoodCut
 * Purpose:Provides the functionality of a WoodCut for a WoodCuter
 * 
 * public fields:
 *  
 * public methods:
 *  void Start(): 
 *  ItemType MakeFireWood(Log): Takes one log and returns one firewood;
 * 
 * @author: NvS 
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WoodCut : BaseBuilding
{
    public override void Start()
    {
        base.Start();
        this.debug = false;
    }

    public ItemType MakeFireWood(Item input)
    {
        ItemType produces = ItemType.INVALID;
        if (input.Type == ItemType.LOG)
        {
            produces = ItemType.FIREWOOD;
        }
        return produces;
    }
}

