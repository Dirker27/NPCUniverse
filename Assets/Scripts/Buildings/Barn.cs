/**
 * Class:Barn
 * Purpose:Provides storage for item
 * 
 * public fields:
 *  
 * public methods:
 *  void Start(): Sets the debug value for the barn
 * 
 * @author: NvS 
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Barn : BaseBuilding
{

    public override void Start()
    {
        base.Start();
        this.debug = false;
    }
}

