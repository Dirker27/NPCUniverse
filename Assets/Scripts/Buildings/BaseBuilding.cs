/**
 * Class:BaseBuilding
 * Purpose: To define the methods and fields of buildings in such a way that adding new buildings is easy
 * as is adding functionality.
 * 
 * public fields:
 *  Inventory inventory: The collection of items the building has stored in its inventory.
 *  Logger logger: A reference to the logging class for logging information.
 *  bool debug: A bool determining whether or not the debug logs should be shown.
 *  
 * public methods:
 *  void Start(): Derived from MonoBehavior; Gets the logger and inventory set up.
 *  void Deposit (Item): Adds Item to building inventory
 *  void Withdraw (Item): Removes Item from building inventory
 *  Inventory PeekContents(): Returns a copy of the buildings inventory for inspection.
 * 
 * @author: NvS
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BaseBuilding : MonoBehaviour
{
    public Inventory inventory;

    public Logger logger;
    public bool debug = true;

    public List<Recipe> supportedRecipes;
    public List<ItemType> canHold;

    public Dictionary<Jobs, int> CurrentPositions;
    public Dictionary<Jobs, int> TotalPositions;
    public List<CharacterSheet> workers;

    public virtual void Start()
    {
        this.logger = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().GetLogger();
        this.inventory = new Inventory();
        this.inventory.items = new List<Item>();
        this.supportedRecipes = new List<Recipe>();
        this.canHold = new List<ItemType>();
        CurrentPositions = new Dictionary<Jobs, int>();
        TotalPositions = new Dictionary<Jobs, int>();
        workers = new List<CharacterSheet>();
    }

    public bool StoreItem(Instruction instruction, CharacterSheet sheet)
    {
        bool result = false;
        if (canHold.Contains(instruction.give[0]))
        {
            foreach (Item item in sheet.inventory.items)
            {
                if (item.Type == instruction.give[0])
                {
                    inventory.Add(item);
                    sheet.inventory.Remove(item);
                    result = true;
                    break;
                }
            }
        }
        else
        {
            logger.Log(debug, "Could not store item:" + instruction.give[0]);
        }
        return result;
    }

    public bool GetItem(Instruction instruction, CharacterSheet sheet)
    {
        bool result = false;
        if (canHold.Contains(instruction.gather[0]))
        {
            foreach (Item item in sheet.inventory.items)
            {
                if (item.Type == instruction.gather[0])
                {
                    sheet.inventory.Add(item);
                    inventory.Remove(item);
                    result = true;
                    break;
                }
            }
        }
        else
        {
            logger.Log(debug, "Could not gather item:" + instruction.gather[0]);
        }
        return result;
    }

    public bool MakeRecipe(Instruction instruction, CharacterSheet sheet)
    {
        bool result = instruction.recipe.CompleteRecipe(sheet);

        if (result == false)
        {
            logger.Log(debug, "Could not make recipie item");
        }
        return result;
    }

    public void ReleaseJob()
    {
        if (this.GetType() == typeof(Pond))
        {
            CurrentPositions[Jobs.FISHERMAN]++;
        }
    }

    public TradeCity GetClosestCity()
    {
        GameObject[] cities = GameObject.FindGameObjectsWithTag("TradeCity");
        GameObject closestsCity = null;
        foreach (GameObject city in cities)
        {
            if (closestsCity == null)
            {
                closestsCity = city;
            }
            if (Vector3.Distance(transform.position, city.transform.position) <= Vector3.Distance(transform.position, closestsCity.transform.position))
            {
                closestsCity = city;
            }
        }
        return closestsCity.GetComponent<TradeCity>();
    }

    public void Register()
    {
        TradeCity city = GetClosestCity();
        JobOracle oracle = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().GetJobOracle();
        System.Type buildingType = this.GetType();
        
        if (buildingType == typeof(Bakery))
        {
            city.Bakeries.Add((Bakery)this);
        }
        else if (buildingType == typeof(Barn))
        {
            city.Barns.Add((Barn)this);
        }
        else if (buildingType == typeof(BowShop))
        {
            city.BowShops.Add((BowShop)this);
        }
        else if (buildingType == typeof(Brewhouse))
        {
            city.Brewhouses.Add((Brewhouse)this);
        }
        else if (buildingType == typeof(CharcoalPit))
        {
            city.CharcoalPits.Add((CharcoalPit)this);
        }
        else if (buildingType == typeof(Farm))
        {
            city.Farms.Add((Farm)this);
        }
        else if (buildingType == typeof(Forest))
        {
            city.Forests.Add((Forest)this);
        }
        else if (buildingType == typeof(Foundry))
        {
            city.Foundries.Add((Foundry)this);
        }
        else if (buildingType == typeof(GuildHall))
        {
            city.GuildHalls.Add((GuildHall)this);
        }
        else if (buildingType == typeof(HuntingLodge))
        {
            city.HuntingLodges.Add((HuntingLodge)this);
        }
        else if (buildingType == typeof(LogStore))
        {
            city.LogStores.Add((LogStore)this);
        }
        else if (buildingType == typeof(Masonry))
        {
            city.Masonries.Add((Masonry)this);
        }
        else if (buildingType == typeof(Mill))
        {
            city.Mills.Add((Mill)this);
        }
        else if (buildingType == typeof(Mine))
        {
            city.Mines.Add((Mine)this);
        }
        else if (buildingType == typeof(OreShop))
        {
            city.OreShops.Add((OreShop)this);
        }
        else if (buildingType == typeof(Pond))
        {
            city.Ponds.Add((Pond)this);
        }
        else if (buildingType == typeof(SawHouse))
        {
            city.SawHouses.Add((SawHouse)this);
        }
        else if (buildingType == typeof(Smithy))
        {
            city.Smithies.Add((Smithy)this);
        }
        else if (buildingType == typeof(Tavern))
        {
            city.Taverns.Add((Tavern)this);
        }
        else if (buildingType == typeof(WoodCut))
        {
            city.WoodCuts.Add((WoodCut)this);
        }

        oracle.AddJobs(CurrentPositions, TotalPositions);
    }

}

