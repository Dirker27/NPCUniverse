/**
 * Class:TradeHouse
 * Purpose:Provides storage for item of trade and requst them
 * 
 * public fields:
 *  
 * public methods:
 *  void Start(): Sets the debug value for the TradeHouse
 * 
 * @author: NvS 
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
public class TradeHouse : BaseBuilding
{

    public override void Start()
    {
        base.Start();
        this.debug = false;

        canHold = new List<ItemType>(Enum.GetValues(typeof(ItemType)).Cast<ItemType>().ToList());

        CurrentPositions.Add(Jobs.TRADERUNNER, 1);
        TotalPositions.Add(Jobs.TRADERUNNER, 1);

        CurrentPositions.Add(Jobs.MULTICITYTRADER, 1);
        TotalPositions.Add(Jobs.MULTICITYTRADER, 1);

        Register();
    }


}

