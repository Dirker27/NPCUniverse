using UnityEngine;
using System.Collections.Generic;

public class BakerOracle
{
    public List<Instruction> GetInstructions(CharacterSheet sheet)
    {
        List<Instruction> instructions = new List<Instruction>();

        Instruction getFlour = new Instruction();
        getFlour.destination = sheet.baseCity.Mills[0].gameObject.GetComponent<NavigationWaypoint>();
        getFlour.building = sheet.baseCity.Mills[0];
        getFlour.gather = new ItemType[] { ItemType.FLOUR };
        getFlour.give = new ItemType[] { };
        getFlour.fun1 = new instructionFunction((getFlour.building).GetItem);

        instructions.Add(getFlour);

        Instruction makeBread = new Instruction();
        Bakery destination = null;
        foreach (Bakery bakery in sheet.baseCity.Bakeries)
        {
            if (bakery.workers.Contains(sheet))
            {
                destination = bakery;
                break;
            }
        }

        if (destination == null)
        {
            foreach (Bakery bakery in sheet.baseCity.Bakeries)
            {
                if (bakery.CurrentPositions[Jobs.BAKER] > 0)
                {
                    destination = bakery;
                    bakery.workers.Add(sheet);
                    bakery.CurrentPositions[Jobs.BAKER]--;
                    break;
                }
            }
        }
        makeBread.destination = destination.gameObject.GetComponent<NavigationWaypoint>();
        makeBread.building = destination;
        makeBread.gather = new ItemType[] { ItemType.BREAD };
        makeBread.give = new ItemType[] { ItemType.FLOUR };
        makeBread.recipe = MasterRecipe.Instance.Bread;
        makeBread.fun1 = new instructionFunction((makeBread.building).MakeRecipe);

        instructions.Add(makeBread);

        Instruction storeBread = new Instruction();
        storeBread.destination = destination.gameObject.GetComponent<NavigationWaypoint>();
        storeBread.building = destination;
        storeBread.gather = new ItemType[] { };
        storeBread.give = new ItemType[] { ItemType.BREAD };
        storeBread.fun1 = new instructionFunction((storeBread.building).StoreItem);
        storeBread.fun2 = new instructionFunction2((destination).ReleaseJob);
        instructions.Add(storeBread);

        return instructions;
    }
}
