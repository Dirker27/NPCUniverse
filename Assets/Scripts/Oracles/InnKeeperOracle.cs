using UnityEngine;
using System.Collections.Generic;

public class InnKeeperOracle : MonoBehaviour
{
    public List<Instruction> GetInstructions(TradeCity currentCity)
    {
        List<Instruction> instructions = new List<Instruction>();

        Instruction getBeer = new Instruction();
        getBeer.destination = currentCity.Brewhouses[0].gameObject.GetComponent<NavigationWaypoint>();
        getBeer.building = currentCity.Brewhouses[0];
        getBeer.gather = new ItemType[] { ItemType.BEER };
        getBeer.give = new ItemType[] { };
        getBeer.fun1 = new instructionFunction((getBeer.building).GetItem);

        instructions.Add(getBeer);

        Instruction getFish = new Instruction();
        getFish.destination = currentCity.Barns[0].gameObject.GetComponent<NavigationWaypoint>();
        getFish.building = currentCity.Barns[0];
        getFish.gather = new ItemType[] { ItemType.FISH };
        getFish.give = new ItemType[] { };
        getFish.fun1 = new instructionFunction((getFish.building).GetItem);

        instructions.Add(getFish);

        Instruction getBread = new Instruction();
        getBread.destination = currentCity.Bakeries[0].gameObject.GetComponent<NavigationWaypoint>();
        getBread.building = currentCity.Bakeries[0];
        getBread.gather = new ItemType[] { ItemType.BREAD };
        getBread.give = new ItemType[] { };
        getBread.fun1 = new instructionFunction((getBread.building).GetItem);

        instructions.Add(getBread);


        Instruction makeMeal = new Instruction();
        makeMeal.destination = currentCity.Taverns[0].gameObject.GetComponent<NavigationWaypoint>();
        makeMeal.building = currentCity.Taverns[0];
        makeMeal.gather = new ItemType[] { ItemType.MEAL };
        makeMeal.give = new ItemType[] { ItemType.BEER, ItemType.BREAD, ItemType.FISH };
        makeMeal.recipe = MasterRecipe.Instance.Meal;
        makeMeal.fun1 = new instructionFunction((makeMeal.building).MakeRecipe);

        instructions.Add(makeMeal);

        Instruction storeMeal = new Instruction();
        storeMeal.destination = currentCity.Taverns[0].gameObject.GetComponent<NavigationWaypoint>();
        storeMeal.building = currentCity.Taverns[0];
        storeMeal.gather = new ItemType[] { };
        storeMeal.give = new ItemType[] { ItemType.MEAL };
        storeMeal.fun1 = new instructionFunction((storeMeal.building).StoreItem);

        instructions.Add(storeMeal);

        return instructions;
    }
}
