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
        this.logger = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Logger>();
        logger.Log(debug, " Non player character start");
        sheet = GameObject.FindGameObjectWithTag("GameManager").AddComponent<CharacterSheet>();

        sheet.inventory = GetComponent<Inventory>();
        sheet.inventory.items = new Dictionary<Item, int>();

        sheet.tradeOracle = GameObject.FindGameObjectWithTag("GameManager").GetComponent<TradeOracle>();
        sheet.npcOracle = GameObject.FindGameObjectWithTag("GameManager").GetComponent<NPCOracle>();

        logger.Log(debug, " calling where base city should be");
        sheet.baseCity = sheet.npcOracle.WhereShouldBaseCityBe();

        logger.Log(debug, "city:" + sheet.baseCity);

        sheet.destinationTaverrn = sheet.npcOracle.WhereShouldISleepAndEat(sheet.baseCity);
        sheet.tavern = sheet.destinationTaverrn.gameObject.GetComponent<NavigationWaypoint>();

        sheet.health = 100;
        sheet.hunger = 100;
        sheet.energy = 100;
        InvokeRepeating("TimePasses", 5, 5);
	}
	
	// Update is called once per frame
    void Update()
    {
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

    public void NPCUpdate(NPCStates state)
    {
        logger.Log(debug, "State was:" + sheet.previousState + " State will be:" + state);
        if (state == NPCStates.SLEEP)
        {
            if (sheet.previousState != NPCStates.SLEEP)
            {
                sheet.previousState = NPCStates.SLEEP;
                GetComponent<CharacterMovement>().destination = sheet.tavern;
                GetComponent<CharacterMovement>().location = null;
            }
            GoingToSleep();
        }
        else if (state == NPCStates.EAT)
        {
            if (sheet.previousState != NPCStates.EAT)
            {
                sheet.previousState = NPCStates.EAT;
                GetComponent<CharacterMovement>().destination = sheet.tavern;
                GetComponent<CharacterMovement>().location = null;
            }
            FindingFood();
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
            eat.Action = "Eat";

            instructions.Add(eat);
        }
        else if (sheet.currentState == NPCStates.SLEEP)
        {
            Instruction sleep = new Instruction();
            sleep.destination = sheet.baseCity.Taverns[0].gameObject.GetComponent<NavigationWaypoint>();
            sleep.building = sheet.baseCity.Taverns[0];
            sleep.gather = new ItemType[] { };
            sleep.give = new ItemType[] { };
            sleep.Action = "Sleep";

            instructions.Add(sleep);
        }

        return instructions;
    }

    void FindingFood()
    {
        logger.Log(debug, "Finding food");
        if (!GetComponent<CharacterMovement>().isInTransit())
        {
            logger.Log(debug, "Not traveling");
            if (GetComponent<CharacterMovement>().location == sheet.tavern)
           {
               logger.Log(debug, "At tavern");
               Item meal = sheet.destinationTaverrn.GetMeal();
               Destroy(meal);
               sheet.hunger = 100;
               GetComponent<CharacterMovement>().destination = sheet.workDestination;
               GetComponent<CharacterMovement>().location = null;
           }
        }
    }

    void GoingToSleep()
    {
        if (!GetComponent<CharacterMovement>().isInTransit())
        {
            if (GetComponent<CharacterMovement>().location == sheet.tavern)
            {
                Sleep(5);
                sheet.energy = 100;
                GetComponent<CharacterMovement>().destination = sheet.workDestination;
                GetComponent<CharacterMovement>().location = null;
            }
        }
    }

    IEnumerator Sleep(int seconds) {
        yield return new WaitForSeconds(seconds);
    }

    public void SetDestinationForWork(NavigationWaypoint destination)
    {
        GetComponent<CharacterMovement>().destination = destination;
        sheet.workDestination = destination;
        GetComponent<CharacterMovement>().location = null;
    }
}