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

    public bool MakeArmor(Instruction instruction, CharacterSheet sheet)
    {
        if (instruction.give[0] == ItemType.BAR && instruction.gather[0] == ItemType.ARMOR)
        {
            Item armor = GameObject.FindGameObjectWithTag("GameManager").AddComponent<Item>();
            armor.Type = ItemType.ARMOR;
            armor.PurchasedPrice = 0;
            sheet.inventory.Add(armor);
            return true;
        }
        return false;
    }

    public bool StoreArmor(Instruction instruction, CharacterSheet sheet)
    {
        bool result = false;
        if (instruction.give[0] == ItemType.ARMOR && instruction.gather.Length == 0)
        {
            foreach (Item item in sheet.inventory.items.Keys)
            {
                if (item.Type == ItemType.ARMOR)
                {
                    inventory.Add(item);
                    sheet.inventory.Remove(item);
                    result = true;
                    break;
                }
            }
        }
        return result;
    }
}

