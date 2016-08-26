/**
 * Class:Bakery
 * Purpose:Provides the functionality of a Bakery for a Baker. Creates bread.
 * 
 * public fields:
 *  
 * public methods:
 *  void Start(): 
 *  ItemType BakeBread(Item): Takes one flour and returns one bread
 * 
 * @author: NvS 
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bakery : BaseBuilding
{
    public override void Start()
    {
        base.Start();
        this.debug = false;
    }

    public ItemType BakeBread(Item input)
    {
        ItemType produced = ItemType.INVALID;
        if(input.Type == ItemType.FLOUR)
        {
            produced = ItemType.BREAD;
        }
        return produced;
    }

    public bool MakeBread(Instruction instruction, CharacterSheet sheet)
    {
        if (instruction.give[0] == ItemType.FLOUR && instruction.gather[0] == ItemType.BREAD)
        {
            Item bread = GameObject.FindGameObjectWithTag("GameManager").AddComponent<Item>();
            bread.Type = ItemType.BREAD;
            bread.PurchasedPrice = 0;
            sheet.inventory.Add(bread);
            return true;
        }
        return false;
    }

    public bool StoreBread(Instruction instruction, CharacterSheet sheet)
    {
        bool result = false;
        if (instruction.give[0] == ItemType.BREAD && instruction.gather.Length == 0)
        {
            foreach (Item item in sheet.inventory.items.Keys)
            {
                if (item.Type == ItemType.BREAD)
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

