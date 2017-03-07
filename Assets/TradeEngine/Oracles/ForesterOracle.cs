using UnityEngine;
using System.Collections.Generic;

public class ForesterOracle
{
    public List<Instruction> GetInstructions(CharacterSheet sheet)
    {
        List<Instruction> instructions = new List<Instruction>();

        Instruction getLog = new Instruction();
        Forest destination = null;
        foreach (Forest forest in sheet.baseCity.Forests)
        {
            if (forest.workers.Contains(sheet))
            {
                destination = forest;
                break;
            }
        }

        if (destination == null)
        {
            foreach (Forest forest in sheet.baseCity.Forests)
            {
                if (forest.CurrentPositions[Jobs.FORESTER] > 0)
                {
                    destination = forest;
                    forest.workers.Add(sheet);
                    forest.CurrentPositions[Jobs.FORESTER]--;
                    break;
                }
            }
        }
        getLog.destination = destination.gameObject.GetComponent<NavigationWaypoint>();
        getLog.building = destination;
        getLog.gather = new ItemType[] { ItemType.LOG };
        getLog.give = new ItemType[] { };
        getLog.recipe = MasterRecipe.Instance.Log;
        getLog.fun1 = new instructionFunction((getLog.building).MakeRecipe);

        instructions.Add(getLog);

        Instruction storeLog = new Instruction();
        storeLog.destination = sheet.baseCity.LogStores[0].gameObject.GetComponent<NavigationWaypoint>();
        storeLog.building = sheet.baseCity.LogStores[0];
        storeLog.gather = new ItemType[] { };
        storeLog.give = new ItemType[] { ItemType.LOG };
        storeLog.fun1 = new instructionFunction((storeLog.building).StoreItem);
        storeLog.fun2 = new instructionFunction2((destination).ReleaseJob);
        instructions.Add(storeLog);

        return instructions;
    }
}
