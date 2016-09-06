/**
 * Class:HuntingLodge
 * Purpose:Provides the functionality of a HuntingLodge for a Hunter. Creates meat and leather.
 * 
 * Supports the Meat and Leather recipie
 * Can Store Meat and Leather
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

public class HuntingLodge : BaseBuilding
{
    public override void Start()
    {
        base.Start();
        this.debug = false;

        canHold = new List<ItemType> { ItemType.MEAT, ItemType.LEATHER };

        supportedRecipes.Add(MasterRecipe.Instance.Meat);
        supportedRecipes.Add(MasterRecipe.Instance.Leather);
    }
}

