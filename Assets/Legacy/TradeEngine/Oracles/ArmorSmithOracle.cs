using UnityEngine;
using System.Collections.Generic;

public class ArmorSmithOracle
{
    public List<Instruction> GetInstructions(CharacterSheet sheet)
    {
        List<Instruction> instructions = new List<Instruction>();

        Instruction getBar = new Instruction();
        getBar.destination = sheet.baseCity.Foundries[0].gameObject.GetComponent<NavigationWaypoint>();
        getBar.building = sheet.baseCity.Foundries[0];
        getBar.gather = new ItemType[] {ItemType.BAR};
        getBar.give = new ItemType[] {};
        getBar.fun1 = new instructionFunction((getBar.building).GetItem);

        instructions.Add(getBar);

        Instruction makeArmor = new Instruction();
        Smithy destination = null;
        foreach (Smithy smithy in sheet.baseCity.Smithies)
        {
            if (smithy.workers.Contains(sheet))
            {
                destination = smithy;
                break;
            }
        }

        if (destination == null)
        {
            foreach (Smithy smithy in sheet.baseCity.Smithies)
            {
                if (smithy.CurrentPositions[Jobs.ARMORSMITH] > 0)
                {
                    destination = smithy;
                    smithy.workers.Add(sheet);
                    smithy.CurrentPositions[Jobs.ARMORSMITH]--;
                    break;
                }
            }
        }
        makeArmor.destination = destination.gameObject.GetComponent<NavigationWaypoint>();
        makeArmor.building = destination;
        makeArmor.gather = new ItemType[] { ItemType.ARMOR };
        makeArmor.give = new ItemType[] { ItemType.BAR };
        makeArmor.recipe = MasterRecipe.Instance.Armor;
        makeArmor.fun1 = new instructionFunction((makeArmor.building).MakeRecipe);

        instructions.Add(makeArmor);

        Instruction storeArmor = new Instruction();
        storeArmor.destination = destination.gameObject.GetComponent<NavigationWaypoint>();
        storeArmor.building = destination;
        storeArmor.gather = new ItemType[] { };
        storeArmor.give = new ItemType[] { ItemType.ARMOR };
        storeArmor.fun1 = new instructionFunction((storeArmor.building).StoreItem);
        storeArmor.fun2 = new instructionFunction2((destination).ReleaseJob);

        instructions.Add(storeArmor);

        return instructions;
    }
}
