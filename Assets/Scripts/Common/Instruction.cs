using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public delegate bool instructionFunction(Instruction instruction, CharacterSheet sheet);

public class Instruction
{
    public NavigationWaypoint destination;
    public BaseBuilding building;
    public ItemType[] gather;
    public ItemType[] give;
    public instructionFunction fun1;

}