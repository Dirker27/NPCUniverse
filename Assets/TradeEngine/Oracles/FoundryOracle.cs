using UnityEngine;
using System.Collections.Generic;

public class FoundryOracle
{
    public List<Instruction> GetInstructions(CharacterSheet sheet)
    {
        List<Instruction> instructions = new List<Instruction>();

        Instruction getOre = new Instruction();
        getOre.destination = sheet.baseCity.OreShops[0].gameObject.GetComponent<NavigationWaypoint>();
        getOre.building = sheet.baseCity.OreShops[0];
        getOre.gather = new ItemType[] { ItemType.ORE };
        getOre.give = new ItemType[] { };
        getOre.fun1 = new instructionFunction((getOre.building).GetItem);

        instructions.Add(getOre);

        Instruction makeBar = new Instruction();
        Foundry destination = null;
        foreach (Foundry foundry in sheet.baseCity.Foundries)
        {
            if (foundry.workers.Contains(sheet))
            {
                destination = foundry;
                break;
            }
        }

        if (destination == null)
        {
            foreach (Foundry foundry in sheet.baseCity.Foundries)
            {
                if (foundry.CurrentPositions[Jobs.SMITH] > 0)
                {
                    destination = foundry;
                    foundry.workers.Add(sheet);
                    foundry.CurrentPositions[Jobs.SMITH]--;
                    break;
                }
            }
        }
        makeBar.destination = destination.gameObject.GetComponent<NavigationWaypoint>();
        makeBar.building = destination;
        makeBar.gather = new ItemType[] { ItemType.BAR };
        makeBar.give = new ItemType[] { ItemType.ORE };
        makeBar.recipe = MasterRecipe.Instance.Bar;
        makeBar.fun1 = new instructionFunction((makeBar.building).MakeRecipe);

        instructions.Add(makeBar);

        Instruction storeBar = new Instruction();
        storeBar.destination = destination.gameObject.GetComponent<NavigationWaypoint>();
        storeBar.building = destination;
        storeBar.gather = new ItemType[] { };
        storeBar.give = new ItemType[] { ItemType.BAR };
        storeBar.fun1 = new instructionFunction((storeBar.building).StoreItem);
        storeBar.fun2 = new instructionFunction2((destination).ReleaseJob);

        instructions.Add(storeBar);

        return instructions;
    }
}
