/**
 * Class:OreShop
 * Purpose:Provides the functionality of a OreShop for a miner
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

    public void Start()
    {
        base.Start();
        this.debug = false;
    }

}

