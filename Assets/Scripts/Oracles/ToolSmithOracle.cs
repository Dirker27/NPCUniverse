using UnityEngine;
using System.Collections.Generic;

public class ToolSmithOracle
{
    public List<Instruction> GetInstructions(CharacterSheet sheet)
    {
        List<Instruction> instructions = new List<Instruction>();

        Instruction getBar = new Instruction();
        getBar.destination = sheet.baseCity.Foundries[0].gameObject.GetComponent<NavigationWaypoint>();
        getBar.building = sheet.baseCity.Foundries[0];
        getBar.gather = new ItemType[] { ItemType.BAR };
        getBar.give = new ItemType[] { };
        getBar.fun1 = new instructionFunction((getBar.building).GetItem);

        instructions.Add(getBar);

        Instruction makeTool = new Instruction();
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
                if (smithy.CurrentPositions[Jobs.TOOLSMITH] > 0)
                {
                    destination = smithy;
                    smithy.workers.Add(sheet);
                    smithy.CurrentPositions[Jobs.TOOLSMITH]--;
                    break;
                }
            }
        }
        makeTool.destination = destination.gameObject.GetComponent<NavigationWaypoint>();
        makeTool.building = destination;
        makeTool.gather = new ItemType[] { ItemType.TOOL };
        makeTool.give = new ItemType[] { ItemType.BAR };
        makeTool.recipe = MasterRecipe.Instance.Tool;
        makeTool.fun1 = new instructionFunction((makeTool.building).MakeRecipe);

        instructions.Add(makeTool);

        Instruction storeTool = new Instruction();
        storeTool.destination = destination.gameObject.GetComponent<NavigationWaypoint>();
        storeTool.building = destination;
        storeTool.gather = new ItemType[] { };
        storeTool.give = new ItemType[] { ItemType.TOOL };
        storeTool.fun1 = new instructionFunction((storeTool.building).StoreItem);
        storeTool.fun2 = new instructionFunction2((destination).ReleaseJob);

        instructions.Add(storeTool);

        return instructions;
    }
}
