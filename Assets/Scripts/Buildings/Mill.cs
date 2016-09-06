/**
 * Class:Mill
 * Purpose:Provides the functionality of a Mill for a Miller
 * 
 * Supports the Flour recipie
 * Can Store Flour
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

public class Mill : BaseBuilding
{
    public override void Start()
    {
        base.Start();
        this.debug = false;

        canHold = new List<ItemType> { ItemType.FLOUR};

        supportedRecipes.Add(MasterRecipe.Instance.Flour);
    }

}

