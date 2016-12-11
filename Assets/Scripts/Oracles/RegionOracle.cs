using UnityEngine;
using System.Collections.Generic;
using System;

public class RegionOracle 
{
    int heightY = 1;
    int startinX = 0;
    int startingZ = 0;
    int rangeHeight = 100;
    int rangeWidth = 100;

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
        Vector3 position = new Vector3(); ;
        bool foundNewPosition = false;
        while (!foundNewPosition)
        {
            for (float x = startinX + rangeWidth; x >= startinX - rangeWidth; x = x - 100)
            {
                for (float z = startingZ; z >= startingZ - rangeHeight; z = z - 100)
                {
                    position = new Vector3(x, heightY, z);
                    if (!townCoordinates.Contains(position))
                    {
                        foundNewPosition = true;
                        break;
                    }
                }
                if (foundNewPosition)
                {
                    break;
                }
            }
            if (foundNewPosition)
            {
                TownOracle newTown = new TownOracle(position);
                newTown.BuildBasicBuildings();
                townOracles.Add(newTown);
                townCoordinates.Add(position);
            }
            else
            {
                rangeWidth += 100;
                rangeHeight += 100;
            }
        }
    }

    public void UpdateTowns()
    {
        bool allTownsFull = true;
        foreach (TownOracle town in townOracles)
        {
            town.SpawnCharacter();
            allTownsFull &= town.townFull;
        }
        if (allTownsFull)
        {
            NewTown();
        }
    }
}
