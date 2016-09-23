using UnityEngine;
using System.Collections.Generic;

public class StoneCutterOracle
{
    public List<Instruction> GetInstructions(CharacterSheet sheet)
    {
        List<Instruction> instructions = new List<Instruction>();

        Instruction getStone = new Instruction();
        getStone.destination = sheet.baseCity.OreShops[0].gameObject.GetComponent<NavigationWaypoint>();
        getStone.building = sheet.baseCity.OreShops[0];
        getStone.gather = new ItemType[] { ItemType.STONE };
        getStone.give = new ItemType[] { };
        getStone.fun1 = new instructionFunction((getStone.building).GetItem);

        instructions.Add(getStone);

        Instruction makeStoneBlock = new Instruction();
        Masonry destination = null;
        foreach (Masonry mason in sheet.baseCity.Masonries)
        {
            if (mason.workers.Contains(sheet))
            {
                destination = mason;
                break;
            }
        }

        if (destination == null)
        {
            foreach (Masonry mason in sheet.baseCity.Masonries)
            {
                if (mason.CurrentPositions[Jobs.STONECUTTER] > 0)
                {
                    destination = mason;
                    mason.workers.Add(sheet);
                    mason.CurrentPositions[Jobs.STONECUTTER]--;
                    break;
                }
            }
        }
        makeStoneBlock.destination = destination.gameObject.GetComponent<NavigationWaypoint>();
        makeStoneBlock.building = destination;
        makeStoneBlock.gather = new ItemType[] { ItemType.STONEBLOCK };
        makeStoneBlock.give = new ItemType[] { ItemType.STONE };
        makeStoneBlock.recipe = MasterRecipe.Instance.StoneBlock;
        makeStoneBlock.fun1 = new instructionFunction((makeStoneBlock.building).MakeRecipe);

        instructions.Add(makeStoneBlock);

        Instruction storeStoneBlock = new Instruction();
        storeStoneBlock.destination = destination.gameObject.GetComponent<NavigationWaypoint>();
        storeStoneBlock.building = destination;
        storeStoneBlock.gather = new ItemType[] { };
        storeStoneBlock.give = new ItemType[] { ItemType.STONEBLOCK };
        storeStoneBlock.fun1 = new instructionFunction((storeStoneBlock.building).StoreItem);
        storeStoneBlock.fun2 = new instructionFunction2((destination).ReleaseJob);

        instructions.Add(storeStoneBlock);

        return instructions;
    }
}
