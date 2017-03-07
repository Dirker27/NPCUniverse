using UnityEngine;
using System.Collections.Generic;

public class InnKeeperOracle
{
    public List<Instruction> GetInstructions(CharacterSheet sheet)
    {
        List<Instruction> instructions = new List<Instruction>();

        Instruction getBeer = new Instruction();
        if (sheet.baseCity.Brewhouses.Count > 0)
        {
            getBeer.destination = sheet.baseCity.Brewhouses[0].gameObject.GetComponent<NavigationWaypoint>();
            getBeer.building = sheet.baseCity.Brewhouses[0];
            getBeer.gather = new ItemType[] { ItemType.BEER };
            getBeer.give = new ItemType[] { };
            getBeer.fun1 = new instructionFunction((getBeer.building).GetItem);

            instructions.Add(getBeer);
        }
        else
        {
            getBeer.destination = sheet.baseCity.TradeHouses[0].gameObject.GetComponent<NavigationWaypoint>();
            getBeer.building = sheet.baseCity.TradeHouses[0];
            getBeer.gather = new ItemType[] { ItemType.BEER };
            getBeer.give = new ItemType[] { };
            getBeer.fun1 = new instructionFunction((getBeer.building).GetItem);

            instructions.Add(getBeer);
        }
        

        Instruction getFish = new Instruction();
        if (sheet.baseCity.Ponds.Count > 0)
        {
            getFish.destination = sheet.baseCity.Barns[0].gameObject.GetComponent<NavigationWaypoint>();
            getFish.building = sheet.baseCity.Barns[0];
            getFish.gather = new ItemType[] { ItemType.FISH };
            getFish.give = new ItemType[] { };
            getFish.fun1 = new instructionFunction((getFish.building).GetItem);

            instructions.Add(getFish);
        }
        else
        {
            getFish.destination = sheet.baseCity.TradeHouses[0].gameObject.GetComponent<NavigationWaypoint>();
            getFish.building = sheet.baseCity.TradeHouses[0];
            getFish.gather = new ItemType[] { ItemType.FISH };
            getFish.give = new ItemType[] { };
            getFish.fun1 = new instructionFunction((getFish.building).GetItem);

            instructions.Add(getFish);
        }



        Instruction getBread = new Instruction();
        if (sheet.baseCity.Bakeries.Count > 0)
        {
            getBread.destination = sheet.baseCity.Bakeries[0].gameObject.GetComponent<NavigationWaypoint>();
            getBread.building = sheet.baseCity.Bakeries[0];
            getBread.gather = new ItemType[] { ItemType.BREAD };
            getBread.give = new ItemType[] { };
            getBread.fun1 = new instructionFunction((getBread.building).GetItem);

            instructions.Add(getBread);
        }
        else
        {
            getBread.destination = sheet.baseCity.TradeHouses[0].gameObject.GetComponent<NavigationWaypoint>();
            getBread.building = sheet.baseCity.TradeHouses[0];
            getBread.gather = new ItemType[] { ItemType.BREAD };
            getBread.give = new ItemType[] { };
            getBread.fun1 = new instructionFunction((getBread.building).GetItem);

            instructions.Add(getBread);
        }


        Instruction makeMeal = new Instruction();
        Tavern destination = null;
        foreach (Tavern tavern in sheet.baseCity.Taverns)
        {
            if (tavern.workers.Contains(sheet))
            {
                destination = tavern;
                break;
            }
        }

        if (destination == null)
        {
            foreach (Tavern tavern in sheet.baseCity.Taverns)
            {
                if (tavern.CurrentPositions[Jobs.INNKEEPER] > 0)
                {
                    destination = tavern;
                    tavern.workers.Add(sheet);
                    tavern.CurrentPositions[Jobs.INNKEEPER]--;
                    break;
                }
            }
        }
        makeMeal.destination = destination.gameObject.GetComponent<NavigationWaypoint>();
        makeMeal.building = destination;
        makeMeal.gather = new ItemType[] { ItemType.MEAL };
        makeMeal.give = new ItemType[] { ItemType.BEER, ItemType.BREAD, ItemType.FISH };
        makeMeal.recipe = MasterRecipe.Instance.Meal;
        makeMeal.fun1 = new instructionFunction((makeMeal.building).MakeRecipe);

        instructions.Add(makeMeal);

        Instruction storeMeal = new Instruction();
        storeMeal.destination = destination.gameObject.GetComponent<NavigationWaypoint>();
        storeMeal.building = destination;
        storeMeal.gather = new ItemType[] { };
        storeMeal.give = new ItemType[] { ItemType.MEAL };
        storeMeal.fun1 = new instructionFunction((storeMeal.building).StoreItem);
        storeMeal.fun2 = new instructionFunction2((destination).ReleaseJob);
        instructions.Add(storeMeal);

        return instructions;
    }
}
