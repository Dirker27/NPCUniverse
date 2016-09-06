/**
 * Class:Masonry
 * Purpose:Provides the functionality of a Masonry for a Mason
 * 
 * Supports the Stoneblock recipie
 * Can Store Stoneblocks
 * 
 * public fields:
 *  
 * public methods:
 *  void Start(): 
 *  ItemType CutStone(Item): Takes one stone and returns one stoneblock
 * 
 * @author: NvS 
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Masonry : BaseBuilding
{
    public override void Start()
    {
        base.Start();
        this.debug = false;


        canHold = new List<ItemType> { ItemType.STONEBLOCK };

        supportedRecipes.Add(MasterRecipe.Instance.StoneBlock);
    }
}

