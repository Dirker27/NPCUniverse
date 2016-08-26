/**
 * Class:Mill
 * Purpose:Provides the functionality of a Mill for a Miller
 * 
 * public fields:
 *  
 * public methods:
 *  void Start(): 
 *  ItemType MakeFlour(Item): Takes one wheat and returns one flour
 * 
 * @author: NvS 
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Mill : BaseBuilding
{
    public override void Start()
    {
        base.Start();
        this.debug = false;
    }

    public ItemType MakeFlour(Item input)
    {
        ItemType produces = ItemType.INVALID;
        if (input.Type == ItemType.WHEAT)
        {
            produces = ItemType.FLOUR;
        }
        return produces;
    }

    public bool GetFlour(Instruction instruction, CharacterSheet sheet)
    {
        bool result = false;
        if (instruction.give.Length == 0 && instruction.gather[0] == ItemType.FLOUR)
        {
            foreach (Item item in inventory.items.Keys)
            {
                if (item.Type == ItemType.FLOUR)
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

