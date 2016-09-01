using UnityEngine;
using System.Collections.Generic;

public class FletcherOracle : MonoBehaviour
{
    public List<Instruction> GetInstructions(TradeCity currentCity)
    {
        List<Instruction> instructions = new List<Instruction>();

        Instruction getLog = new Instruction();
        getLog.destination = currentCity.LogStores[0].gameObject.GetComponent<NavigationWaypoint>();
        getLog.building = currentCity.LogStores[0];
        getLog.gather = new ItemType[] { ItemType.LOG };
        getLog.give = new ItemType[] { };
        getLog.fun1 = new instructionFunction((getLog.building).GetItem);

        instructions.Add(getLog);
        instructions.Add(getLog);

        Instruction makeBow = new Instruction();
        makeBow.destination = currentCity.BowShops[0].gameObject.GetComponent<NavigationWaypoint>();
        makeBow.building = currentCity.BowShops[0];
        makeBow.gather = new ItemType[] { ItemType.BOW };
        makeBow.give = new ItemType[] { ItemType.LOG };
        makeBow.recipe = MasterRecipe.Instance.Bow;
        makeBow.fun1 = new instructionFunction((makeBow.building).MakeRecipe);

        instructions.Add(makeBow);

        Instruction makeArrow = new Instruction();
        makeArrow.destination = currentCity.BowShops[0].gameObject.GetComponent<NavigationWaypoint>();
        makeArrow.building = currentCity.BowShops[0];
        makeArrow.gather = new ItemType[] { ItemType.ARROW };
        makeArrow.give = new ItemType[] { ItemType.LOG };
        makeArrow.recipe = MasterRecipe.Instance.Arrow;
        makeArrow.fun1 = new instructionFunction((makeArrow.building).MakeRecipe);

        instructions.Add(makeArrow);

        Instruction storeBow = new Instruction();
        storeBow.destination = currentCity.BowShops[0].gameObject.GetComponent<NavigationWaypoint>();
        storeBow.building = currentCity.BowShops[0];
        storeBow.gather = new ItemType[] { };
        storeBow.give = new ItemType[] { ItemType.BOW };
        storeBow.fun1 = new instructionFunction((storeBow.building).StoreItem);

        instructions.Add(storeBow);

        Instruction storeArrow = new Instruction();
        storeArrow.destination = currentCity.BowShops[0].gameObject.GetComponent<NavigationWaypoint>();
        storeArrow.building = currentCity.BowShops[0];
        storeArrow.gather = new ItemType[] { };
        storeArrow.give = new ItemType[] { ItemType.ARROW };
        storeArrow.fun1 = new instructionFunction((storeArrow.building).StoreItem);

        instructions.Add(storeArrow);

        return instructions;
    }
}
