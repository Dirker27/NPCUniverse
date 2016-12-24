using UnityEngine;
using System.Collections.Generic;
using System;

public class RegionOracle 
{

    List<List<Type>> RegionTowns = new List<List<Type>> { TownTypes.Types.TOWN_EVERYBUILDING,
                                                          TownTypes.Types.TOWN_FARMINGTOWN,
                                                          TownTypes.Types.TOWN_FORESTTOWN,
                                                          TownTypes.Types.TOWN_HUNTINGTOWN,
                                                          TownTypes.Types.TOWN_MINNINGTOWN
                                                        };
    int heightY = 1;
    int startinX = 0;
    int startingZ = 0;
    int rangeHeight = 100;
    int rangeWidth = 100;

    List<Vector3> townCoordinates;
    int townNumber = 0;
    int nextToBuild = 0;
    int maxTowns;

    List<TownOracle> townOracles;

    public RegionOracle()
    {
        maxTowns = RegionTowns.Count;
        townCoordinates = new List<Vector3>();
        townOracles = new List<TownOracle>();
    }

    public void NewTown(List<Type> townBuildings)
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
                TownOracle newTown = new TownOracle("Town " + townNumber, position, townBuildings, TownTypes.Types.TOWN_BASEBUILDING);
                newTown.BuildBasicBuildings();
                townOracles.Add(newTown);
                townCoordinates.Add(position);
                townNumber++;
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
            BuildNextTown();
        }
    }

    public void BuildNextTown()
    {
        if (nextToBuild != -1)
        {
            NewTown(RegionTowns[nextToBuild]);
            nextToBuild++;
            if (nextToBuild >= maxTowns)
            {
                nextToBuild = -1;
            }
        }
    }
}
