using UnityEngine;
using System.Collections.Generic;

public class FarmOracle
{
    private bool Wheat = true;
    private bool Barley = false;

    public ItemType WhatShouldIFarm()
    {
        if (Wheat)
        {
            Wheat = false;
            Barley = true;
            return ItemType.WHEAT;
        }
        else if (Barley)
        {
            Barley = false;
            Wheat = true;
            return ItemType.BARLEY;
        }
        return ItemType.INVALID;
    }

    public List<Instruction> GetInstructions(CharacterSheet sheet)
    {
        List<Instruction> instructions = new List<Instruction>();

        Instruction getCrop = new Instruction();
        Farm destination = null;
        foreach (Farm farm in sheet.baseCity.Farms)
        {
            if (farm.workers.Contains(sheet))
            {
                destination = farm;
                break;
            }
        }

        if (destination == null)
        {
            foreach (Farm farm in sheet.baseCity.Farms)
            {
                if (farm.CurrentPositions[Jobs.FARMER] > 0)
                {
                    destination = farm;
                    farm.workers.Add(sheet);
                    farm.CurrentPositions[Jobs.FARMER]--;
                    break;
                }
            }
        }
        getCrop.destination = destination.gameObject.GetComponent<NavigationWaypoint>();
        getCrop.building = destination;
        getCrop.give = new ItemType[] { };
        getCrop.fun1 = new instructionFunction((getCrop.building).MakeRecipe);


        Instruction storeCrop = new Instruction();
        storeCrop.destination = sheet.baseCity.Barns[0].gameObject.GetComponent<NavigationWaypoint>();
        storeCrop.building = sheet.baseCity.Barns[0];
        storeCrop.gather = new ItemType[] { };
        storeCrop.fun1 = new instructionFunction((storeCrop.building).StoreItem);
        storeCrop.fun2 = new instructionFunction2((destination).ReleaseJob);

        if (Wheat)
        {

            getCrop.gather = new ItemType[] { ItemType.WHEAT };
            storeCrop.give = new ItemType[] { ItemType.WHEAT };
            getCrop.recipe = MasterRecipe.Instance.Wheat;
            Wheat = false;
            Barley = true;
        }
        else if (Barley)
        {

            getCrop.gather = new ItemType[] { ItemType.BARLEY };
            storeCrop.give = new ItemType[] { ItemType.BARLEY };
            getCrop.recipe = MasterRecipe.Instance.Barley;
            Barley = false;
            Wheat = true;
        }

        instructions.Add(getCrop);


        instructions.Add(storeCrop);

        return instructions;
    }
}
