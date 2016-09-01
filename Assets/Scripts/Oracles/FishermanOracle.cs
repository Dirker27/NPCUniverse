using UnityEngine;
using System.Collections.Generic;

public class FishermanOracle : MonoBehaviour
{
    public List<Instruction> GetInstructions(TradeCity currentCity)
    {
        List<Instruction> instructions = new List<Instruction>();

        Instruction getFish = new Instruction();
        getFish.destination = currentCity.Ponds[0].gameObject.GetComponent<NavigationWaypoint>();
        getFish.building = currentCity.Ponds[0];
        getFish.gather = new ItemType[] { ItemType.FISH };
        getFish.give = new ItemType[] { };
        getFish.recipe = MasterRecipe.Instance.Fish;
        getFish.fun1 = new instructionFunction((getFish.building).MakeRecipe);

        instructions.Add(getFish);

        Instruction storeFish = new Instruction();
        storeFish.destination = currentCity.Barns[0].gameObject.GetComponent<NavigationWaypoint>();
        storeFish.building = currentCity.Barns[0];
        storeFish.gather = new ItemType[] { };
        storeFish.give = new ItemType[] { ItemType.FISH };
        storeFish.fun1 = new instructionFunction((storeFish.building).StoreItem);

        instructions.Add(storeFish);

        return instructions;
    }
}
