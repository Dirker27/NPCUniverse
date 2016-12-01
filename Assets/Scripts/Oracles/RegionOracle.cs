using UnityEngine;
using System.Collections.Generic;
using System;

public class RegionOracle 
{
    int heightY = 1;
    int startinX = 0;
    int startingZ = 0;
    List<Vector3> townCoordinates;
    int townNumber = 0;
    int nextToBuild = 0;

    List<TownOracle> townOracles;

    public RegionOracle()
    {
        townCoordinates = new List<Vector3>();
        townOracles = new List<TownOracle>();
    }

    public void NewTown()
    {
        TownOracle newTown = new TownOracle(new Vector3(0,1,0));
        newTown.BuildBasicBuildings();
        townOracles.Add(newTown);
    }

    public void UpdateTowns()
    {
        foreach (TownOracle town in townOracles)
        {
            town.SpawnCharacter();
        }
    }
}
