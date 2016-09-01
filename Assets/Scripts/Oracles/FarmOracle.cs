using UnityEngine;
using System.Collections.Generic;

public class FarmOracle : MonoBehaviour
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

    public List<Instruction> GetInstructions(TradeCity currentCity)
    {
        List<Instruction> instructions = new List<Instruction>();

        Instruction getCrop = new Instruction();
        getCrop.destination = currentCity.Farms[0].gameObject.GetComponent<NavigationWaypoint>();
        getCrop.building = currentCity.Farms[0];
        getCrop.give = new ItemType[] { };
        getCrop.fun1 = new instructionFunction((getCrop.building).MakeRecipe);


        Instruction storeCrop = new Instruction();
        storeCrop.destination = currentCity.Barns[0].gameObject.GetComponent<NavigationWaypoint>();
        storeCrop.building = currentCity.Barns[0];
        storeCrop.gather = new ItemType[] { };
        storeCrop.fun1 = new instructionFunction((storeCrop.building).StoreItem);

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
