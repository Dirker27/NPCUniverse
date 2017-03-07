/**
 * Class:WoodCut
 * Purpose:Provides the functionality of a WoodCut for a WoodCuter
 * Supports the FireWood recipe
 * Can Store FireWood
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

public class WoodCut : BaseBuilding
{
    public override void Start()
    {
        base.Start();
        this.debug = false;

        canHold = new List<ItemType> { ItemType.FIREWOOD };

        supportedRecipes.Add(MasterRecipe.Instance.FireWood);

        CurrentPositions.Add(Jobs.WOODCUTER, 1);
        TotalPositions.Add(Jobs.WOODCUTER, 1);
        Register();
    }

}

