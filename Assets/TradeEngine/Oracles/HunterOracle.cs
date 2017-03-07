using UnityEngine;
using System.Collections.Generic;

public class HunterOracle
{
    public List<Instruction> GetInstructions(CharacterSheet sheet)
    {
        List<Instruction> instructions = new List<Instruction>();

        Instruction getBow = new Instruction();
        getBow.destination = sheet.baseCity.BowShops[0].gameObject.GetComponent<NavigationWaypoint>();
        getBow.building = sheet.baseCity.BowShops[0];
        getBow.gather = new ItemType[] { ItemType.BOW };
        getBow.give = new ItemType[] { };
        getBow.fun1 = new instructionFunction((getBow.building).GetItem);

        instructions.Add(getBow);
        instructions.Add(getBow);

        Instruction getArrow = new Instruction();
        getArrow.destination = sheet.baseCity.BowShops[0].gameObject.GetComponent<NavigationWaypoint>();
        getArrow.building = sheet.baseCity.BowShops[0];
        getArrow.gather = new ItemType[] { ItemType.ARROW };
        getArrow.give = new ItemType[] { };
        getArrow.fun1 = new instructionFunction((getArrow.building).GetItem);

        instructions.Add(getArrow);
        instructions.Add(getArrow);

        Instruction hunt = new Instruction();
        HuntingLodge destination = null;
        foreach (HuntingLodge huntingLodge in sheet.baseCity.HuntingLodges)
        {
            if (huntingLodge.workers.Contains(sheet))
            {
                destination = huntingLodge;
                break;
            }
        }

        if (destination == null)
        {
            foreach (HuntingLodge huntingLodge in sheet.baseCity.HuntingLodges)
            {
                if (huntingLodge.CurrentPositions[Jobs.HUNTER] > 0)
                {
                    destination = huntingLodge;
                    huntingLodge.workers.Add(sheet);
                    huntingLodge.CurrentPositions[Jobs.HUNTER]--;
                    break;
                }
            }
        }
        hunt.destination = destination.gameObject.GetComponent<NavigationWaypoint>();
        hunt.building = destination;
        hunt.gather = new ItemType[] { ItemType.MEAT };
        hunt.give = new ItemType[] { ItemType.BOW, ItemType.ARROW };
        hunt.recipe = MasterRecipe.Instance.Meat;
        hunt.fun1 = new instructionFunction((hunt.building).MakeRecipe);

        instructions.Add(hunt);

        Instruction skin = new Instruction();
        skin.destination = destination.gameObject.GetComponent<NavigationWaypoint>();
        skin.building = destination;
        skin.gather = new ItemType[] { ItemType.LEATHER };
        skin.give = new ItemType[] { ItemType.BOW, ItemType.ARROW };
        skin.recipe = MasterRecipe.Instance.Leather;
        skin.fun1 = new instructionFunction((skin.building).MakeRecipe);

        instructions.Add(skin);

        Instruction storeMeat = new Instruction();
        storeMeat.destination = destination.gameObject.GetComponent<NavigationWaypoint>();
        storeMeat.building = destination;
        storeMeat.gather = new ItemType[] { };
        storeMeat.give = new ItemType[] { ItemType.MEAT };
        storeMeat.fun1 = new instructionFunction((storeMeat.building).StoreItem);

        instructions.Add(storeMeat);

        Instruction storeLeather = new Instruction();
        storeLeather.destination = destination.gameObject.GetComponent<NavigationWaypoint>();
        storeLeather.building = destination;
        storeLeather.gather = new ItemType[] { };
        storeLeather.give = new ItemType[] { ItemType.LEATHER };
        storeLeather.fun1 = new instructionFunction((storeLeather.building).StoreItem);
        storeLeather.fun2 = new instructionFunction2((destination).ReleaseJob);
        instructions.Add(storeLeather);

        return instructions;
    }
}
