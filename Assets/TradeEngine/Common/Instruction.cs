using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public delegate bool instructionFunction(Instruction instruction, CharacterSheet sheet);
public delegate void instructionFunction2();

public class Instruction
{
    public NavigationWaypoint destination;
    public BaseBuilding building;
    public ItemType[] gather;
    public ItemType[] give;
    public Recipe recipe;
    public instructionFunction fun1;
    public instructionFunction2 fun2;

}