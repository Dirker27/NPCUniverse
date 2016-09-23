using UnityEngine;
using System.Collections.Generic;

public class BrewMasterOracle
{
    public List<Instruction> GetInstructions(CharacterSheet sheet)
    {
        List<Instruction> instructions = new List<Instruction>();

        Instruction getBarley = new Instruction();
        getBarley.destination = sheet.baseCity.Barns[0].gameObject.GetComponent<NavigationWaypoint>();
        getBarley.building = sheet.baseCity.Barns[0];
        getBarley.gather = new ItemType[] { ItemType.BARLEY };
        getBarley.give = new ItemType[] { };
        getBarley.fun1 = new instructionFunction((getBarley.building).GetItem);

        instructions.Add(getBarley);

        Instruction makeBeer = new Instruction();
        Brewhouse destination = null;
        foreach (Brewhouse brewHouse in sheet.baseCity.Brewhouses)
        {
            if (brewHouse.workers.Contains(sheet))
            {
                destination = brewHouse;
                break;
            }
        }

        if (destination == null)
        {
            foreach (Brewhouse brewHouse in sheet.baseCity.Brewhouses)
            {
                if (brewHouse.CurrentPositions[Jobs.BREWMASTER] > 0)
                {
                    destination = brewHouse;
                    brewHouse.workers.Add(sheet);
                    brewHouse.CurrentPositions[Jobs.BREWMASTER]--;
                    break;
                }
            }
        }
        makeBeer.destination = destination.gameObject.GetComponent<NavigationWaypoint>();
        makeBeer.building = destination;
        makeBeer.gather = new ItemType[] { ItemType.BEER };
        makeBeer.give = new ItemType[] { ItemType.BARLEY };
        makeBeer.recipe = MasterRecipe.Instance.Beer;
        makeBeer.fun1 = new instructionFunction((makeBeer.building).MakeRecipe);

        instructions.Add(makeBeer);

        Instruction storeBeer = new Instruction();
        storeBeer.destination = destination.gameObject.GetComponent<NavigationWaypoint>();
        storeBeer.building = destination;
        storeBeer.gather = new ItemType[] { };
        storeBeer.give = new ItemType[] { ItemType.BEER };
        storeBeer.fun1 = new instructionFunction((storeBeer.building).StoreItem);
        storeBeer.fun2 = new instructionFunction2((destination).ReleaseJob);

        instructions.Add(storeBeer);

        return instructions;
    }
}
