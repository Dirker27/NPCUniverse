/**
 * Class:GuildHall
 * Purpose:Provides the functionality of a GuildHall for a Quatermaster.
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
    }

}

