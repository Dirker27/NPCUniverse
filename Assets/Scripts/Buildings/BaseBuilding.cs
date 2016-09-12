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

    public virtual void Start()
    {
        this.logger = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().GetLogger();
        this.inventory = new Inventory();
        this.inventory.items = new List<Item>();
        this.supportedRecipes = new List<Recipe>();
        this.canHold = new List<ItemType>();
        Register();
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
            oracle.AddJobs(Jobs.BAKER, 1);
        }
        else if (buildingType == typeof(Barn))
        {
            city.Barns.Add((Barn)this);
        }
        else if (buildingType == typeof(BowShop))
        {
            city.BowShops.Add((BowShop)this);
            oracle.AddJobs(Jobs.FLETCHER, 1);
        }
        else if (buildingType == typeof(Brewhouse))
        {
            city.Brewhouses.Add((Brewhouse)this);
            oracle.AddJobs(Jobs.BREWMASTER, 1);
        }
        else if (buildingType == typeof(CharcoalPit))
        {
            city.CharcoalPits.Add((CharcoalPit)this);
            oracle.AddJobs(Jobs.COLLIER, 1);
        }
        else if (buildingType == typeof(Farm))
        {
            city.Farms.Add((Farm)this);
            oracle.AddJobs(Jobs.FARMER, 2);
        }
        else if (buildingType == typeof(Forest))
        {
            city.Forests.Add((Forest)this);
            oracle.AddJobs(Jobs.FORESTER, 1);
        }
        else if (buildingType == typeof(Foundry))
        {
            city.Foundries.Add((Foundry)this);
            oracle.AddJobs(Jobs.SMITH, 1);
        }
        else if (buildingType == typeof(GuildHall))
        {
            city.GuildHalls.Add((GuildHall)this);
            oracle.AddJobs(Jobs.QUATERMASTER, 1);
        }
        else if (buildingType == typeof(HuntingLodge))
        {
            city.HuntingLodges.Add((HuntingLodge)this);
            oracle.AddJobs(Jobs.HUNTER, 1);
        }
        else if (buildingType == typeof(LogStore))
        {
            city.LogStores.Add((LogStore)this);
        }
        else if (buildingType == typeof(Masonry))
        {
            city.Masonries.Add((Masonry)this);
            oracle.AddJobs(Jobs.STONECUTTER, 1);
        }
        else if (buildingType == typeof(Mill))
        {
            city.Mills.Add((Mill)this);
            oracle.AddJobs(Jobs.MILLER, 1);
        }
        else if (buildingType == typeof(Mine))
        {
            city.Mines.Add((Mine)this);
            oracle.AddJobs(Jobs.MINER, 2);
        }
        else if (buildingType == typeof(OreShop))
        {
            city.OreShops.Add((OreShop)this);
        }
        else if (buildingType == typeof(Pond))
        {
            city.Ponds.Add((Pond)this);
            oracle.AddJobs(Jobs.FISHERMAN, 1);
        }
        else if (buildingType == typeof(SawHouse))
        {
            city.SawHouses.Add((SawHouse)this);
            oracle.AddJobs(Jobs.SAWWORKER, 1);
        }
        else if (buildingType == typeof(Smithy))
        {
            city.Smithies.Add((Smithy)this);
            oracle.AddJobs(Jobs.TOOLSMITH, 1);
            oracle.AddJobs(Jobs.ARMORSMITH, 1);
            oracle.AddJobs(Jobs.WEAPONSMITH, 1);
        }
        else if (buildingType == typeof(Tavern))
        {
            city.Taverns.Add((Tavern)this);
            oracle.AddJobs(Jobs.INNKEEPER, 1);
        }
        else if (buildingType == typeof(WoodCut))
        {
            city.WoodCuts.Add((WoodCut)this);
            oracle.AddJobs(Jobs.WOODCUTER, 1);
        }
    }

}

