using UnityEngine;
using System.Collections.Generic;
using System;

public class TownOracle 
{
    int heightY = 1;
    int startinX = 10;
    int startingZ = 10;
    List<Vector3> buildingCoordinates;
    int buildingNumber = 0;
    int pillNumber = 0;
    int nextToBuild = 0;
    List<Type> baseBuildings = new List<Type> { typeof(Barn), typeof(LogStore), typeof(OreShop) };
    List<Type> buildingOrder = new List<Type> { typeof(Forest), typeof(Farm), typeof(Pond), typeof(Mine),
                                                typeof(WoodCut), typeof(Mill), typeof(Brewhouse), typeof(Foundry), typeof(BowShop), typeof(SawHouse), typeof(Masonry),
                                                typeof(CharcoalPit), typeof(Smithy), typeof(Bakery), typeof(HuntingLodge),
                                                typeof(GuildHall), typeof(Tavern),
                                              };
    int maxBuilding;
    public TownOracle()
    {
        buildingCoordinates = new List<Vector3>();
        buildingCoordinates.Add(new Vector3(0, heightY, 0));

        maxBuilding = buildingOrder.Count;
    }

    public void SpawnCharacter()
    {
        GameObject myCube;
        myCube = GameObject.CreatePrimitive(PrimitiveType.Capsule);

        myCube.transform.position = new Vector3(0, heightY, 0);

        myCube.AddComponent<NPCJobDriver>();
        myCube.AddComponent<NavigationWaypoint>();
        myCube.AddComponent<CharacterMovement>();
        myCube.name = "Pill person:" + pillNumber;
        myCube.GetComponent<CharacterMovement>().travelRate = 10;
        myCube.GetComponent<NPCJobDriver>().Start();
        myCube.GetComponent<NPCJobDriver>().sheet.name = myCube.name;
        myCube.GetComponent<NPCJobDriver>().sheet.Save();
        pillNumber++;
    }

    public void BuildBasicBuildings()
    {
        foreach (Type building in baseBuildings)
        {
            BuildNewBuilding(building);
        }
    }

    public void BuildNextBuilding()
    {
        BuildNewBuilding(buildingOrder[nextToBuild]);
        nextToBuild++;
        if(nextToBuild >= maxBuilding)
        {
            nextToBuild = 0;
        }
    }

    public void BuildNewBuilding(Type building)
    {
        Vector3 position = new Vector3(); ;
        bool foundNewPosition = false;
        while (!foundNewPosition)
        {
            for (int x = startinX; x >= -(startinX); x = x - 10)
            {
                for (int z = startingZ; z >= -(startingZ); z = z - 10)
                {
                    position = new Vector3(x, heightY, z);
                    if (!buildingCoordinates.Contains(position))
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
                GameObject myCube;
                myCube = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                myCube.AddComponent(building);
                myCube.AddComponent<NavigationWaypoint>();
                myCube.name = building.ToString() + " " + buildingNumber;
                myCube.GetComponent<BaseBuilding>().name = myCube.name;
                myCube.transform.position = position;
                myCube.GetComponent<BaseBuilding>().Save();
                buildingCoordinates.Add(position);
                buildingNumber++;
            }
            else
            {

                startinX += 10;
                startingZ += 10;
            }
        }
    }
}
