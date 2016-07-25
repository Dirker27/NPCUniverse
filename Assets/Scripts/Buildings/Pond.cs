/**
 * Class:Pond
 * Purpose:Provides the functionality of a Pond for a Fisherman
 * 
 * public fields:
 *  
 * public methods:
 *  void Start(): 
 *  ItemType FishPond(Item): Returns one fish
 * 
 * @author: NvS 
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pond : BaseBuilding
{

    public void Start()
    {
        base.Start();
        this.debug = false;
    }

    public ItemType FishPond()
    {
        return ItemType.FISH;
    }
}

