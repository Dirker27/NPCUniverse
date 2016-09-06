using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class Recipe
{
    public Dictionary<ItemType, int> required;
    public Dictionary<ItemType, int> produces;

    public bool CompleteRecipe(CharacterSheet sheet)
    {
        bool canFufill = true;
        Dictionary<ItemType, int> needed = new Dictionary<ItemType,int>(required);
        List<Item> toRemove = new List<Item>();
        foreach (Item item in sheet.inventory.items)
        {
            if (needed.ContainsKey(item.Type) && needed[item.Type] > 0)
            {
                needed[item.Type]--;
                toRemove.Add(item);
            }
        }

        foreach (ItemType key in needed.Keys)
        {
            if (needed[key] != 0)
            {
                canFufill = false;
            }
        }

        if (canFufill)
        {
            foreach (Item item in toRemove)
            {
                sheet.inventory.items.Remove(item);
            }
            
            foreach (ItemType toAdd in produces.Keys)
            {
                for(int i = 0; i < produces[toAdd]; i++)
                {
                    Item adding = new Item();
                    adding.Type = toAdd;
                    adding.PurchasedPrice = 0;
                    sheet.inventory.items.Add(adding);
                }
            }
        }

        return canFufill;
    }
}

public sealed class MasterRecipe
{
    private static readonly MasterRecipe instance = new MasterRecipe();
    public Recipe Bar = new Recipe();
    public Recipe Bread = new Recipe();
    public Recipe Armor = new Recipe();
    public Recipe Flour = new Recipe();
    public Recipe StoneBlock = new Recipe();
    public Recipe Meat = new Recipe();
    public Recipe Leather = new Recipe();
    public Recipe Log = new Recipe();
    public Recipe Wheat = new Recipe();
    public Recipe Barley = new Recipe();
    public Recipe Charcoal = new Recipe();
    public Recipe Beer = new Recipe();
    public Recipe Bow = new Recipe();
    public Recipe FireWood = new Recipe();
    public Recipe Meal = new Recipe();
    public Recipe Weapon = new Recipe();
    public Recipe Tool = new Recipe();
    public Recipe LumberPlank = new Recipe();
    public Recipe Fish = new Recipe();
    public Recipe Ore = new Recipe();
    public Recipe Arrow = new Recipe();
    public Recipe Stone = new Recipe();

    private MasterRecipe() 
    {
        // Bar recipe
        Bar.required = new Dictionary<ItemType, int>();
        Bar.required.Add(ItemType.ORE, 1);
        Bar.produces = new Dictionary<ItemType, int>();
        Bar.produces.Add(ItemType.BAR, 1);

        // Bread recipe
        Bread.required = new Dictionary<ItemType, int>();
        Bread.required.Add(ItemType.FLOUR, 1);
        Bread.produces = new Dictionary<ItemType, int>();
        Bread.produces.Add(ItemType.BREAD, 1);

        // Armor recipe
        Armor.required = new Dictionary<ItemType, int>();
        Armor.required.Add(ItemType.BAR, 1);
        Armor.produces = new Dictionary<ItemType, int>();
        Armor.produces.Add(ItemType.ARMOR, 1);

        // Flour recipe
        Flour.required = new Dictionary<ItemType, int>();
        Flour.required.Add(ItemType.WHEAT, 1);
        Flour.produces = new Dictionary<ItemType, int>();
        Flour.produces.Add(ItemType.FLOUR, 1);

        // StoneBlock recipe
        StoneBlock.required = new Dictionary<ItemType, int>();
        StoneBlock.required.Add(ItemType.STONE, 1);
        StoneBlock.produces = new Dictionary<ItemType, int>();
        StoneBlock.produces.Add(ItemType.STONEBLOCK, 1);

        // Meat recipe
        Meat.required = new Dictionary<ItemType, int>();
        Meat.required.Add(ItemType.BOW, 1);
        Meat.required.Add(ItemType.ARROW, 1);
        Meat.produces = new Dictionary<ItemType, int>();
        Meat.produces.Add(ItemType.MEAT, 1);

        // Leather recipe
        Leather.required = new Dictionary<ItemType, int>();
        Leather.required.Add(ItemType.BOW, 1);
        Leather.required.Add(ItemType.ARROW, 1);
        Leather.produces = new Dictionary<ItemType, int>();
        Leather.produces.Add(ItemType.LEATHER, 1);

        // Log recipe
        Log.required = new Dictionary<ItemType, int>();
        Log.produces = new Dictionary<ItemType, int>();
        Log.produces.Add(ItemType.LOG, 1);

        // Wheat recipe
        Wheat.required = new Dictionary<ItemType, int>();
        Wheat.produces = new Dictionary<ItemType, int>();
        Wheat.produces.Add(ItemType.WHEAT, 1);

        // Barley recipe
        Barley.required = new Dictionary<ItemType, int>();
        Barley.produces = new Dictionary<ItemType, int>();
        Barley.produces.Add(ItemType.BARLEY, 1);

        // Charcoal recipe
        Charcoal.required = new Dictionary<ItemType, int>();
        Charcoal.required.Add(ItemType.FIREWOOD, 1);
        Charcoal.produces = new Dictionary<ItemType, int>();
        Charcoal.produces.Add(ItemType.CHARCOAL, 1);

        // Beer recipe
        Beer.required = new Dictionary<ItemType, int>();
        Beer.required.Add(ItemType.BARLEY, 1);
        Beer.produces = new Dictionary<ItemType, int>();
        Beer.produces.Add(ItemType.BEER, 1);

        // Bow recipe
        Bow.required = new Dictionary<ItemType, int>();
        Bow.required.Add(ItemType.LOG, 1);
        Bow.produces = new Dictionary<ItemType, int>();
        Bow.produces.Add(ItemType.BOW, 1);

        // Firewood recipe
        FireWood.required = new Dictionary<ItemType, int>();
        FireWood.required.Add(ItemType.LOG, 1);
        FireWood.produces = new Dictionary<ItemType, int>();
        FireWood.produces.Add(ItemType.FIREWOOD, 1);

        // Meal recipe
        Meal.required = new Dictionary<ItemType, int>();
        Meal.required.Add(ItemType.BREAD, 1);
        Meal.required.Add(ItemType.FISH, 1);
        Meal.required.Add(ItemType.BEER, 1);
        Meal.produces = new Dictionary<ItemType, int>();
        Meal.produces.Add(ItemType.MEAL, 1);

        // Weapon recipe
        Weapon.required = new Dictionary<ItemType, int>();
        Weapon.required.Add(ItemType.BAR, 1);
        Weapon.produces = new Dictionary<ItemType, int>();
        Weapon.produces.Add(ItemType.WEAPON, 1);

        // Tool recipe
        Tool.required = new Dictionary<ItemType, int>();
        Tool.required.Add(ItemType.BAR, 1);
        Tool.produces = new Dictionary<ItemType, int>();
        Tool.produces.Add(ItemType.TOOL, 1);

        // LumberPlank recipe
        LumberPlank.required = new Dictionary<ItemType, int>();
        LumberPlank.required.Add(ItemType.LOG, 1);
        LumberPlank.produces = new Dictionary<ItemType, int>();
        LumberPlank.produces.Add(ItemType.LUMBERPLANK, 1);

        // Fish recipe
        Fish.required = new Dictionary<ItemType, int>();
        Fish.produces = new Dictionary<ItemType, int>();
        Fish.produces.Add(ItemType.FISH, 1);

        // Ore recipe
        Ore.required = new Dictionary<ItemType, int>();
        Ore.produces = new Dictionary<ItemType, int>();
        Ore.produces.Add(ItemType.ORE, 1);

        // Arrow recipe
        Arrow.required = new Dictionary<ItemType, int>();
        Arrow.required.Add(ItemType.LOG, 1);
        Arrow.produces = new Dictionary<ItemType, int>();
        Arrow.produces.Add(ItemType.ARROW, 1);

        // Ore recipe
        Stone.required = new Dictionary<ItemType, int>();
        Stone.produces = new Dictionary<ItemType, int>();
        Stone.produces.Add(ItemType.STONE, 1);
    }

    public static MasterRecipe Instance
    {
        get
        {
            return instance;
        }
    }

    
}
 