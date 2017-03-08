using UnityEngine;
using System.Collections.Generic;

public class FletcherOracle
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
        instructions.Add(getLog);

        Instruction makeBow = new Instruction();
        BowShop destination = null;
        foreach (BowShop bowShop in sheet.baseCity.BowShops)
        {
            if (bowShop.workers.Contains(sheet))
            {
                destination = bowShop;
                break;
            }
        }

        if (destination == null)
        {
            foreach (BowShop bowShop in sheet.baseCity.BowShops)
            {
                if (bowShop.CurrentPositions[Jobs.FLETCHER] > 0)
                {
                    destination = bowShop;
                    bowShop.workers.Add(sheet);
                    bowShop.CurrentPositions[Jobs.FLETCHER]--;
                    break;
                }
            }
        }
        makeBow.destination = destination.gameObject.GetComponent<NavigationWaypoint>();
        makeBow.building = destination;
        makeBow.gather = new ItemType[] { ItemType.BOW };
        makeBow.give = new ItemType[] { ItemType.LOG };
        makeBow.recipe = MasterRecipe.Instance.Bow;
        makeBow.fun1 = new instructionFunction((makeBow.building).MakeRecipe);

        instructions.Add(makeBow);

        Instruction makeArrow = new Instruction();
        makeArrow.destination = destination.gameObject.GetComponent<NavigationWaypoint>();
        makeArrow.building = destination;
        makeArrow.gather = new ItemType[] { ItemType.ARROW };
        makeArrow.give = new ItemType[] { ItemType.LOG };
        makeArrow.recipe = MasterRecipe.Instance.Arrow;
        makeArrow.fun1 = new instructionFunction((makeArrow.building).MakeRecipe);

        instructions.Add(makeArrow);

        Instruction storeBow = new Instruction();
        storeBow.destination = destination.gameObject.GetComponent<NavigationWaypoint>();
        storeBow.building = destination;
        storeBow.gather = new ItemType[] { };
        storeBow.give = new ItemType[] { ItemType.BOW };
        storeBow.fun1 = new instructionFunction((storeBow.building).StoreItem);

        instructions.Add(storeBow);

        Instruction storeArrow = new Instruction();
        storeArrow.destination = destination.gameObject.GetComponent<NavigationWaypoint>();
        storeArrow.building = destination;
        storeArrow.gather = new ItemType[] { };
        storeArrow.give = new ItemType[] { ItemType.ARROW };
        storeArrow.fun1 = new instructionFunction((storeArrow.building).StoreItem);
        storeArrow.fun2 = new instructionFunction2((destination).ReleaseJob);
        instructions.Add(storeArrow);

        return instructions;
    }
}
