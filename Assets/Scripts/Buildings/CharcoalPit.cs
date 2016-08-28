/**
 * Class:CharcoalPit
 * Purpose:Provides the functionality of a Charcoal pit for a Colier. Creates coal.
 * 
 * public fields:
 *  
 * public methods:
 *  void Start(): 
 *  ItemType MakeCharcoal(Item): Takes one log and returns one charcoal
 * 
 * @author: NvS 
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharcoalPit : BaseBuilding
{

    public override void Start()
    {
        base.Start();
        this.debug = false;
    }

    public ItemType MakeCharcoal(Item input)
    {
        ItemType produces = ItemType.INVALID;
        if (input.Type == ItemType.LOG)
        {
            produces = ItemType.CHARCOAL;
        }
        return produces;
    }

    public bool MakeCharcoal(Instruction instruction, CharacterSheet sheet)
    {
        if (instruction.give[0] == ItemType.FIREWOOD && instruction.gather[0] == ItemType.CHARCOAL)
        {
            Item charcoal = GameObject.FindGameObjectWithTag("GameManager").AddComponent<Item>();
            charcoal.Type = ItemType.CHARCOAL;
            charcoal.PurchasedPrice = 0;
            sheet.inventory.Add(charcoal);
            return true;
        }
        return false;
    }

    public bool StoreCharcoal(Instruction instruction, CharacterSheet sheet)
    {
        bool result = false;
        if (instruction.give[0] == ItemType.CHARCOAL && instruction.gather.Length == 0)
        {
            foreach (Item item in sheet.inventory.items.Keys)
            {
                if (item.Type == ItemType.CHARCOAL)
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

