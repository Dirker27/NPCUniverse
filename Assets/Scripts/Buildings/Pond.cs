/**
 * Class:Pond
 * Purpose:Provides the functionality of a Pond for a Fisherman
 * 
 * Supports the Fish recipe
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

public class Pond : BaseBuilding
{

    public override void Start()
    {
        base.Start();
        this.debug = false;
        
        supportedRecipes.Add(MasterRecipe.Instance.Fish);
        logger.Log(debug, "woodCut constructor");

        CurrentPositions.Add(Jobs.FISHERMAN, 1);
        TotalPositions.Add(Jobs.FISHERMAN, 1);
        Register();
    }
}

