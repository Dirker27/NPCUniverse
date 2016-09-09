using UnityEngine;
using System.Collections.Generic;

public class FoundryOracle
{
    public List<Instruction> GetInstructions(TradeCity currentCity)
    {
        List<Instruction> instructions = new List<Instruction>();

        Instruction getOre = new Instruction();
        getOre.destination = currentCity.OreShops[0].gameObject.GetComponent<NavigationWaypoint>();
        getOre.building = currentCity.OreShops[0];
        getOre.gather = new ItemType[] { ItemType.ORE };
        getOre.give = new ItemType[] { };
        getOre.fun1 = new instructionFunction((getOre.building).GetItem);

        instructions.Add(getOre);

        Instruction makeBar = new Instruction();
        makeBar.destination = currentCity.Foundries[0].gameObject.GetComponent<NavigationWaypoint>();
        makeBar.building = currentCity.Foundries[0];
        makeBar.gather = new ItemType[] { ItemType.BAR };
        makeBar.give = new ItemType[] { ItemType.ORE };
        makeBar.recipe = MasterRecipe.Instance.Bar;
        makeBar.fun1 = new instructionFunction((makeBar.building).MakeRecipe);

        instructions.Add(makeBar);

        Instruction storeBar = new Instruction();
        storeBar.destination = currentCity.Foundries[0].gameObject.GetComponent<NavigationWaypoint>();
        storeBar.building = currentCity.Foundries[0];
        storeBar.gather = new ItemType[] { };
        storeBar.give = new ItemType[] { ItemType.BAR };
        storeBar.fun1 = new instructionFunction((storeBar.building).StoreItem);

        instructions.Add(storeBar);

        return instructions;
    }
}
