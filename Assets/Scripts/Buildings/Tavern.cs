/**
 * Class:Tavern
 * Purpose:Provides the functionality of a Tavern for an InnKeeper
 * 
 * public fields:
 *  
 * public methods:
 *  void Start(): 
 *  ItemType MakeMeal(Bread, Fish, Beer): Takes a Bread, Fish, and Beer to make one meal
 * 
 * @author: NvS 
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tavern : BaseBuilding
{
    public void Start()
    {
        base.Start();
        this.debug = false;
    }

    public ItemType MakeMeal(Item inputbread, Item inputfish, Item inputbeer)
    {
        ItemType produces = ItemType.INVALID;
        if (inputbread.Type == ItemType.BREAD &&
            inputfish.Type == ItemType.FISH &&
            inputbeer.Type == ItemType.BEER)
        {
            produces = ItemType.MEAL;
        }
        return produces;
    }
}

