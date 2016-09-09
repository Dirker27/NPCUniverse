using UnityEngine;
using System.Collections.Generic;

public class ForesterOracle
{
    public List<Instruction> GetInstructions(TradeCity currentCity)
    {
        List<Instruction> instructions = new List<Instruction>();

        Instruction getLog = new Instruction();
        getLog.destination = currentCity.Forests[0].gameObject.GetComponent<NavigationWaypoint>();
        getLog.building = currentCity.Forests[0];
        getLog.gather = new ItemType[] { ItemType.LOG };
        getLog.give = new ItemType[] { };
        getLog.recipe = MasterRecipe.Instance.Log;
        getLog.fun1 = new instructionFunction((getLog.building).MakeRecipe);

        instructions.Add(getLog);

        Instruction storeLog = new Instruction();
        storeLog.destination = currentCity.LogStores[0].gameObject.GetComponent<NavigationWaypoint>();
        storeLog.building = currentCity.LogStores[0];
        storeLog.gather = new ItemType[] { };
        storeLog.give = new ItemType[] { ItemType.LOG };
        storeLog.fun1 = new instructionFunction((storeLog.building).StoreItem);

        instructions.Add(storeLog);

        return instructions;
    }
}
