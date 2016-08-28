/**
 * Class:Pond
 * Purpose:Provides the functionality of a Pond for a Fisherman
 * 
 * public fields:
 *  
 * public methods:
 *  void Start(): 
 *  ItemType FishPond(Item): Returns one fish
 * 
 * @author: NvS 
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pond : BaseBuilding
{

    public override void Start()
    {
        base.Start();
        this.debug = false;
    }

    public ItemType FishPond()
    {
        return ItemType.FISH;
    }

    public bool GetFish(Instruction instruction, CharacterSheet sheet)
    {
        if (instruction.give.Length == 0 && instruction.gather[0] == ItemType.FISH)
        {
            Item fish = GameObject.FindGameObjectWithTag("GameManager").AddComponent<Item>();
            fish.Type = ItemType.FISH;
            fish.PurchasedPrice = 0;
            sheet.inventory.Add(fish);
            return true;
        }
        return false;
    }
}

