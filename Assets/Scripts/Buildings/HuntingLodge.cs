/**
 * Class:HuntingLodge
 * Purpose:Provides the functionality of a HuntingLodge for a Hunter. Creates meat and leather.
 * 
 * public fields:
 *  
 * public methods:
 *  void Start(): 
 *  ItemType GatherMeat(Item): Takes bow and arrow and returns one meat
 *  ItemType GatherLeather(Item): Takes bow and arrow and returns one leather
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

