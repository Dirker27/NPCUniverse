using UnityEngine;
using System.Collections.Generic;

public class WoodCuterOracle
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

        Instruction makeFireWood = new Instruction();
        WoodCut destination = null;
        foreach (WoodCut woodCut in sheet.baseCity.WoodCuts)
        {
            if (woodCut.workers.Contains(sheet))
            {
                destination = woodCut;
                break;
            }
        }

        if (destination == null)
        {
            foreach (WoodCut woodCut in sheet.baseCity.WoodCuts)
            {
                if (woodCut.CurrentPositions[Jobs.WOODCUTER] > 0)
                {
                    destination = woodCut;
                    woodCut.workers.Add(sheet);
                    woodCut.CurrentPositions[Jobs.WOODCUTER]--;
                    break;
                }
            }
        }

        makeFireWood.destination = destination.gameObject.GetComponent<NavigationWaypoint>();
        makeFireWood.building = destination;
        makeFireWood.gather = new ItemType[] { ItemType.FIREWOOD };
        makeFireWood.give = new ItemType[] { ItemType.LOG };
        makeFireWood.recipe = MasterRecipe.Instance.FireWood;
        makeFireWood.fun1 = new instructionFunction((makeFireWood.building).MakeRecipe);

        instructions.Add(makeFireWood);

        Instruction storeFireWood = new Instruction();
        storeFireWood.destination = destination.gameObject.GetComponent<NavigationWaypoint>();
        storeFireWood.building = destination;
        storeFireWood.gather = new ItemType[] { };
        storeFireWood.give = new ItemType[] { ItemType.FIREWOOD };
        storeFireWood.fun1 = new instructionFunction((storeFireWood.building).StoreItem);
        storeFireWood.fun2 = new instructionFunction2((destination).ReleaseJob);

        instructions.Add(storeFireWood);

        return instructions;
    }
}
