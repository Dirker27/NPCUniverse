/**
 * Class:GuildHall
 * Purpose:Provides the functionality of a GuildHall for a Quatermaster.
 * 
 * Can Store Armor and Weapons
 * 
 * public fields:
 *  
 * public methods:
 *  void Start(): 
 * 
 * @author: NvS 
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GuildHall : BaseBuilding
{
    public override void Start()
    {
        base.Start();
        this.debug = false;

        canHold = new List<ItemType> { ItemType.ARMOR, ItemType.WEAPON };
        CurrentPositions.Add(Jobs.QUATERMASTER, 1);
        TotalPositions.Add(Jobs.QUATERMASTER, 1);
        Register();
    }

}

