using UnityEngine;
using System.Collections.Generic;

public class MillOracle
{
    public List<Instruction> GetInstructions(CharacterSheet sheet)
    {
        List<Instruction> instructions = new List<Instruction>();

        Instruction getWheat = new Instruction();
        getWheat.destination = sheet.baseCity.Barns[0].gameObject.GetComponent<NavigationWaypoint>();
        getWheat.building = sheet.baseCity.Barns[0];
        getWheat.gather = new ItemType[] { ItemType.WHEAT };
        getWheat.give = new ItemType[] { };
        getWheat.fun1 = new instructionFunction((getWheat.building).GetItem);

        instructions.Add(getWheat);

        Instruction makeFlour = new Instruction();
        Mill destination = null;
        foreach (Mill mill in sheet.baseCity.Mills)
        {
            if (mill.workers.Contains(sheet))
            {
                destination = mill;
                break;
            }
        }

        if (destination == null)
        {
            foreach (Mill mill in sheet.baseCity.Mills)
            {
                if (mill.CurrentPositions[Jobs.MILLER] > 0)
                {
                    destination = mill;
                    mill.workers.Add(sheet);
                    mill.CurrentPositions[Jobs.MILLER]--;
                    break;
                }
            }
        }
        makeFlour.destination = destination.gameObject.GetComponent<NavigationWaypoint>();
        makeFlour.building = destination;
        makeFlour.gather = new ItemType[] { ItemType.FLOUR };
        makeFlour.give = new ItemType[] { ItemType.WHEAT };
        makeFlour.recipe = MasterRecipe.Instance.Armor;
        makeFlour.fun1 = new instructionFunction((makeFlour.building).MakeRecipe);

        instructions.Add(makeFlour);

        Instruction storeFlour = new Instruction();
        storeFlour.destination = destination.gameObject.GetComponent<NavigationWaypoint>();
        storeFlour.building = destination;
        storeFlour.gather = new ItemType[] { };
        storeFlour.give = new ItemType[] { ItemType.FLOUR };
        storeFlour.fun1 = new instructionFunction((storeFlour.building).StoreItem);
        storeFlour.fun2 = new instructionFunction2((destination).ReleaseJob);
        instructions.Add(storeFlour);

        return instructions;
    }
}
