/**
 * Class:Farm
 * Purpose:Provides the functionality of a farm for a farmer. Creates barley and wheat.
 * 
 * Supports the Wheat and Barley recipie
 * 
 * public fields:
 *  
 * public methods:
 *  void Start(): 
 *  ItemType WorkFarm(Item): Produces one Item; TODO this should take in some kind of seed and grow a crop based on seed
 * 
 * @author: NvS 
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Farm : BaseBuilding
{

    public override void Start()
    {
        base.Start();
        this.debug = false;
        
        supportedRecipes.Add(MasterRecipe.Instance.Wheat);
        supportedRecipes.Add(MasterRecipe.Instance.Barley);

        CurrentPositions.Add(Jobs.FARMER, 2);
        TotalPositions.Add(Jobs.FARMER, 2);
        Register();
    }

}

