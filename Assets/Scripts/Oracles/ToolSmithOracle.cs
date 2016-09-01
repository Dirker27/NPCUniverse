using UnityEngine;
using System.Collections.Generic;

public class ToolSmithOracle : MonoBehaviour
{
    public List<Instruction> GetInstructions(TradeCity currentCity)
    {
        List<Instruction> instructions = new List<Instruction>();

        Instruction getBar = new Instruction();
        getBar.destination = currentCity.Foundries[0].gameObject.GetComponent<NavigationWaypoint>();
        getBar.building = currentCity.Foundries[0];
        getBar.gather = new ItemType[] { ItemType.BAR };
        getBar.give = new ItemType[] { };
        getBar.fun1 = new instructionFunction((getBar.building).GetItem);

        instructions.Add(getBar);

        Instruction makeTool = new Instruction();
        makeTool.destination = currentCity.Smithies[0].gameObject.GetComponent<NavigationWaypoint>();
        makeTool.building = currentCity.Smithies[0];
        makeTool.gather = new ItemType[] { ItemType.TOOL };
        makeTool.give = new ItemType[] { ItemType.BAR };
        makeTool.recipe = MasterRecipe.Instance.Tool;
        makeTool.fun1 = new instructionFunction((makeTool.building).MakeRecipe);

        instructions.Add(makeTool);

        Instruction storeTool = new Instruction();
        storeTool.destination = currentCity.Smithies[0].gameObject.GetComponent<NavigationWaypoint>();
        storeTool.building = currentCity.Smithies[0];
        storeTool.gather = new ItemType[] { };
        storeTool.give = new ItemType[] { ItemType.TOOL };
        storeTool.fun1 = new instructionFunction((storeTool.building).StoreItem);

        instructions.Add(storeTool);

        return instructions;
    }
}
