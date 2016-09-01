using UnityEngine;
using System.Collections.Generic;

public class StoneCutterOracle : MonoBehaviour
{
    public List<Instruction> GetInstructions(TradeCity currentCity)
    {
        List<Instruction> instructions = new List<Instruction>();

        Instruction getStone = new Instruction();
        getStone.destination = currentCity.OreShops[0].gameObject.GetComponent<NavigationWaypoint>();
        getStone.building = currentCity.OreShops[0];
        getStone.gather = new ItemType[] { ItemType.STONE };
        getStone.give = new ItemType[] { };
        getStone.fun1 = new instructionFunction((getStone.building).GetItem);

        instructions.Add(getStone);

        Instruction makeStoneBlock = new Instruction();
        makeStoneBlock.destination = currentCity.Masonries[0].gameObject.GetComponent<NavigationWaypoint>();
        makeStoneBlock.building = currentCity.Masonries[0];
        makeStoneBlock.gather = new ItemType[] { ItemType.STONEBLOCK };
        makeStoneBlock.give = new ItemType[] { ItemType.STONE };
        makeStoneBlock.recipe = MasterRecipe.Instance.StoneBlock;
        makeStoneBlock.fun1 = new instructionFunction((makeStoneBlock.building).MakeRecipe);

        instructions.Add(makeStoneBlock);

        Instruction storeStoneBlock = new Instruction();
        storeStoneBlock.destination = currentCity.Masonries[0].gameObject.GetComponent<NavigationWaypoint>();
        storeStoneBlock.building = currentCity.Masonries[0];
        storeStoneBlock.gather = new ItemType[] { };
        storeStoneBlock.give = new ItemType[] { ItemType.STONEBLOCK };
        storeStoneBlock.fun1 = new instructionFunction((storeStoneBlock.building).StoreItem);

        instructions.Add(storeStoneBlock);

        return instructions;
    }
}
