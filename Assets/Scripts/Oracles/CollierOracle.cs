using UnityEngine;
using System.Collections.Generic;

public class CollierOracle : MonoBehaviour
{
    public List<Instruction> GetInstructions(TradeCity currentCity)
    {
        List<Instruction> instructions = new List<Instruction>();

        Instruction getFirewood = new Instruction();
        getFirewood.destination = currentCity.WoodCuts[0].gameObject.GetComponent<NavigationWaypoint>();
        getFirewood.building = currentCity.WoodCuts[0];
        getFirewood.gather = new ItemType[] { ItemType.FIREWOOD };
        getFirewood.give = new ItemType[] { };
        getFirewood.fun1 = new instructionFunction((getFirewood.building).GetItem);

        instructions.Add(getFirewood);

        Instruction getCharcoal = new Instruction();
        getCharcoal.destination = currentCity.CharcoalPits[0].gameObject.GetComponent<NavigationWaypoint>();
        getCharcoal.building = currentCity.CharcoalPits[0];
        getCharcoal.gather = new ItemType[] { ItemType.CHARCOAL };
        getCharcoal.give = new ItemType[] { ItemType.FIREWOOD };
        getCharcoal.fun1 = new instructionFunction((getCharcoal.building).MakeRecipe);

        instructions.Add(getCharcoal);

        Instruction storeCharcoal = new Instruction();
        storeCharcoal.destination = currentCity.CharcoalPits[0].gameObject.GetComponent<NavigationWaypoint>();
        storeCharcoal.building = currentCity.CharcoalPits[0];
        storeCharcoal.gather = new ItemType[] { };
        storeCharcoal.give = new ItemType[] { ItemType.CHARCOAL };
        storeCharcoal.fun1 = new instructionFunction((storeCharcoal.building).StoreItem);

        instructions.Add(storeCharcoal);

        return instructions;
    }
}
