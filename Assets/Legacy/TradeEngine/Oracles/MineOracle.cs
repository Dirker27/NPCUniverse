using UnityEngine;
using System.Collections.Generic;

public class MineOracle
{
    private bool Ore = true;
    private bool Stone = false;

    public List<Instruction> GetInstructions(CharacterSheet sheet)
    {
        List<Instruction> instructions = new List<Instruction>();

        Instruction getRock = new Instruction();
        Mine destination = null;
        foreach (Mine mine in sheet.baseCity.Mines)
        {
            if (mine.workers.Contains(sheet))
            {
                destination = mine;
                break;
            }
        }

        if (destination == null)
        {
            foreach (Mine mine in sheet.baseCity.Mines)
            {
                if (mine.CurrentPositions[Jobs.MINER] > 0)
                {
                    destination = mine;
                    mine.workers.Add(sheet);
                    mine.CurrentPositions[Jobs.MINER]--;
                    break;
                }
            }
        }
        getRock.destination = destination.gameObject.GetComponent<NavigationWaypoint>();
        getRock.building = destination;
        getRock.give = new ItemType[] { };
        getRock.fun1 = new instructionFunction((getRock.building).MakeRecipe);


        Instruction storeRock = new Instruction();
        storeRock.destination = sheet.baseCity.OreShops[0].gameObject.GetComponent<NavigationWaypoint>();
        storeRock.building = sheet.baseCity.OreShops[0];
        storeRock.gather = new ItemType[] { };
        storeRock.fun1 = new instructionFunction((storeRock.building).StoreItem);
        storeRock.fun2 = new instructionFunction2((destination).ReleaseJob);
        if (Stone)
        {

            getRock.gather = new ItemType[] { ItemType.STONE };
            storeRock.give = new ItemType[] { ItemType.STONE };
            getRock.recipe = MasterRecipe.Instance.Stone;
            Stone = false;
            Ore = true;
        }
        else if (Ore)
        {

            getRock.gather = new ItemType[] { ItemType.ORE };
            storeRock.give = new ItemType[] { ItemType.ORE };
            getRock.recipe = MasterRecipe.Instance.Ore;
            Ore = false;
            Stone = true;
        }

        instructions.Add(getRock);

        instructions.Add(storeRock);

        return instructions;
    }
}
