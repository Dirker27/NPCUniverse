using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class Instruction
{
    public NavigationWaypoint destination;
    public BaseBuilding building;
    public ItemType[] gather;
    public ItemType[] give;
    public string Action;
}