/**
 * Class:Brewhouse
 * Purpose:Provides the functionality of a Brew house for a Brewmaster. Creates beer.
 * 
 * public fields:
 *  
 * public methods:
 *  void Start(): 
 *  ItemType CraftBeer(Item): Takes one barley and returns one beer
 * 
 * @author: NvS 
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Brewhouse : BaseBuilding
{

    public override void Start()
    {
        base.Start();
        this.debug = false;
    }

    public ItemType CraftBeer(Item input)
    {
        ItemType produces = ItemType.INVALID;
        if (input.Type == ItemType.BARLEY)
        {
            produces = ItemType.BEER;
        }
        return produces;
    }

    public bool MakeBeer(Instruction instruction, CharacterSheet sheet)
    {
        if (instruction.give[0] == ItemType.BARLEY && instruction.gather[0] == ItemType.BEER)
        {
            Item beer = GameObject.FindGameObjectWithTag("GameManager").AddComponent<Item>();
            beer.Type = ItemType.BEER;
            beer.PurchasedPrice = 0;
            sheet.inventory.Add(beer);
            return true;
        }
        return false;
    }

    public bool StoreBeer(Instruction instruction, CharacterSheet sheet)
    {
        bool result = false;
        if (instruction.give[0] == ItemType.BEER && instruction.gather.Length == 0)
        {
            foreach (Item item in sheet.inventory.items.Keys)
            {
                if (item.Type == ItemType.BEER)
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

