/**
 * Class:Barn
 * Purpose:Provides storage for item
 * 
 * public fields:
 *  
 * public methods:
 *  void Start(): Sets the debug value for the barn
 * 
 * @author: NvS 
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Barn : BaseBuilding
{

    public override void Start()
    {
        base.Start();
        this.debug = false;
    }

    public bool GetBarley(Instruction instruction, CharacterSheet sheet)
    {
        bool result = false;
        if (instruction.give.Length == 0 && instruction.gather[0] == ItemType.BARLEY)
        {
            foreach (Item item in inventory.items.Keys)
            {
                if (item.Type == ItemType.BARLEY)
                {
                    sheet.inventory.Add(item);
                    inventory.Remove(item);
                    result = true;
                    break;
                }
            }
        }
        return result;
    }

    public bool StoreCrop(Instruction instruction, CharacterSheet sheet)
    {
        bool result = false;
        if ((instruction.give[0] == ItemType.BARLEY  || instruction.give[0] == ItemType.WHEAT)
            && instruction.gather.Length == 0)
        {
            foreach (Item item in sheet.inventory.items.Keys)
            {
                if (item.Type == instruction.give[0])
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

    public bool StoreFish(Instruction instruction, CharacterSheet sheet)
    {
        bool result = false;
        if (instruction.give[0] == ItemType.FISH && instruction.gather.Length == 0)
        {
            foreach (Item item in sheet.inventory.items.Keys)
            {
                if (item.Type == ItemType.FISH)
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

