/**
 * Class:Tavern
 * Purpose:Provides the functionality of a Tavern for an InnKeeper
 * 
 * public fields:
 *  
 * public methods:
 *  void Start(): 
 *  ItemType MakeMeal(Bread, Fish, Beer): Takes a Bread, Fish, and Beer to make one meal
 * 
 * @author: NvS 
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tavern : BaseBuilding
{
    public override void Start()
    {
        base.Start();
        this.debug = false;
    }

    public ItemType MakeMeal(Item inputbread, Item inputfish, Item inputbeer)
    {
        ItemType produces = ItemType.INVALID;
        if (inputbread.Type == ItemType.BREAD &&
            inputfish.Type == ItemType.FISH &&
            inputbeer.Type == ItemType.BEER)
        {
            produces = ItemType.MEAL;
        }
        return produces;
    }

    public Item GetMeal()
    {
        Inventory fridge = PeekContents();
        Dictionary<Item, int> contents = fridge.SeeContents();

        Item meal = GameObject.FindGameObjectWithTag("GameManager").AddComponent<Item>();
        bool foundMeal = false;
        foreach (Item item in contents.Keys)
        {
            if (item.Type == ItemType.MEAL)
            {
                meal.Type = item.Type;
                meal.PurchasedPrice = item.PurchasedPrice;
                foundMeal = true;
            }
        }
        if (foundMeal)
        {
            Withdraw(meal);
        }
        return meal;
    }

    public bool Eat(Instruction instruction, CharacterSheet sheet)
    {
        if (instruction.give.Length == 0 && instruction.gather[0] == ItemType.MEAL)
        {
            sheet.hunger = 100;
            return true;
        }
        return false;
    }

    public bool Sleep(Instruction instruction, CharacterSheet sheet)
    {
        if (instruction.give.Length == 0 && instruction.gather.Length == 0)
        {
            sheet.energy = 100;
            return true;
        }
        return false;
    }
}

