/**
 * Class:OreShop
 * Purpose:Provides the functionality of a OreShop for a miner
 * 
 * Can Store Ore and Stone
 * 
 * public fields:
 *  
 * public methods:
 *  void Start(): 
 * 
 * @author: NvS 
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OreShop : BaseBuilding
{

    public override void Start()
    {
        base.Start();
        this.debug = false;

        canHold = new List<ItemType> { ItemType.ORE, ItemType.STONE };
    }

}

