/**
 * Class:Smithy
 * Purpose:Provides the functionality of a Smithy for a Weapon or Armor smith or Tool smith
 * 
 * public fields:
 *  
 * public methods:
 *  void Start(): 
 *  ItemType WorkSmithyArmor(Item): Takes one bar and returns Armor
 *  ItemType WorkSmithyWeapon(Item): Takes one bar and returns Weapon
 *  ItemType WorkSmithyTool(Item): Takes one bar and returns Tool
 * 
 * @author: NvS 
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Smithy : BaseBuilding
{
    public override void Start()
    {
        base.Start();
        this.debug = false;

        canHold = new List<ItemType> { ItemType.ARMOR, ItemType.WEAPON, ItemType.TOOL };

        supportedRecipes.Add(MasterRecipe.Instance.Armor);
        supportedRecipes.Add(MasterRecipe.Instance.Weapon);
        supportedRecipes.Add(MasterRecipe.Instance.Tool);
    }

}

