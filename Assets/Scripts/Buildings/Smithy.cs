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
    public void Start()
    {
        base.Start();
        this.debug = false;
    }

    public ItemType WorkSmithyArmor(Item input)
    {
        ItemType produces = ItemType.INVALID;
        if (input.Type == ItemType.BAR)
        {
            produces = ItemType.ARMOR;
        }
        return produces;
    }

    public ItemType WorkSmithyWeapon(Item input)
    {
        ItemType produces = ItemType.INVALID;
        if (input.Type == ItemType.BAR)
        {
            produces = ItemType.WEAPON;
        }
        return produces;
    }

    public ItemType WorkSmithyTool(Item input)
    {
        ItemType produces = ItemType.INVALID;
        if (input.Type == ItemType.BAR)
        {
            produces = ItemType.TOOL;
        }
        return produces;
    }
}

