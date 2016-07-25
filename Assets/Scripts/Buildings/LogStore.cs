/**
 * Class:LogStore
 * Purpose:Provides the functionality of a LogStore for a forester.
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

    public void Start()
    {
        base.Start();
        this.debug = false;
    }
}

