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
            eat.fun1 = new instructionFunction(((Tavern)eat.building).Eat);

            instructions.Add(eat);
        }
        else if (state == NPCStates.SLEEP)
        {
            Instruction sleep = new Instruction();
            sleep.destination = baseCity.Taverns[0].gameObject.GetComponent<NavigationWaypoint>();
            sleep.building = baseCity.Taverns[0];
            sleep.gather = new ItemType[] { };
            sleep.give = new ItemType[] { };
            sleep.fun1 = new instructionFunction(((Tavern)sleep.building).Sleep);

            instructions.Add(sleep);
        }

        return instructions;
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