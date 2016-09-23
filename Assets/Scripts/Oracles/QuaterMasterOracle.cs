using UnityEngine;
using System.Collections.Generic;

public class QuaterMasterOracle
{
    public List<Instruction> GetInstructions(CharacterSheet sheet)
    {
        List<Instruction> instructions = new List<Instruction>();

        Instruction getArmor = new Instruction();
        getArmor.destination = sheet.baseCity.Smithies[0].gameObject.GetComponent<NavigationWaypoint>();
        getArmor.building = sheet.baseCity.Smithies[0];
        getArmor.gather = new ItemType[] { ItemType.ARMOR };
        getArmor.give = new ItemType[] { };
        getArmor.fun1 = new instructionFunction((getArmor.building).GetItem);

        instructions.Add(getArmor);

        Instruction getWeapon = new Instruction();
        getWeapon.destination = sheet.baseCity.Smithies[0].gameObject.GetComponent<NavigationWaypoint>();
        getWeapon.building = sheet.baseCity.Smithies[0];
        getWeapon.gather = new ItemType[] { ItemType.WEAPON };
        getWeapon.give = new ItemType[] { };
        getWeapon.fun1 = new instructionFunction((getWeapon.building).GetItem);

        instructions.Add(getWeapon);

        Instruction storeArmor = new Instruction();
        GuildHall destination = null;
        foreach (GuildHall guildHall in sheet.baseCity.GuildHalls)
        {
            if (guildHall.workers.Contains(sheet))
            {
                destination = guildHall;
                break;
            }
        }

        if (destination == null)
        {
            foreach (GuildHall guildHall in sheet.baseCity.GuildHalls)
            {
                if (guildHall.CurrentPositions[Jobs.QUATERMASTER] > 0)
                {
                    destination = guildHall;
                    guildHall.workers.Add(sheet);
                    guildHall.CurrentPositions[Jobs.QUATERMASTER]--;
                    break;
                }
            }
        }
        storeArmor.destination = destination.gameObject.GetComponent<NavigationWaypoint>();
        storeArmor.building = destination;
        storeArmor.gather = new ItemType[] { };
        storeArmor.give = new ItemType[] { ItemType.ARMOR };
        storeArmor.fun1 = new instructionFunction((storeArmor.building).StoreItem);

        instructions.Add(storeArmor);

        Instruction storeWeapon = new Instruction();
        storeWeapon.destination = destination.gameObject.GetComponent<NavigationWaypoint>();
        storeWeapon.building = destination;
        storeWeapon.gather = new ItemType[] { };
        storeWeapon.give = new ItemType[] { ItemType.WEAPON };
        storeWeapon.fun1 = new instructionFunction((storeWeapon.building).StoreItem);
        storeWeapon.fun2 = new instructionFunction2((destination).ReleaseJob);

        instructions.Add(storeWeapon);

        return instructions;
    }
}
