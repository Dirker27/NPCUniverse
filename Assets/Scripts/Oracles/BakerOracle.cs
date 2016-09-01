using UnityEngine;
using System.Collections.Generic;

public class BakerOracle : MonoBehaviour
{
    public List<Instruction> GetInstructions(TradeCity currentCity)
    {
        List<Instruction> instructions = new List<Instruction>();

        Instruction getFlour = new Instruction();
        getFlour.destination = currentCity.Mills[0].gameObject.GetComponent<NavigationWaypoint>();
        getFlour.building = currentCity.Mills[0];
        getFlour.gather = new ItemType[] { ItemType.FLOUR };
        getFlour.give = new ItemType[] { };
        getFlour.fun1 = new instructionFunction((getFlour.building).GetItem);

        instructions.Add(getFlour);

        Instruction makeBread = new Instruction();
        makeBread.destination = currentCity.Bakeries[0].gameObject.GetComponent<NavigationWaypoint>();
        makeBread.building = currentCity.Bakeries[0];
        makeBread.gather = new ItemType[] { ItemType.BREAD };
        makeBread.give = new ItemType[] { ItemType.FLOUR };
        makeBread.recipe = MasterRecipe.Instance.Bread;
        makeBread.fun1 = new instructionFunction((makeBread.building).MakeRecipe);

        instructions.Add(makeBread);

        Instruction storeBread = new Instruction();
        storeBread.destination = currentCity.Bakeries[0].gameObject.GetComponent<NavigationWaypoint>();
        storeBread.building = currentCity.Bakeries[0];
        storeBread.gather = new ItemType[] { };
        storeBread.give = new ItemType[] { ItemType.BREAD };
        storeBread.fun1 = new instructionFunction((storeBread.building).StoreItem);

        instructions.Add(storeBread);

        return instructions;
    }
}
