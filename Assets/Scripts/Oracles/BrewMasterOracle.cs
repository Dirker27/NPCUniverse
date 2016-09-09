using UnityEngine;
using System.Collections.Generic;

public class BrewMasterOracle
{
    public List<Instruction> GetInstructions(TradeCity currentCity)
    {
        List<Instruction> instructions = new List<Instruction>();

        Instruction getBarley = new Instruction();
        getBarley.destination = currentCity.Barns[0].gameObject.GetComponent<NavigationWaypoint>();
        getBarley.building = currentCity.Barns[0];
        getBarley.gather = new ItemType[] { ItemType.BARLEY };
        getBarley.give = new ItemType[] { };
        getBarley.fun1 = new instructionFunction((getBarley.building).GetItem);

        instructions.Add(getBarley);

        Instruction makeBeer = new Instruction();
        makeBeer.destination = currentCity.Brewhouses[0].gameObject.GetComponent<NavigationWaypoint>();
        makeBeer.building = currentCity.Brewhouses[0];
        makeBeer.gather = new ItemType[] { ItemType.BEER };
        makeBeer.give = new ItemType[] { ItemType.BARLEY };
        makeBeer.recipe = MasterRecipe.Instance.Beer;
        makeBeer.fun1 = new instructionFunction((makeBeer.building).MakeRecipe);

        instructions.Add(makeBeer);

        Instruction storeBeer = new Instruction();
        storeBeer.destination = currentCity.Brewhouses[0].gameObject.GetComponent<NavigationWaypoint>();
        storeBeer.building = currentCity.Brewhouses[0];
        storeBeer.gather = new ItemType[] { };
        storeBeer.give = new ItemType[] { ItemType.BEER };
        storeBeer.fun1 = new instructionFunction((storeBeer.building).StoreItem);

        instructions.Add(storeBeer);

        return instructions;
    }
}
