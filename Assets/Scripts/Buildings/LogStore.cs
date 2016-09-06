/**
 * Class:LogStore
 * Purpose:Provides the functionality of a LogStore for a forester.
 * 
 * Can Store Log
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

public class LogStore : BaseBuilding
{

    public override void Start()
    {
        base.Start();
        this.debug = false;

        canHold = new List<ItemType> { ItemType.LOG };
    }
}

