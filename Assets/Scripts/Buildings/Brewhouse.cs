/**
 * Class:Brewhouse
 * Purpose:Provides the functionality of a Brew house for a Brewmaster. Creates beer.
 * 
 * public fields:
 *  
 * public methods:
 *  void Start(): 
 *  ItemType CraftBeer(Item): Takes one barley and returns one beer
 * 
 * @author: NvS 
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Brewhouse : BaseBuilding
{

    public void Start()
    {
        base.Start();
        this.debug = false;
    }

    public ItemType CraftBeer(Item input)
    {
        ItemType produces = ItemType.INVALID;
        if (input.Type == ItemType.BARLEY)
        {
            produces = ItemType.BEER;
        }
        return produces;
    }
}

