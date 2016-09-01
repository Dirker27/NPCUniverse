/**
 * Class:BowShop
 * Purpose:Provides the functionality of a BowShop for a Fletcher. Creates bow and arrows.
 * 
 * public fields:
 *  
 * public methods:
 *  void Start():
 *  ItemType CraftBow(Item): Takes one log and returns one bow
 *  ItemType CraftArrow(Item): Takes one log and returns one arrow
 * 
 * @author: NvS 
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BowShop : BaseBuilding
{

    public override void Start()
    {
        base.Start();
        this.debug = false;

        canHold = new List<ItemType> { ItemType.BOW, ItemType.ARROW};

        supportedRecipes.Add(MasterRecipe.Instance.Bow);
        supportedRecipes.Add(MasterRecipe.Instance.Arrow);
    }
}

