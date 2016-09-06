/**
 * Class:Brewhouse
 * Purpose:Provides the functionality of a Brew house for a Brewmaster. Creates beer.
 * 
 * Supports the Beer recipie
 * Can Store Beer
 * 
 * public fields:
 *  
 * public methods:
 *  void Start(): 
 *  ItemType CraftBeer(Item): Takes one barley and returns one beer
 * 
 * @author: NvS 
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Brewhouse : BaseBuilding
{

    public override void Start()
    {
        base.Start();
        this.debug = false;

        canHold = new List<ItemType> { ItemType.BEER };

        supportedRecipes.Add(MasterRecipe.Instance.Beer);
    }

}

