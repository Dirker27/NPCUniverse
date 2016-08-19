using UnityEngine;
using System.Collections.Generic;

public class ArmorSmithOracle : NPCOracle
{
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

        Instruction giveArmor = new Instruction();
        giveArmor.destination = currentCity.Smithies[0].gameObject.GetComponent<NavigationWaypoint>();
        giveArmor.building = currentCity.Smithies[0];
        giveArmor.gather = new ItemType[] { };
        giveArmor.give = new ItemType[] { ItemType.ARMOR };
        giveArmor.Action = "StoreArmor";

        instructions.Add(giveArmor);

        return instructions;
    }
}
