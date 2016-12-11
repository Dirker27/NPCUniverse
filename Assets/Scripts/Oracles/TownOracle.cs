using UnityEngine;
using System.Collections.Generic;
using System;

public class TownOracle 
{
    float heightY;
    float startinX;
    float startingZ;
    float rangeHeight = 10;
    float rangeWidth = 10;
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
    public bool townFull = false;

    public TownOracle(Vector3 location)
    {
        heightY = location.y;
        startinX = location.x;
        startingZ = location.z;
        
        GameObject town = UnityEngine.Object.Instantiate(Resources.Load("City") as GameObject);
        town.name = "town 1";
        town.AddComponent<TradeCity>();
        town.GetComponent<TradeCity>().townOracle = this;
        town.tag = "TradeCity";
        town.transform.position = location;
        buildingCoordinates = new List<Vector3>();
        buildingCoordinates.Add(location);
        maxBuilding = buildingOrder.Count;
    }

    public void SpawnCharacter()
    {
        if (!townFull)
        {
            GameObject myCube;
            myCube = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            myCube.transform.position = new Vector3(startinX, heightY, startingZ);
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
            townFull = true;
        }
    }

    public void BuildNewBuilding(Type building)
    {
        Vector3 position = new Vector3(); ;
        bool foundNewPosition = false;
        while (!foundNewPosition)
        {
            for (float x = startinX + rangeWidth; x >= startinX - rangeWidth; x = x - 10)
            {
                for (float z = startingZ + rangeHeight; z >= startingZ - rangeHeight; z = z - 10)
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

                rangeWidth += 10;
                rangeHeight += 10;
            }
        }
    }
}
