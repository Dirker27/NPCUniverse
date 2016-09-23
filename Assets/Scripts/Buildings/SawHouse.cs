/**
 * Class:SawHouse
 * Purpose:Provides the functionality of a SawHouse for a SawWorker
 * 
 * Supports the LumberPlank recipe
 * Can Store LumberPlanks
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

public class SawHouse : BaseBuilding
{
    public override void Start()
    {
        base.Start();
        this.debug = false;

        canHold = new List<ItemType> { ItemType.LUMBERPLANK };

        supportedRecipes.Add(MasterRecipe.Instance.LumberPlank);

        CurrentPositions.Add(Jobs.SAWWORKER, 1);
        TotalPositions.Add(Jobs.SAWWORKER, 1);
        Register();
    }
}

