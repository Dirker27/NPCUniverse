/**
 * Class:Foundry
 * Purpose:Provides the functionality of a Foundry for a Smith. Creates bars.
 * 
 * public fields:
 *  
 * public methods:
 *  void Start(): 
 *  ItemType WorkFoundry(Item): Takes one ore and returns one bar
 * 
 * @author: NvS 
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Foundry : BaseBuilding
{

    public void Start()
    {
        base.Start();
        this.debug = false;
    }

    public ItemType WorkFoundry(Item input)
    {

        ItemType produces = ItemType.INVALID;
        if (input.Type == ItemType.ORE)
        {
            produces = ItemType.BAR;
        }
        return produces;
    }
        
}

