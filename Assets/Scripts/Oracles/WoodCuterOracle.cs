using UnityEngine;
using System.Collections.Generic;

public class WoodCuterOracle : MonoBehaviour
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

        Instruction makeFireWood = new Instruction();
        makeFireWood.destination = currentCity.WoodCuts[0].gameObject.GetComponent<NavigationWaypoint>();
        makeFireWood.building = currentCity.WoodCuts[0];
        makeFireWood.gather = new ItemType[] { ItemType.FIREWOOD };
        makeFireWood.give = new ItemType[] { ItemType.LOG };
        makeFireWood.recipe = MasterRecipe.Instance.FireWood;
        makeFireWood.fun1 = new instructionFunction((makeFireWood.building).MakeRecipe);

        instructions.Add(makeFireWood);

        Instruction storeFireWood = new Instruction();
        storeFireWood.destination = currentCity.WoodCuts[0].gameObject.GetComponent<NavigationWaypoint>();
        storeFireWood.building = currentCity.WoodCuts[0];
        storeFireWood.gather = new ItemType[] { };
        storeFireWood.give = new ItemType[] { ItemType.FIREWOOD };
        storeFireWood.fun1 = new instructionFunction((storeFireWood.building).StoreItem);

        instructions.Add(storeFireWood);

        return instructions;
    }
}
