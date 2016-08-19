﻿/**
 * Class:Foundry
 * Purpose:Provides the functionality of a Foundry for a Smith. Creates bars.
 * 
 * public fields:
 *  
 * public methods:
 *  void Start(): 
 *  ItemType WorkFoundry(Item): Takes one ore and returns one bar
 * 
 * @author: NvS 
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Foundry : BaseBuilding
{

    public override void Start()
    {
        base.Start();
        this.debug = false;
    }

    public ItemType WorkFoundry(Item input)
    {

        ItemType produces = ItemType.INVALID;
        if (input.Type == ItemType.ORE)
        {
            produces = ItemType.BAR;
        }
        return produces;
    }

    public bool GetBar(Instruction instruction)
    {
        if (instruction.give.Length ==0 && instruction.gather[0] == ItemType.BAR)
        {
            return true;
        }
        return false;
    }

    public override bool DoAction(Instruction instruction, CharacterSheet sheet)
    {
        bool toReturn = false;
        switch (instruction.Action)
        {
            case "GetBar":
                toReturn = GetBar(instruction);
                break;

            default:
                // log that a miss match instruction has arrived
                toReturn = false;
                break;
        }
        return toReturn;
    }
        
}

