/**
 * Class:Foundry
 * Purpose:Provides the functionality of a Foundry for a Smith. Creates bars.
 * 
 * Supports the Bar recipie
 * Can Store Bars
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

    public override void Start()
    {
        base.Start();
        this.debug = false;
        
        canHold = new List<ItemType> { ItemType.BAR };
       
        supportedRecipes.Add(MasterRecipe.Instance.Bar);
    }        
}

