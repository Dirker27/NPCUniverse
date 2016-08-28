using UnityEngine;
using System.Collections.Generic;

public class FarmOracle : MonoBehaviour
{
    private bool debug = false;

    private bool Wheat = true;
    private bool Barley = false;

    void Log(string s)
    {
        if (debug)
        {
            Debug.Log("FarmOracle log <" + s + ">");
        }
    }
    public Farm WhereShouldIFarm(TradeCity currentCity)
    {
        return currentCity.Farms[0];
    }

    public Barn WhereShouldIBarn(TradeCity currentCity)
    {
        return currentCity.Barns[0];
    }

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
        getCrop.fun1 = new instructionFunction(((Farm)getCrop.building).GetCrop);


        Instruction storeCrop = new Instruction();
        storeCrop.destination = currentCity.Barns[0].gameObject.GetComponent<NavigationWaypoint>();
        storeCrop.building = currentCity.Barns[0];
        storeCrop.gather = new ItemType[] { };
        storeCrop.fun1 = new instructionFunction(((Barn)storeCrop.building).StoreCrop);

        if (Wheat)
        {

            getCrop.gather = new ItemType[] { ItemType.WHEAT };
            storeCrop.give = new ItemType[] { ItemType.WHEAT };
        }
        else if (Barley)
        {

            getCrop.gather = new ItemType[] { ItemType.BARLEY };
            storeCrop.give = new ItemType[] { ItemType.BARLEY };
        }

        instructions.Add(getCrop);


        instructions.Add(storeCrop);

        return instructions;
    }
}
