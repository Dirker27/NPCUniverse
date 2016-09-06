/**
 * Class:Tavern
 * Purpose:Provides the functionality of a Tavern for an InnKeeper
 * 
 * Supports the Meal recipie
 * Can Store Meals
 * 
 * public methods:
 *  void Start(): 
 *  void Eat():
 *  void Sleep():
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

        canHold = new List<ItemType> { ItemType.MEAL };

        supportedRecipes.Add(MasterRecipe.Instance.Meal);
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

