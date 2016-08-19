using UnityEngine;
using System.Collections.Generic;
using System;

public class NPCOracle : MonoBehaviour
{

    public Logger logger;
    private bool debug = true;

    //Figure out why job count logic won't work
    public int jobCount = 1;

    void Start()
    {
        this.logger = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Logger>();
        jobCount = 1;
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

    private int GetAndIncrementJobCount()
    {
        logger.Log(debug, "job count:" + jobCount);
        int toReturn = jobCount;
        jobCount++;
        if (Enum.GetName(typeof(Jobs),jobCount) == null)
        {
            jobCount = 1;
        }
        logger.Log(debug, "Returning for job:" + toReturn);
        return toReturn;
    }
    public void FindAndSetJob(CharacterSheet sheet)
    {
        Jobs job = (Jobs)GetAndIncrementJobCount();
        job = (Jobs)1;
        sheet.job = job;
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
