using UnityEngine;
using System.Collections.Generic;

public class ArmorSmithOracle : MonoBehaviour
{
    public List<Instruction> GetInstructions(TradeCity currentCity)
    {
        List<Instruction> instructions = new List<Instruction>();

        Instruction getBar = new Instruction();
        getBar.destination = currentCity.Foundries[0].gameObject.GetComponent<NavigationWaypoint>();
        getBar.building = currentCity.Foundries[0];
        getBar.gather = new ItemType[] {ItemType.BAR};
        getBar.give = new ItemType[] {};
        getBar.fun1 = new instructionFunction((getBar.building).GetItem);

        instructions.Add(getBar);

        Instruction makeArmor = new Instruction();
        makeArmor.destination = currentCity.Smithies[0].gameObject.GetComponent<NavigationWaypoint>();
        makeArmor.building = currentCity.Smithies[0];
        makeArmor.gather = new ItemType[] { ItemType.ARMOR };
        makeArmor.give = new ItemType[] { ItemType.BAR };
        makeArmor.recipe = MasterRecipe.Instance.Armor;
        makeArmor.fun1 = new instructionFunction((makeArmor.building).MakeRecipe);

        instructions.Add(makeArmor);

        Instruction storeArmor = new Instruction();
        storeArmor.destination = currentCity.Smithies[0].gameObject.GetComponent<NavigationWaypoint>();
        storeArmor.building = currentCity.Smithies[0];
        storeArmor.gather = new ItemType[] { };
        storeArmor.give = new ItemType[] { ItemType.ARMOR };
        storeArmor.fun1 = new instructionFunction((storeArmor.building).StoreItem);

        instructions.Add(storeArmor);

        return instructions;
    }
}
