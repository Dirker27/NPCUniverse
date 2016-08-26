using UnityEngine;
using System.Collections.Generic;

public class ArmorSmithOracle : MonoBehaviour
{
    public Logger logger;
    private bool debug = true;

    void Start()
    {
        this.logger = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Logger>();
    }

    public Smithy WhereShouldISmith(TradeCity currentCity)
    {
        return currentCity.Smithies[0];
    }

    public Foundry WhereShouldIShop(TradeCity currentCity)
    {
        return currentCity.Foundries[0];
    }

    public List<Instruction> GetInstructions(TradeCity currentCity)
    {
        List<Instruction> instructions = new List<Instruction>();

        Instruction getBar = new Instruction();
        getBar.destination = currentCity.Foundries[0].gameObject.GetComponent<NavigationWaypoint>();
        getBar.building = currentCity.Foundries[0];
        getBar.gather = new ItemType[] {ItemType.BAR};
        getBar.give = new ItemType[] {};
        getBar.Action = "GetBar";

        instructions.Add(getBar);

        Instruction getArmor = new Instruction();
        getArmor.destination = currentCity.Smithies[0].gameObject.GetComponent<NavigationWaypoint>();
        getArmor.building = currentCity.Smithies[0];
        getArmor.gather = new ItemType[] { ItemType.ARMOR };
        getArmor.give = new ItemType[] { ItemType.BAR };
        getArmor.Action = "MakeArmor";

        instructions.Add(getArmor);

        Instruction storeArmor = new Instruction();
        storeArmor.destination = currentCity.Smithies[0].gameObject.GetComponent<NavigationWaypoint>();
        storeArmor.building = currentCity.Smithies[0];
        storeArmor.gather = new ItemType[] { };
        storeArmor.give = new ItemType[] { ItemType.ARMOR };
        storeArmor.Action = "StoreArmor";

        instructions.Add(storeArmor);

        return instructions;
    }
}
