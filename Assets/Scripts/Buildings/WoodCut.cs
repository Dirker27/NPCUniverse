/**
 * Class:WoodCut
 * Purpose:Provides the functionality of a WoodCut for a WoodCuter
 * 
 * public fields:
 *  
 * public methods:
 *  void Start(): 
 *  ItemType MakeFireWood(Log): Takes one log and returns one firewood;
 * 
 * @author: NvS 
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WoodCut : BaseBuilding
{
    public override void Start()
    {
        base.Start();
        this.debug = false;
    }

    public ItemType MakeFireWood(Item input)
    {
        ItemType produces = ItemType.INVALID;
        if (input.Type == ItemType.LOG)
        {
            produces = ItemType.FIREWOOD;
        }
        return produces;
    }

    public bool GetFirewood(Instruction instruction, CharacterSheet sheet)
    {
        bool result = false;
        if (instruction.give.Length == 0 && instruction.gather[0] == ItemType.FIREWOOD)
        {
            foreach (Item item in inventory.items.Keys)
            {
                if (item.Type == ItemType.FIREWOOD)
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
}

