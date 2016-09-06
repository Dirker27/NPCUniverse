﻿using UnityEngine;
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
    }
}