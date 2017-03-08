using UnityEngine;
using System.Collections.Generic;

public class WeaponSmithOracle
{
    public List<Instruction> GetInstructions(CharacterSheet sheet)
    {
        List<Instruction> instructions = new List<Instruction>();

        Instruction getBar = new Instruction();
        getBar.destination = sheet.baseCity.Foundries[0].gameObject.GetComponent<NavigationWaypoint>();
        getBar.building = sheet.baseCity.Foundries[0];
        getBar.gather = new ItemType[] { ItemType.BAR };
        getBar.give = new ItemType[] { };
        getBar.fun1 = new instructionFunction((getBar.building).GetItem);

        instructions.Add(getBar);

        Instruction makeWeapon = new Instruction();
        Smithy destination = null;
        foreach (Smithy smithy in sheet.baseCity.Smithies)
        {
            if (smithy.workers.Contains(sheet))
            {
                destination = smithy;
                break;
            }
        }

        if (destination == null)
        {
            foreach (Smithy smithy in sheet.baseCity.Smithies)
            {
                if (smithy.CurrentPositions[Jobs.WEAPONSMITH] > 0)
                {
                    destination = smithy;
                    smithy.workers.Add(sheet);
                    smithy.CurrentPositions[Jobs.WEAPONSMITH]--;
                    break;
                }
            }
        }
        makeWeapon.destination = destination.gameObject.GetComponent<NavigationWaypoint>();
        makeWeapon.building = destination;
        makeWeapon.gather = new ItemType[] { ItemType.WEAPON };
        makeWeapon.give = new ItemType[] { ItemType.BAR };
        makeWeapon.recipe = MasterRecipe.Instance.Weapon;
        makeWeapon.fun1 = new instructionFunction((makeWeapon.building).MakeRecipe);

        instructions.Add(makeWeapon);

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
