/**
 * Class:Forest
 * Purpose:Provides the functionality of a Forest for a Forester. Creates logs.
 * 
 * Supports the Log recipie
 * 
 * public fields:
 *  
 * public methods:
 *  void Start(): 
 *  ItemType WorkForest(): Returns one log
 * 
 * @author: NvS 
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Forest : BaseBuilding
{
    public override void Start()
    {
        base.Start();
        this.debug = false;

        supportedRecipes.Add(MasterRecipe.Instance.Log);

        CurrentPositions.Add(Jobs.FORESTER, 1);
        TotalPositions.Add(Jobs.FORESTER, 1);
        Register();
    }

}

