using UnityEngine;
using System.Collections.Generic;

public class MultiCityTraderOracle
{
    bool debug = true;

    int allItemsAtLeast = 5;

    Logger logger;

    public List<Instruction> GetInstructions(CharacterSheet sheet)
    {
        this.logger = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().GetLogger();
        //First Go to Local Trade House
        //Find out what all it has
        //Look at list of other trade houses
        //See who needs what this house has
        //Take an item there
        //set sheet base city as the destination city



        List<Instruction> instructions = new List<Instruction>();

        Instruction goToTradeHouse = new Instruction();
        goToTradeHouse.destination = sheet.baseCity.TradeHouses[0].gameObject.GetComponent<NavigationWaypoint>();
        goToTradeHouse.building = sheet.baseCity.TradeHouses[0];
        goToTradeHouse.gather = new ItemType[] { };
        goToTradeHouse.give = new ItemType[] { };

        instructions.Add(goToTradeHouse);

        Dictionary<ItemType, int> currentCityHas = sheet.baseCity.TradeHouses[0].inventory.GetCountOfItems();

        Dictionary<TradeCity, Dictionary<ItemType, int>> otherCitiesHave = new Dictionary<TradeCity, Dictionary<ItemType, int>>();

        RegionOracle regionOracle = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().GetRegionOracle();
        foreach (TownOracle townOracle in regionOracle.townOracles)
        {
            otherCitiesHave.Add(townOracle.town.GetComponent<TradeCity>(), townOracle.town.GetComponent<TradeCity>().TradeHouses[0].inventory.GetCountOfItems());
        }

        TradeCity newBaseCity = sheet.baseCity;
        logger.Log(debug, "Starting multicitytrader foreach loop");
        foreach (ItemType item in currentCityHas.Keys)
        {
            logger.Log(debug, "Searing for item:(" + item + ") which has amount:(" + currentCityHas[item] +")");
            if (currentCityHas[item] > allItemsAtLeast)
            {
                logger.Log(debug, "Item:(" + item + ") can be traded");
                foreach (TradeCity town in otherCitiesHave.Keys)
                {
                    logger.Log(debug, "Searing for item:(" + item + ") in town (" + town.name + ")");
                    if (!otherCitiesHave[town].ContainsKey(item) || otherCitiesHave[town][item] <= allItemsAtLeast)
                    {
                        logger.Log(debug, "Item:(" + item + ") can be traded to town (" + town.name + ")");
                        Instruction getTradeItem = new Instruction();
                        goToTradeHouse.destination = sheet.baseCity.TradeHouses[0].gameObject.GetComponent<NavigationWaypoint>();
                        goToTradeHouse.building = sheet.baseCity.TradeHouses[0];
                        goToTradeHouse.gather = new ItemType[] { item };
                        goToTradeHouse.give = new ItemType[] { };

                        instructions.Add(goToTradeHouse);

                        Instruction depositTradeItem = new Instruction();
                        depositTradeItem.destination = town.TradeHouses[0].gameObject.GetComponent<NavigationWaypoint>();
                        depositTradeItem.building = town.TradeHouses[0];
                        depositTradeItem.gather = new ItemType[] { };
                        depositTradeItem.give = new ItemType[] { item };

                        instructions.Add(depositTradeItem);

                        newBaseCity = town;


                    }
                }
            }
        }

        sheet.baseCity = newBaseCity;

        return instructions;
    }
}
