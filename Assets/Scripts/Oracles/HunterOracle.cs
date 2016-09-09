using UnityEngine;
using System.Collections.Generic;

public class HunterOracle
{
    public List<Instruction> GetInstructions(TradeCity currentCity)
    {
        List<Instruction> instructions = new List<Instruction>();

        Instruction getBow = new Instruction();
        getBow.destination = currentCity.BowShops[0].gameObject.GetComponent<NavigationWaypoint>();
        getBow.building = currentCity.BowShops[0];
        getBow.gather = new ItemType[] { ItemType.BOW };
        getBow.give = new ItemType[] { };
        getBow.fun1 = new instructionFunction((getBow.building).GetItem);

        instructions.Add(getBow);
        instructions.Add(getBow);

        Instruction getArrow = new Instruction();
        getArrow.destination = currentCity.BowShops[0].gameObject.GetComponent<NavigationWaypoint>();
        getArrow.building = currentCity.BowShops[0];
        getArrow.gather = new ItemType[] { ItemType.ARROW };
        getArrow.give = new ItemType[] { };
        getArrow.fun1 = new instructionFunction((getArrow.building).GetItem);

        instructions.Add(getArrow);
        instructions.Add(getArrow);

        Instruction hunt = new Instruction();
        hunt.destination = currentCity.HuntingLodges[0].gameObject.GetComponent<NavigationWaypoint>();
        hunt.building = currentCity.HuntingLodges[0];
        hunt.gather = new ItemType[] { ItemType.MEAT };
        hunt.give = new ItemType[] { ItemType.BOW, ItemType.ARROW };
        hunt.recipe = MasterRecipe.Instance.Meat;
        hunt.fun1 = new instructionFunction((hunt.building).MakeRecipe);

        instructions.Add(hunt);

        Instruction skin = new Instruction();
        skin.destination = currentCity.HuntingLodges[0].gameObject.GetComponent<NavigationWaypoint>();
        skin.building = currentCity.HuntingLodges[0];
        skin.gather = new ItemType[] { ItemType.LEATHER };
        skin.give = new ItemType[] { ItemType.BOW, ItemType.ARROW };
        skin.recipe = MasterRecipe.Instance.Leather;
        skin.fun1 = new instructionFunction((skin.building).MakeRecipe);

        instructions.Add(hunt);

        Instruction storeMeat = new Instruction();
        storeMeat.destination = currentCity.HuntingLodges[0].gameObject.GetComponent<NavigationWaypoint>();
        storeMeat.building = currentCity.HuntingLodges[0];
        storeMeat.gather = new ItemType[] { };
        storeMeat.give = new ItemType[] { ItemType.MEAT };
        storeMeat.fun1 = new instructionFunction((storeMeat.building).StoreItem);

        instructions.Add(storeMeat);

        Instruction storeLeather = new Instruction();
        storeLeather.destination = currentCity.HuntingLodges[0].gameObject.GetComponent<NavigationWaypoint>();
        storeLeather.building = currentCity.HuntingLodges[0];
        storeLeather.gather = new ItemType[] { };
        storeLeather.give = new ItemType[] { ItemType.LEATHER };
        storeLeather.fun1 = new instructionFunction((storeLeather.building).StoreItem);

        instructions.Add(storeLeather);

        return instructions;
    }
}
