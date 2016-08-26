using UnityEngine;
using System.Collections.Generic;

public class BakerOracle : MonoBehaviour
{
    private bool debug = false;

    void Log(string s)
    {
        if (debug)
        {
            Debug.Log("BakerOracle log <" + s + ">");
        }
    }

    public Bakery WhereShouldIBake(TradeCity currentCity)
    {
        return currentCity.Bakeries[0];
    }

    public Mill WhereShouldIMill(TradeCity currentCity)
    {
        return currentCity.Mills[0];
    }

    public List<Instruction> GetInstructions(TradeCity currentCity)
    {
        List<Instruction> instructions = new List<Instruction>();

        Instruction getFlour = new Instruction();
        getFlour.destination = currentCity.Mills[0].gameObject.GetComponent<NavigationWaypoint>();
        getFlour.building = currentCity.Mills[0];
        getFlour.gather = new ItemType[] { ItemType.FLOUR };
        getFlour.give = new ItemType[] { };
        getFlour.fun1 = new instructionFunction(((Mill)getFlour.building).GetFlour);

        instructions.Add(getFlour);

        Instruction makeBread = new Instruction();
        makeBread.destination = currentCity.Bakeries[0].gameObject.GetComponent<NavigationWaypoint>();
        makeBread.building = currentCity.Bakeries[0];
        makeBread.gather = new ItemType[] { ItemType.BREAD };
        makeBread.give = new ItemType[] { ItemType.FLOUR };
        makeBread.fun1 = new instructionFunction(((Bakery)makeBread.building).MakeBread);

        instructions.Add(makeBread);

        Instruction storeBread = new Instruction();
        storeBread.destination = currentCity.Bakeries[0].gameObject.GetComponent<NavigationWaypoint>();
        storeBread.building = currentCity.Bakeries[0];
        storeBread.gather = new ItemType[] { };
        storeBread.give = new ItemType[] { ItemType.BREAD };
        storeBread.fun1 = new instructionFunction(((Bakery)storeBread.building).StoreBread);

        instructions.Add(storeBread);

        return instructions;
    }
}
