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

    public bool MakeArmor(Instruction instruction)
    {
        if (instruction.give[0] == ItemType.BAR && instruction.gather[0] == ItemType.ARMOR)
        {
            return true;
        }
        return false;
    }

    public bool StoreArmor(Instruction instruction)
    {
        if (instruction.give[0] == ItemType.ARMOR && instruction.gather.Length == 0)
        {
            return true;
        }
        return false;
    }

    public override bool DoAction(Instruction instruction, CharacterSheet sheet)
    {
        bool toReturn = false;
        switch (instruction.Action)
        {            
            case "MakeArmor":
                toReturn = MakeArmor(instruction);
                break;
            
            case "StoreArmor":
                toReturn = StoreArmor(instruction);
                break;

            default:
                // log that a miss match instruction has arrived
                toReturn = false;
                break;
        }
        return toReturn;
    }
}

