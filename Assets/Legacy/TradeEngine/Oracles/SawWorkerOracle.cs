using UnityEngine;
using System.Collections.Generic;

public class SawWorkerOracle
{
    public List<Instruction> GetInstructions(CharacterSheet sheet)
    {
        List<Instruction> instructions = new List<Instruction>();

        Instruction getLog = new Instruction();
        getLog.destination = sheet.baseCity.LogStores[0].gameObject.GetComponent<NavigationWaypoint>();
        getLog.building = sheet.baseCity.LogStores[0];
        getLog.gather = new ItemType[] { ItemType.LOG };
        getLog.give = new ItemType[] { };
        getLog.fun1 = new instructionFunction((getLog.building).GetItem);

        instructions.Add(getLog);

        Instruction makeLumber = new Instruction();
        SawHouse destination = null;
        foreach (SawHouse sawHouse in sheet.baseCity.SawHouses)
        {
            if (sawHouse.workers.Contains(sheet))
            {
                destination = sawHouse;
                break;
            }
        }

        if (destination == null)
        {
            foreach (SawHouse sawHouse in sheet.baseCity.SawHouses)
            {
                if (sawHouse.CurrentPositions[Jobs.SAWWORKER] > 0)
                {
                    destination = sawHouse;
                    sawHouse.workers.Add(sheet);
                    sawHouse.CurrentPositions[Jobs.SAWWORKER]--;
                    break;
                }
            }
        }
        makeLumber.destination = destination.gameObject.GetComponent<NavigationWaypoint>();
        makeLumber.building = destination;
        makeLumber.gather = new ItemType[] { ItemType.LUMBERPLANK };
        makeLumber.give = new ItemType[] { ItemType.LOG };
        makeLumber.recipe = MasterRecipe.Instance.LumberPlank;
        makeLumber.fun1 = new instructionFunction((makeLumber.building).MakeRecipe);

        instructions.Add(makeLumber);

        Instruction storeLumber = new Instruction();
        storeLumber.destination = destination.gameObject.GetComponent<NavigationWaypoint>();
        storeLumber.building = destination;
        storeLumber.gather = new ItemType[] { };
        storeLumber.give = new ItemType[] { ItemType.LUMBERPLANK };
        storeLumber.fun1 = new instructionFunction((storeLumber.building).StoreItem);
        storeLumber.fun2 = new instructionFunction2((destination).ReleaseJob);
        instructions.Add(storeLumber);

        return instructions;
    }
}
