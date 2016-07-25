﻿/**
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

    public void Start()
    {
        base.Start();
        this.debug = true;
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
}

