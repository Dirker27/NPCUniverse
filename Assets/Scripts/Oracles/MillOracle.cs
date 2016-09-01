using UnityEngine;
using System.Collections.Generic;

public class MillOracle : MonoBehaviour
{
    public List<Instruction> GetInstructions(TradeCity currentCity)
    {
        List<Instruction> instructions = new List<Instruction>();

        Instruction getWheat = new Instruction();
        getWheat.destination = currentCity.Barns[0].gameObject.GetComponent<NavigationWaypoint>();
        getWheat.building = currentCity.Barns[0];
        getWheat.gather = new ItemType[] { ItemType.WHEAT };
        getWheat.give = new ItemType[] { };
        getWheat.fun1 = new instructionFunction((getWheat.building).GetItem);

        instructions.Add(getWheat);

        Instruction makeFlour = new Instruction();
        makeFlour.destination = currentCity.Mills[0].gameObject.GetComponent<NavigationWaypoint>();
        makeFlour.building = currentCity.Mills[0];
        makeFlour.gather = new ItemType[] { ItemType.FLOUR };
        makeFlour.give = new ItemType[] { ItemType.WHEAT };
        makeFlour.recipe = MasterRecipe.Instance.Armor;
        makeFlour.fun1 = new instructionFunction((makeFlour.building).MakeRecipe);

        instructions.Add(makeFlour);

        Instruction storeFlour = new Instruction();
        storeFlour.destination = currentCity.Mills[0].gameObject.GetComponent<NavigationWaypoint>();
        storeFlour.building = currentCity.Mills[0];
        storeFlour.gather = new ItemType[] { };
        storeFlour.give = new ItemType[] { ItemType.FLOUR };
        storeFlour.fun1 = new instructionFunction((storeFlour.building).StoreItem);

        instructions.Add(storeFlour);

        return instructions;
    }
}
