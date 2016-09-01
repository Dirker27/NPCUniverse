/**
 * Class:SawHouse
 * Purpose:Provides the functionality of a SawHouse for a SawWorker
 * 
 * public fields:
 *  
 * public methods:
 *  void Start(): 
 *  ItemType MakePlanks(Item): Takes one log and makes one lumberplank.
 * 
 * @author: NvS 
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SawHouse : BaseBuilding
{
    public override void Start()
    {
        base.Start();
        this.debug = false;

        canHold = new List<ItemType> { ItemType.LUMBERPLANK };

        supportedRecipes.Add(MasterRecipe.Instance.LumberPlank);
    }
}

