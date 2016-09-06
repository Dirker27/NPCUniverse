/**
 * Class:CharcoalPit
 * Purpose:Provides the functionality of a Charcoal pit for a Colier. Creates coal.
 * 
 * Supports the Charcoal recipie
 * Can Store Charcoal
 * 
 * public fields:
 *  
 * public methods:
 *  void Start(): 
 *  ItemType MakeCharcoal(Item): Takes one log and returns one charcoal
 * 
 * @author: NvS 
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharcoalPit : BaseBuilding
{

    public override void Start()
    {
        base.Start();
        this.debug = false;

        canHold = new List<ItemType> { ItemType.CHARCOAL };

        supportedRecipes.Add(MasterRecipe.Instance.Charcoal);
    }

}

