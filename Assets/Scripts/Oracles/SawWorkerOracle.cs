using UnityEngine;
using System.Collections.Generic;

public class SawWorkerOracle : MonoBehaviour
{
    public List<Instruction> GetInstructions(TradeCity currentCity)
    {
        List<Instruction> instructions = new List<Instruction>();

        Instruction getLog = new Instruction();
        getLog.destination = currentCity.LogStores[0].gameObject.GetComponent<NavigationWaypoint>();
        getLog.building = currentCity.LogStores[0];
        getLog.gather = new ItemType[] { ItemType.LOG };
        getLog.give = new ItemType[] { };
        getLog.fun1 = new instructionFunction((getLog.building).GetItem);

        instructions.Add(getLog);

        Instruction makeLumber = new Instruction();
        makeLumber.destination = currentCity.SawHouses[0].gameObject.GetComponent<NavigationWaypoint>();
        makeLumber.building = currentCity.SawHouses[0];
        makeLumber.gather = new ItemType[] { ItemType.LUMBERPLANK };
        makeLumber.give = new ItemType[] { ItemType.LOG };
        makeLumber.recipe = MasterRecipe.Instance.LumberPlank;
        makeLumber.fun1 = new instructionFunction((makeLumber.building).MakeRecipe);

        instructions.Add(makeLumber);

        Instruction storeLumber = new Instruction();
        storeLumber.destination = currentCity.SawHouses[0].gameObject.GetComponent<NavigationWaypoint>();
        storeLumber.building = currentCity.SawHouses[0];
        storeLumber.gather = new ItemType[] { };
        storeLumber.give = new ItemType[] { ItemType.LUMBERPLANK };
        storeLumber.fun1 = new instructionFunction((storeLumber.building).StoreItem);

        instructions.Add(storeLumber);

        return instructions;
    }
}
