/**
 * Class:Mill
 * Purpose:Provides the functionality of a Mill for a Miller
 * 
 * public fields:
 *  
 * public methods:
 *  void Start(): 
 *  ItemType MakeFlour(Item): Takes one wheat and returns one flour
 * 
 * @author: NvS 
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Mill : BaseBuilding
{
    public override void Start()
    {
        base.Start();
        this.debug = false;
    }

    public ItemType MakeFlour(Item input)
    {
        ItemType produces = ItemType.INVALID;
        if (input.Type == ItemType.WHEAT)
        {
            produces = ItemType.FLOUR;
        }
        return produces;
    }
}

