using UnityEngine;
using System.Collections.Generic;

public class MineOracle
{
    public Logger logger;
    private bool debug = false;

    private bool Ore = true;
    private bool Stone = false;
    void Start()
    {
        this.logger = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Logger>();
    }

    public List<Instruction> GetInstructions(TradeCity currentCity)
    {
        List<Instruction> instructions = new List<Instruction>();

        Instruction getRock = new Instruction();
        getRock.destination = currentCity.Mines[0].gameObject.GetComponent<NavigationWaypoint>();
        getRock.building = currentCity.Mines[0];
        getRock.give = new ItemType[] { };
        getRock.fun1 = new instructionFunction((getRock.building).MakeRecipe);


        Instruction storeRock = new Instruction();
        storeRock.destination = currentCity.OreShops[0].gameObject.GetComponent<NavigationWaypoint>();
        storeRock.building = currentCity.OreShops[0];
        storeRock.gather = new ItemType[] { };
        storeRock.fun1 = new instructionFunction((storeRock.building).StoreItem);

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
