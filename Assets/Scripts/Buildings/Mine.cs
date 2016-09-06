/**
 * Class:Mine
 * Purpose:Provides the functionality of a Mine for a Miner
 * 
 * Supports the Ore and Stone recipe
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

public class Mine : BaseBuilding
{
    public ItemType produces;

    public override void Start()
    {
        base.Start();
        this.debug = false;
        
        supportedRecipes.Add(MasterRecipe.Instance.Ore);
        supportedRecipes.Add(MasterRecipe.Instance.Stone);
    }
}

