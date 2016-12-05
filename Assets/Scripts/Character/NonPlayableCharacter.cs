using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NonPlayableCharacter : MonoBehaviour
{
    public Logger logger;
    public bool debug = true;

    public CharacterSheet sheet;
    
    // Use this for initialization
    public virtual void Start() 
    {
        this.logger = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().GetLogger();
        logger.Log(debug, " Non player character start");
        sheet = new CharacterSheet();

        sheet.inventory = new Inventory();
        sheet.inventory.items = new List<Item>();

        sheet.npcOracle = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().GetNPCOracle();

        logger.Log(debug, " calling where base city should be");
        sheet.baseCity = sheet.npcOracle.WhereShouldBaseCityBe();

        logger.Log(debug, "city:" + sheet.baseCity);
        
        sheet.health = 100;
        sheet.hunger = 100;
        sheet.energy = 100;
        //InvokeRepeating("TimePasses", 500, 500);
	}

    void Update()
    {
        logger.Log(debug, "npc update called");
        if (sheet.baseCity == null)
        {
            sheet.baseCity = sheet.npcOracle.WhereShouldBaseCityBe();
            logger.Log(debug, "new city:" + sheet.baseCity);
        }
    }

    void TimePasses()
    {
        if (sheet.hunger > 0)
        {
            sheet.hunger -= 10;
        }
        if (sheet.energy > 0)
        {
            sheet.energy -= 5;   
        }
    }

    public List<Instruction> NPCInstructions(CharacterSheet sheet)
    {
        List<Instruction> instructions = new List<Instruction>();

        if (sheet.currentState == NPCStates.EAT)
        {
            Instruction eat = new Instruction();
            eat.destination = sheet.baseCity.Taverns[0].gameObject.GetComponent<NavigationWaypoint>();
            eat.building = sheet.baseCity.Taverns[0];
            eat.gather = new ItemType[] { ItemType.MEAL };
            eat.give = new ItemType[] { };
            eat.fun1 = new instructionFunction(((Tavern)eat.building).Eat);

            instructions.Add(eat);
        }
        else if (sheet.currentState == NPCStates.SLEEP)
        {
            Instruction sleep = new Instruction();
            sleep.destination = sheet.baseCity.Taverns[0].gameObject.GetComponent<NavigationWaypoint>();
            sleep.building = sheet.baseCity.Taverns[0];
            sleep.gather = new ItemType[] { };
            sleep.give = new ItemType[] { };
            sleep.fun1 = new instructionFunction(((Tavern)sleep.building).Sleep);

            instructions.Add(sleep);
        }

        return instructions;
    }

    IEnumerator Sleep(int seconds) {
        yield return new WaitForSeconds(seconds);
    }
}