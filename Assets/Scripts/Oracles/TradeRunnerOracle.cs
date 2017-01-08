using UnityEngine;
using System.Collections.Generic;

public class TradeRunnerOracle
{

    public List<Instruction> GetInstructions(CharacterSheet sheet)
    {
        List<Instruction> instructions = new List<Instruction>();


        //for building in town
        // go to building
        // get items stored there
        // leave
        //after for
        // go to storage
        // put all items into storage

        if (sheet.baseCity.LogStores.Count > 0)
        {
            Instruction getLog = new Instruction();
            getLog.destination = sheet.baseCity.LogStores[0].gameObject.GetComponent<NavigationWaypoint>();
            getLog.building = sheet.baseCity.LogStores[0];
            getLog.gather = new ItemType[] { ItemType.LOG };
            getLog.give = new ItemType[] { };
            getLog.fun1 = new instructionFunction(((LogStore)getLog.building).GetItem);

            instructions.Add(getLog);
        }
        
        if (sheet.baseCity.Foundries.Count > 0)
        {
            Instruction getBar = new Instruction();
            getBar.destination = sheet.baseCity.Foundries[0].gameObject.GetComponent<NavigationWaypoint>();
            getBar.building = sheet.baseCity.Foundries[0];
            getBar.gather = new ItemType[] { ItemType.BAR };
            getBar.give = new ItemType[] { };
            getBar.fun1 = new instructionFunction((getBar.building).GetItem);

            instructions.Add(getBar);
        }

        if (sheet.baseCity.OreShops.Count > 0)
        {
            Instruction getStone = new Instruction();
            getStone.destination = sheet.baseCity.OreShops[0].gameObject.GetComponent<NavigationWaypoint>();
            getStone.building = sheet.baseCity.OreShops[0];
            getStone.gather = new ItemType[] { ItemType.STONE };
            getStone.give = new ItemType[] { };
            getStone.fun1 = new instructionFunction((getStone.building).GetItem);

            instructions.Add(getStone);

            Instruction getOre = new Instruction();
            getOre.destination = sheet.baseCity.OreShops[0].gameObject.GetComponent<NavigationWaypoint>();
            getOre.building = sheet.baseCity.OreShops[0];
            getOre.gather = new ItemType[] { ItemType.ORE };
            getOre.give = new ItemType[] { };
            getOre.fun1 = new instructionFunction((getOre.building).GetItem);

            instructions.Add(getOre);
        }
       
        if (sheet.baseCity.Smithies.Count > 0)
        {
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
        }

        if (sheet.baseCity.Barns.Count > 0)
        {
            Instruction getWheat = new Instruction();
            getWheat.destination = sheet.baseCity.Barns[0].gameObject.GetComponent<NavigationWaypoint>();
            getWheat.building = sheet.baseCity.Barns[0];
            getWheat.gather = new ItemType[] { ItemType.WHEAT };
            getWheat.give = new ItemType[] { };
            getWheat.fun1 = new instructionFunction((getWheat.building).GetItem);

            instructions.Add(getWheat);

            Instruction getFish = new Instruction();
            getFish.destination = sheet.baseCity.Barns[0].gameObject.GetComponent<NavigationWaypoint>();
            getFish.building = sheet.baseCity.Barns[0];
            getFish.gather = new ItemType[] { ItemType.FISH };
            getFish.give = new ItemType[] { };
            getFish.fun1 = new instructionFunction((getFish.building).GetItem);

            instructions.Add(getFish);

            Instruction getBarley = new Instruction();
            getBarley.destination = sheet.baseCity.Barns[0].gameObject.GetComponent<NavigationWaypoint>();
            getBarley.building = sheet.baseCity.Barns[0];
            getBarley.gather = new ItemType[] { ItemType.BARLEY };
            getBarley.give = new ItemType[] { };
            getBarley.fun1 = new instructionFunction((getBarley.building).GetItem);

            instructions.Add(getBarley);
        }
        
        if (sheet.baseCity.Brewhouses.Count > 0)
        {
            Instruction getBeer = new Instruction();
            getBeer.destination = sheet.baseCity.Brewhouses[0].gameObject.GetComponent<NavigationWaypoint>();
            getBeer.building = sheet.baseCity.Brewhouses[0];
            getBeer.gather = new ItemType[] { ItemType.BEER };
            getBeer.give = new ItemType[] { };
            getBeer.fun1 = new instructionFunction((getBeer.building).GetItem);

            instructions.Add(getBeer);
        }
        
        if (sheet.baseCity.Bakeries.Count > 0)
        {
            Instruction getBread = new Instruction();
            getBread.destination = sheet.baseCity.Bakeries[0].gameObject.GetComponent<NavigationWaypoint>();
            getBread.building = sheet.baseCity.Bakeries[0];
            getBread.gather = new ItemType[] { ItemType.BREAD };
            getBread.give = new ItemType[] { };
            getBread.fun1 = new instructionFunction((getBread.building).GetItem);

            instructions.Add(getBread);
        }
        
        if (sheet.baseCity.BowShops.Count > 0)
        {
            Instruction getBow = new Instruction();
            getBow.destination = sheet.baseCity.BowShops[0].gameObject.GetComponent<NavigationWaypoint>();
            getBow.building = sheet.baseCity.BowShops[0];
            getBow.gather = new ItemType[] { ItemType.BOW };
            getBow.give = new ItemType[] { };
            getBow.fun1 = new instructionFunction((getBow.building).GetItem);

            instructions.Add(getBow);

            Instruction getArrow = new Instruction();
            getArrow.destination = sheet.baseCity.BowShops[0].gameObject.GetComponent<NavigationWaypoint>();
            getArrow.building = sheet.baseCity.BowShops[0];
            getArrow.gather = new ItemType[] { ItemType.ARROW };
            getArrow.give = new ItemType[] { };
            getArrow.fun1 = new instructionFunction((getArrow.building).GetItem);

            instructions.Add(getArrow);
        }
        
        if (sheet.baseCity.WoodCuts.Count > 0)
        {
            Instruction getFirewood = new Instruction();
            getFirewood.destination = sheet.baseCity.WoodCuts[0].gameObject.GetComponent<NavigationWaypoint>();
            getFirewood.building = sheet.baseCity.WoodCuts[0];
            getFirewood.gather = new ItemType[] { ItemType.FIREWOOD };
            getFirewood.give = new ItemType[] { };
            getFirewood.fun1 = new instructionFunction((getFirewood.building).GetItem);

            instructions.Add(getFirewood);
        }
        
        if (sheet.baseCity.Mills.Count > 0)
        {
            Instruction getFlour = new Instruction();
            getFlour.destination = sheet.baseCity.Mills[0].gameObject.GetComponent<NavigationWaypoint>();
            getFlour.building = sheet.baseCity.Mills[0];
            getFlour.gather = new ItemType[] { ItemType.FLOUR };
            getFlour.give = new ItemType[] { };
            getFlour.fun1 = new instructionFunction((getFlour.building).GetItem);

            instructions.Add(getFlour);
        }
        
        if (sheet.baseCity.TradeHouses.Count > 0)
        {
            Instruction storeLog = new Instruction();
            storeLog.destination = sheet.baseCity.TradeHouses[0].gameObject.GetComponent<NavigationWaypoint>();
            storeLog.building = sheet.baseCity.TradeHouses[0];
            storeLog.gather = new ItemType[] { };
            storeLog.give = new ItemType[] { ItemType.LOG};
            storeLog.fun1 = new instructionFunction((storeLog.building).StoreItem);

            instructions.Add(storeLog);

            Instruction storeBar = new Instruction();
            storeBar.destination = sheet.baseCity.TradeHouses[0].gameObject.GetComponent<NavigationWaypoint>();
            storeBar.building = sheet.baseCity.TradeHouses[0];
            storeBar.gather = new ItemType[] { };
            storeBar.give = new ItemType[] { ItemType.BAR };
            storeBar.fun1 = new instructionFunction((storeBar.building).StoreItem);

            instructions.Add(storeBar);

            Instruction storeStone = new Instruction();
            storeStone.destination = sheet.baseCity.TradeHouses[0].gameObject.GetComponent<NavigationWaypoint>();
            storeStone.building = sheet.baseCity.TradeHouses[0];
            storeStone.gather = new ItemType[] { };
            storeStone.give = new ItemType[] { ItemType.STONE };
            storeStone.fun1 = new instructionFunction((storeStone.building).StoreItem);

            instructions.Add(storeStone);

            Instruction storeOre = new Instruction();
            storeOre.destination = sheet.baseCity.TradeHouses[0].gameObject.GetComponent<NavigationWaypoint>();
            storeOre.building = sheet.baseCity.TradeHouses[0];
            storeOre.gather = new ItemType[] { };
            storeOre.give = new ItemType[] { ItemType.ORE };
            storeOre.fun1 = new instructionFunction((storeOre.building).StoreItem);

            instructions.Add(storeOre);

            Instruction storeArmor = new Instruction();
            storeArmor.destination = sheet.baseCity.TradeHouses[0].gameObject.GetComponent<NavigationWaypoint>();
            storeArmor.building = sheet.baseCity.TradeHouses[0];
            storeArmor.gather = new ItemType[] { };
            storeArmor.give = new ItemType[] { ItemType.ARMOR };
            storeArmor.fun1 = new instructionFunction((storeArmor.building).StoreItem);

            instructions.Add(storeArmor);

            Instruction storeWeapon = new Instruction();
            storeWeapon.destination = sheet.baseCity.TradeHouses[0].gameObject.GetComponent<NavigationWaypoint>();
            storeWeapon.building = sheet.baseCity.TradeHouses[0];
            storeWeapon.gather = new ItemType[] { };
            storeWeapon.give = new ItemType[] { ItemType.WEAPON };
            storeWeapon.fun1 = new instructionFunction((storeWeapon.building).StoreItem);

            instructions.Add(storeWeapon);

            Instruction storeWheat = new Instruction();
            storeWheat.destination = sheet.baseCity.TradeHouses[0].gameObject.GetComponent<NavigationWaypoint>();
            storeWheat.building = sheet.baseCity.TradeHouses[0];
            storeWheat.gather = new ItemType[] { };
            storeWheat.give = new ItemType[] { ItemType.WHEAT };
            storeWheat.fun1 = new instructionFunction((storeWheat.building).StoreItem);

            instructions.Add(storeWheat);

            Instruction storeFish = new Instruction();
            storeFish.destination = sheet.baseCity.TradeHouses[0].gameObject.GetComponent<NavigationWaypoint>();
            storeFish.building = sheet.baseCity.TradeHouses[0];
            storeFish.gather = new ItemType[] { };
            storeFish.give = new ItemType[] { ItemType.FISH };
            storeFish.fun1 = new instructionFunction((storeFish.building).StoreItem);

            instructions.Add(storeFish);

            Instruction storeBarley = new Instruction();
            storeBarley.destination = sheet.baseCity.TradeHouses[0].gameObject.GetComponent<NavigationWaypoint>();
            storeBarley.building = sheet.baseCity.TradeHouses[0];
            storeBarley.gather = new ItemType[] { };
            storeBarley.give = new ItemType[] { ItemType.BARLEY };
            storeBarley.fun1 = new instructionFunction((storeBarley.building).StoreItem);

            instructions.Add(storeBarley);

            Instruction storeBeer = new Instruction();
            storeBeer.destination = sheet.baseCity.TradeHouses[0].gameObject.GetComponent<NavigationWaypoint>();
            storeBeer.building = sheet.baseCity.TradeHouses[0];
            storeBeer.gather = new ItemType[] { };
            storeBeer.give = new ItemType[] { ItemType.BEER };
            storeBeer.fun1 = new instructionFunction((storeBeer.building).StoreItem);

            instructions.Add(storeBeer);

            Instruction storeBread = new Instruction();
            storeBread.destination = sheet.baseCity.TradeHouses[0].gameObject.GetComponent<NavigationWaypoint>();
            storeBread.building = sheet.baseCity.TradeHouses[0];
            storeBread.gather = new ItemType[] { };
            storeBread.give = new ItemType[] { ItemType.BREAD };
            storeBread.fun1 = new instructionFunction((storeBread.building).StoreItem);

            instructions.Add(storeBread);

            Instruction storeBow = new Instruction();
            storeBow.destination = sheet.baseCity.TradeHouses[0].gameObject.GetComponent<NavigationWaypoint>();
            storeBow.building = sheet.baseCity.TradeHouses[0];
            storeBow.gather = new ItemType[] { };
            storeBow.give = new ItemType[] { ItemType.BOW };
            storeBow.fun1 = new instructionFunction((storeBow.building).StoreItem);

            instructions.Add(storeBow);

            Instruction storeArrow = new Instruction();
            storeArrow.destination = sheet.baseCity.TradeHouses[0].gameObject.GetComponent<NavigationWaypoint>();
            storeArrow.building = sheet.baseCity.TradeHouses[0];
            storeArrow.gather = new ItemType[] { };
            storeArrow.give = new ItemType[] { ItemType.ARROW };
            storeArrow.fun1 = new instructionFunction((storeArrow.building).StoreItem);

            instructions.Add(storeArrow);

            Instruction storeFirewood = new Instruction();
            storeFirewood.destination = sheet.baseCity.TradeHouses[0].gameObject.GetComponent<NavigationWaypoint>();
            storeFirewood.building = sheet.baseCity.TradeHouses[0];
            storeFirewood.gather = new ItemType[] { };
            storeFirewood.give = new ItemType[] { ItemType.FIREWOOD };
            storeFirewood.fun1 = new instructionFunction((storeFirewood.building).StoreItem);

            instructions.Add(storeFirewood);

            Instruction storeFlour = new Instruction();
            storeFlour.destination = sheet.baseCity.TradeHouses[0].gameObject.GetComponent<NavigationWaypoint>();
            storeFlour.building = sheet.baseCity.TradeHouses[0];
            storeFlour.gather = new ItemType[] { };
            storeFlour.give = new ItemType[] { ItemType.FLOUR };
            storeFlour.fun1 = new instructionFunction((storeFlour.building).StoreItem);

            instructions.Add(storeFlour);

        }
        return instructions;
    }
}
