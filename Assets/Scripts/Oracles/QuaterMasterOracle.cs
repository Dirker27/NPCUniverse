using UnityEngine;
using System.Collections.Generic;

public class QuaterMasterOracle
{
    public List<Instruction> GetInstructions(TradeCity currentCity)
    {
        List<Instruction> instructions = new List<Instruction>();

        Instruction getArmor = new Instruction();
        getArmor.destination = currentCity.Smithies[0].gameObject.GetComponent<NavigationWaypoint>();
        getArmor.building = currentCity.Smithies[0];
        getArmor.gather = new ItemType[] { ItemType.ARMOR };
        getArmor.give = new ItemType[] { };
        getArmor.fun1 = new instructionFunction((getArmor.building).GetItem);

        instructions.Add(getArmor);

        Instruction getWeapon = new Instruction();
        getWeapon.destination = currentCity.Smithies[0].gameObject.GetComponent<NavigationWaypoint>();
        getWeapon.building = currentCity.Smithies[0];
        getWeapon.gather = new ItemType[] { ItemType.WEAPON };
        getWeapon.give = new ItemType[] { };
        getWeapon.fun1 = new instructionFunction((getWeapon.building).GetItem);

        instructions.Add(getWeapon);

        Instruction storeArmor = new Instruction();
        storeArmor.destination = currentCity.GuildHalls[0].gameObject.GetComponent<NavigationWaypoint>();
        storeArmor.building = currentCity.GuildHalls[0];
        storeArmor.gather = new ItemType[] { };
        storeArmor.give = new ItemType[] { ItemType.ARMOR };
        storeArmor.fun1 = new instructionFunction((storeArmor.building).StoreItem);

        instructions.Add(storeArmor);

        Instruction storeWeapon = new Instruction();
        storeWeapon.destination = currentCity.GuildHalls[0].gameObject.GetComponent<NavigationWaypoint>();
        storeWeapon.building = currentCity.GuildHalls[0];
        storeWeapon.gather = new ItemType[] { };
        storeWeapon.give = new ItemType[] { ItemType.WEAPON };
        storeWeapon.fun1 = new instructionFunction((storeWeapon.building).StoreItem);

        instructions.Add(storeArmor);

        return instructions;
    }
}
