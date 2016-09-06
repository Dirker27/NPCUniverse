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
    public bool debug = false;

    public List<Recipe> supportedRecipes;
    public List<ItemType> canHold;

    public virtual void Start()
    {
        this.logger = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Logger>();
        this.inventory = GetComponent<Inventory>();
        this.inventory.items = new List<Item>();
        this.supportedRecipes = new List<Recipe>();
        this.canHold = new List<ItemType>();
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
        return result;
    }

    public bool MakeRecipe(Instruction instruction, CharacterSheet sheet)
    {
        return instruction.recipe.CompleteRecipe(sheet);
    }
}

