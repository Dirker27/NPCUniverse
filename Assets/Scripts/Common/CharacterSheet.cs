using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterSheet : MonoBehaviour
{
    public Logger logger;
    public bool debug = false;

    public int health;
    public int hunger;
    public int energy;


    public TradeOracle tradeOracle;
    public NPCOracle npcOracle;

    public Tavern destinationTaverrn;
    public NavigationWaypoint tavern;

    public TradeCity baseCity;

    public Inventory inventory;

    public NavigationWaypoint workDestination;
    public NPCStates previousState = NPCStates.WORK;
    public NPCStates currentState = NPCStates.WORK;
    public bool destinationIsBaseCity = false;
    public bool destinationIsHome = false;

    public Jobs job = Jobs.NONE;

    // Use this for initialization
    public virtual void Start()
    {
        this.logger = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Logger>();

        logger.Log(debug, "character sheet start");
        //this.inventory = GetComponent<Inventory>();
        //this.inventory.items = new Dictionary<Item, int>();

        this.tradeOracle = GameObject.FindGameObjectWithTag("GameManager").GetComponent<TradeOracle>();
        this.npcOracle = GameObject.FindGameObjectWithTag("GameManager").GetComponent<NPCOracle>();

        baseCity = npcOracle.WhereShouldBaseCityBe();

        destinationTaverrn = npcOracle.WhereShouldISleepAndEat(baseCity);
        tavern = destinationTaverrn.gameObject.GetComponent<NavigationWaypoint>();

        health = 100;
        hunger = 100;
        energy = 100;
        InvokeRepeating("TimePasses", 5, 5);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void TimePasses()
    {
        if (hunger > 0)
        {
            hunger -= 10;
        }
        if (energy > 0)
        {
            energy -= 5;
        }
    }

    public void NPCUpdate(NPCStates state)
    {
        logger.Log(debug, "State was:" + previousState + " State will be:" + state);
        if (state == NPCStates.SLEEP)
        {
            if (previousState != NPCStates.SLEEP)
            {
                previousState = NPCStates.SLEEP;
                GetComponent<CharacterMovement>().destination = tavern;
                GetComponent<CharacterMovement>().location = null;
            }
            GoingToSleep();
        }
        else if (state == NPCStates.EAT)
        {
            if (previousState != NPCStates.EAT)
            {
                previousState = NPCStates.EAT;
                GetComponent<CharacterMovement>().destination = tavern;
                GetComponent<CharacterMovement>().location = null;
            }
            FindingFood();
        }
    }

    public List<Instruction> NPCInstructions(NPCStates state)
    {
        List<Instruction> instructions = new List<Instruction>();

        if (state == NPCStates.EAT)
        {
            Instruction eat = new Instruction();
            eat.destination = baseCity.Taverns[0].gameObject.GetComponent<NavigationWaypoint>();
            eat.building = baseCity.Taverns[0];
            eat.gather = new ItemType[] { };
            eat.give = new ItemType[] { };
            eat.Action = "Eat";

            instructions.Add(eat);
        }
        else if (state == NPCStates.SLEEP)
        {
            Instruction sleep = new Instruction();
            sleep.destination = baseCity.Taverns[0].gameObject.GetComponent<NavigationWaypoint>();
            sleep.building = baseCity.Taverns[0];
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
            if (GetComponent<CharacterMovement>().location == tavern)
            {
                logger.Log(debug, "At tavern");
                Item meal = destinationTaverrn.GetMeal();
                Destroy(meal);
                hunger = 100;
                GetComponent<CharacterMovement>().destination = workDestination;
                GetComponent<CharacterMovement>().location = null;
            }
        }
    }

    void GoingToSleep()
    {
        if (!GetComponent<CharacterMovement>().isInTransit())
        {
            if (GetComponent<CharacterMovement>().location == tavern)
            {
                Sleep(5);
                energy = 100;
                GetComponent<CharacterMovement>().destination = workDestination;
                GetComponent<CharacterMovement>().location = null;
            }
        }
    }

    IEnumerator Sleep(int seconds)
    {
        yield return new WaitForSeconds(seconds);
    }

    public void SetDestinationForWork(NavigationWaypoint destination)
    {
        GetComponent<CharacterMovement>().destination = destination;
        workDestination = destination;
        GetComponent<CharacterMovement>().location = null;
    }
}