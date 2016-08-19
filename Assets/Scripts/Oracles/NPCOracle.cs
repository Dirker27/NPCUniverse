using UnityEngine;
using System.Collections.Generic;
using System;

public class NPCOracle : MonoBehaviour
{

    public Logger logger;
    private bool debug = true;

    void Start()
    {
        this.logger = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Logger>();
    }
    
    public NPCStates WhatShouldIDo(int hunger, int energy)
    {
        logger.Log(debug, "Hunger is:" + hunger + " energy is:" + energy);
        if (hunger == 0)
        {
            logger.Log(debug, "Returning eat");
            return NPCStates.EAT;
        }
        if (energy == 0)
        {
            logger.Log(debug, "Returning sleep");
            return NPCStates.SLEEP;
        }
        logger.Log(debug, "Returning work");
        return NPCStates.WORK;
    }
    public NPCStates WhatShouldIDo(CharacterSheet sheet)
    {
        logger.Log(debug, "Hunger is:" + sheet.hunger + " energy is:" + sheet.energy);
        if (sheet.hunger == 0)
        {
            logger.Log(debug, "Returning eat");
            return NPCStates.EAT;
        }
        if (sheet.energy == 0)
        {
            logger.Log(debug, "Returning sleep");
            return NPCStates.SLEEP;
        }
        logger.Log(debug, "Returning work");
        return NPCStates.WORK;
    }

    public Tavern WhereShouldISleepAndEat(TradeCity city)
    {
        return city.Taverns[0];
    }
    public void FindAndSetJob(CharacterSheet sheet)
    {
        sheet.job = Jobs.ARMORSMITH;
    }

    public List<Instruction> GetInstruction(CharacterSheet sheet)
    {
        logger.Log(debug, "Getting new instructions");
        if (sheet.job == Jobs.NONE)
        {
            logger.Log(debug, "Finding new job");
            FindAndSetJob(sheet);
        }
        List<Instruction> i = new List<Instruction>();
        switch(sheet.job)
        {
            case Jobs.ARMORSMITH:
                i = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ArmorSmithOracle>().GetInstructions(sheet.baseCity);
                break;
            
            default:
                //throw some error
                logger.Log(debug, "No job figure out why");
                break;
        }
         
        return i;
    }

    public TradeCity WhereShouldBaseCityBe()
    {
        Debug.Log("Finding city");
        GameObject[] cities = new GameObject[] {};
        while (cities.Length == 0)
        {
            cities = GameObject.FindGameObjectsWithTag("TradeCity");
        }
        Debug.Log("Found city:" + cities.Length);
        return cities[0].GetComponent<TradeCity>();

    }
}
