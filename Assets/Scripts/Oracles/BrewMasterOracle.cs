using UnityEngine;
using System.Collections.Generic;

public class BrewMasterOracle : MonoBehaviour
{
    private bool debug = false;

    void Log(string s)
    {
        if (debug)
        {
            Debug.Log("BrewMasterOracle log <" + s + ">");
        }
    }

    public Brewhouse WhereShouldIBrew(TradeCity currentCity)
    {
        return currentCity.Brewhouses[0];
    }

    public Barn WhereShouldIGather(TradeCity currentCity)
    {
        return currentCity.Barns[0];
    }

    public List<Instruction> GetInstructions(TradeCity currentCity)
    {
        List<Instruction> instructions = new List<Instruction>();

        Instruction getBarley = new Instruction();
        getBarley.destination = currentCity.Barns[0].gameObject.GetComponent<NavigationWaypoint>();
        getBarley.building = currentCity.Barns[0];
        getBarley.gather = new ItemType[] { ItemType.BARLEY };
        getBarley.give = new ItemType[] { };
        getBarley.fun1 = new instructionFunction(((Barn)getBarley.building).GetBarley);

        instructions.Add(getBarley);

        Instruction getBeer = new Instruction();
        getBeer.destination = currentCity.Brewhouses[0].gameObject.GetComponent<NavigationWaypoint>();
        getBeer.building = currentCity.Brewhouses[0];
        getBeer.gather = new ItemType[] { ItemType.BEER };
        getBeer.give = new ItemType[] { ItemType.BARLEY };
        getBeer.fun1 = new instructionFunction(((Brewhouse)getBeer.building).MakeBeer);

        instructions.Add(getBeer);

        Instruction storeBeer = new Instruction();
        storeBeer.destination = currentCity.Brewhouses[0].gameObject.GetComponent<NavigationWaypoint>();
        storeBeer.building = currentCity.Brewhouses[0];
        storeBeer.gather = new ItemType[] { };
        storeBeer.give = new ItemType[] { ItemType.BEER };
        storeBeer.fun1 = new instructionFunction(((Brewhouse)storeBeer.building).StoreBeer);

        instructions.Add(storeBeer);

        return instructions;
    }
}
