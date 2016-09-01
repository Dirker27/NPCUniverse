using UnityEngine;
using System.Collections.Generic;

public class WeaponSmithOracle : MonoBehaviour
{
    public List<Instruction> GetInstructions(TradeCity currentCity)
    {
        List<Instruction> instructions = new List<Instruction>();

        Instruction getBar = new Instruction();
        getBar.destination = currentCity.Foundries[0].gameObject.GetComponent<NavigationWaypoint>();
        getBar.building = currentCity.Foundries[0];
        getBar.gather = new ItemType[] { ItemType.BAR };
        getBar.give = new ItemType[] { };
        getBar.fun1 = new instructionFunction((getBar.building).GetItem);

        instructions.Add(getBar);

        Instruction makeWeapon = new Instruction();
        makeWeapon.destination = currentCity.Smithies[0].gameObject.GetComponent<NavigationWaypoint>();
        makeWeapon.building = currentCity.Smithies[0];
        makeWeapon.gather = new ItemType[] { ItemType.WEAPON };
        makeWeapon.give = new ItemType[] { ItemType.BAR };
        makeWeapon.recipe = MasterRecipe.Instance.Weapon;
        makeWeapon.fun1 = new instructionFunction((makeWeapon.building).MakeRecipe);

        instructions.Add(makeWeapon);

        Instruction storeWeapon = new Instruction();
        storeWeapon.destination = currentCity.Smithies[0].gameObject.GetComponent<NavigationWaypoint>();
        storeWeapon.building = currentCity.Smithies[0];
        storeWeapon.gather = new ItemType[] { };
        storeWeapon.give = new ItemType[] { ItemType.WEAPON };
        storeWeapon.fun1 = new instructionFunction((storeWeapon.building).StoreItem);

        instructions.Add(storeWeapon);

        return instructions;
    }
}
