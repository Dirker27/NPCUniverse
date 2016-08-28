using UnityEngine;
using System.Collections.Generic;

public class FishermanOracle : MonoBehaviour
{
    private bool debug = false;

    void Log(string s)
    {
        if (debug)
        {
            Debug.Log("FishermanOracle log <" + s + ">");
        }
    }

    public Pond WhereShouldIFish(TradeCity currentCity)
    {
        return currentCity.Ponds[0];
    }

    public Barn WhereShouldIStore(TradeCity currentCity)
    {
        return currentCity.Barns[0];
    }

    public List<Instruction> GetInstructions(TradeCity currentCity)
    {
        List<Instruction> instructions = new List<Instruction>();

        Instruction getFish = new Instruction();
        getFish.destination = currentCity.Ponds[0].gameObject.GetComponent<NavigationWaypoint>();
        getFish.building = currentCity.Ponds[0];
        getFish.gather = new ItemType[] { ItemType.FISH };
        getFish.give = new ItemType[] { };
        getFish.fun1 = new instructionFunction(((Pond)getFish.building).GetFish);

        instructions.Add(getFish);

        Instruction storeFish = new Instruction();
        storeFish.destination = currentCity.Barns[0].gameObject.GetComponent<NavigationWaypoint>();
        storeFish.building = currentCity.Barns[0];
        storeFish.gather = new ItemType[] { };
        storeFish.give = new ItemType[] { ItemType.FISH };
        storeFish.fun1 = new instructionFunction(((Barn)storeFish.building).StoreFish);

        instructions.Add(storeFish);

        return instructions;
    }
}
