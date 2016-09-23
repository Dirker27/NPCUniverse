using UnityEngine;
using System.Collections.Generic;

public class CollierOracle
{
    public List<Instruction> GetInstructions(CharacterSheet sheet)
    {
        List<Instruction> instructions = new List<Instruction>();

        Instruction getFirewood = new Instruction();
        getFirewood.destination = sheet.baseCity.WoodCuts[0].gameObject.GetComponent<NavigationWaypoint>();
        getFirewood.building = sheet.baseCity.WoodCuts[0];
        getFirewood.gather = new ItemType[] { ItemType.FIREWOOD };
        getFirewood.give = new ItemType[] { };
        getFirewood.fun1 = new instructionFunction((getFirewood.building).GetItem);

        instructions.Add(getFirewood);

        Instruction getCharcoal = new Instruction();
        CharcoalPit destination = null;
        foreach (CharcoalPit charcoalPit in sheet.baseCity.CharcoalPits)
        {
            if (charcoalPit.workers.Contains(sheet))
            {
                destination = charcoalPit;
                break;
            }
        }

        if (destination == null)
        {
            foreach (CharcoalPit charcoalPit in sheet.baseCity.CharcoalPits)
            {
                if (charcoalPit.CurrentPositions[Jobs.COLLIER] > 0)
                {
                    destination = charcoalPit;
                    charcoalPit.workers.Add(sheet);
                    charcoalPit.CurrentPositions[Jobs.COLLIER]--;
                    break;
                }
            }
        }
        getCharcoal.destination = destination.gameObject.GetComponent<NavigationWaypoint>();
        getCharcoal.building = destination;
        getCharcoal.gather = new ItemType[] { ItemType.CHARCOAL };
        getCharcoal.give = new ItemType[] { ItemType.FIREWOOD };
        getCharcoal.recipe = MasterRecipe.Instance.Charcoal;
        getCharcoal.fun1 = new instructionFunction((getCharcoal.building).MakeRecipe);

        instructions.Add(getCharcoal);

        Instruction storeCharcoal = new Instruction();
        storeCharcoal.destination = destination.gameObject.GetComponent<NavigationWaypoint>();
        storeCharcoal.building = destination;
        storeCharcoal.gather = new ItemType[] { };
        storeCharcoal.give = new ItemType[] { ItemType.CHARCOAL };
        storeCharcoal.fun1 = new instructionFunction((storeCharcoal.building).StoreItem);
        storeCharcoal.fun2 = new instructionFunction2((destination).ReleaseJob);

        instructions.Add(storeCharcoal);

        return instructions;
    }
}
