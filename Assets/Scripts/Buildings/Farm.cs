/**
 * Class:Farm
 * Purpose:Provides the functionality of a farm for a farmer. Creates barley and wheat.
 * 
 * public fields:
 *  
 * public methods:
 *  void Start(): 
 *  ItemType WorkFarm(Item): Produces one Item; TODO this should take in some kind of seed and grow a crop based on seed
 * 
 * @author: NvS 
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Farm : BaseBuilding
{

    public override void Start()
    {
        base.Start();
        this.debug = false;
    }

    public ItemType WorkFarm(ItemType type)
    {
        ItemType produces = ItemType.INVALID;
        if (type == ItemType.WHEAT ||
            type == ItemType.BARLEY)
        {
            produces = type;
        }
        return produces;
    }

    public bool GetCrop(Instruction instruction, CharacterSheet sheet)
    {
        bool result = false;
        if (instruction.give.Length == 0 && 
            (instruction.gather[0] == ItemType.WHEAT || instruction.gather[0] == ItemType.BARLEY))
        {
            Item crop = GameObject.FindGameObjectWithTag("GameManager").AddComponent<Item>();
            crop.Type = instruction.gather[0];
            crop.PurchasedPrice = 0;
            sheet.inventory.Add(crop);
            return true;
        }
        return result;
    }

}

