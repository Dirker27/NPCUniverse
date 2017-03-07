using UnityEngine;
using System.Collections.Generic;

public class FishermanOracle
{
    public List<Instruction> GetInstructions(CharacterSheet sheet)
    {
        List<Instruction> instructions = new List<Instruction>();

        Instruction getFish = new Instruction();
        Pond destination = null;
        foreach(Pond pond in sheet.baseCity.Ponds)
        {
            if (pond.workers.Contains(sheet))
            {
                destination = pond;
                break;
            }
        }

        if (destination == null)
        {
            foreach (Pond pond in sheet.baseCity.Ponds)
            {
                if (pond.CurrentPositions[Jobs.FISHERMAN] > 0)
                {
                    destination = pond;
                    pond.workers.Add(sheet);
                    pond.CurrentPositions[Jobs.FISHERMAN]--;
                    break;
                }
            }
        }

        getFish.destination = destination.gameObject.GetComponent<NavigationWaypoint>();
        getFish.building = destination;

        getFish.gather = new ItemType[] { ItemType.FISH };
        getFish.give = new ItemType[] { };
        getFish.recipe = MasterRecipe.Instance.Fish;
        getFish.fun1 = new instructionFunction((getFish.building).MakeRecipe);

        instructions.Add(getFish);

        Instruction storeFish = new Instruction();
        storeFish.destination = sheet.baseCity.Barns[0].gameObject.GetComponent<NavigationWaypoint>();
        storeFish.building = sheet.baseCity.Barns[0];
        storeFish.gather = new ItemType[] { };
        storeFish.give = new ItemType[] { ItemType.FISH };
        storeFish.fun1 = new instructionFunction((storeFish.building).StoreItem);
        storeFish.fun2 = new instructionFunction2((getFish.building).ReleaseJob);
        instructions.Add(storeFish);

        return instructions;
    }
}
