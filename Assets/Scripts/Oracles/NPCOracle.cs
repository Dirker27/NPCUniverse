using UnityEngine;
using System.Collections.Generic;
using System;

public class NPCOracle : MonoBehaviour
{

    public Logger logger;
    private bool debug = true;
    public JobOracle jobOracle;

    void Start()
    {
        this.logger = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Logger>();

        logger.Log(debug, "jobOracle is:" + (jobOracle != null));
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
        logger.Log(debug, "sheet is:" + (sheet != null));
        logger.Log(debug, "jobOracle is:" + (jobOracle != null));
        sheet.job = jobOracle.GetJob(sheet);
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
            
            case Jobs.BAKER:
                i = GameObject.FindGameObjectWithTag("GameManager").GetComponent<BakerOracle>().GetInstructions(sheet.baseCity);
                break;

            case Jobs.BREWMASTER:
                i = GameObject.FindGameObjectWithTag("GameManager").GetComponent<BrewMasterOracle>().GetInstructions(sheet.baseCity);
                break;

            case Jobs.COLLIER:
                i = GameObject.FindGameObjectWithTag("GameManager").GetComponent<CollierOracle>().GetInstructions(sheet.baseCity);
                break;

            case Jobs.FARMER:
                i = GameObject.FindGameObjectWithTag("GameManager").GetComponent<FarmOracle>().GetInstructions(sheet.baseCity);
                break;

            case Jobs.FISHERMAN:
                i = GameObject.FindGameObjectWithTag("GameManager").GetComponent<FishermanOracle>().GetInstructions(sheet.baseCity);
                break;

            default:
                //throw some error
                logger.Log(debug, "No job figure out why:" + sheet.job);
                break;
        }
         
        return i;
    }

    public TradeCity WhereShouldBaseCityBe()
    {
        GameObject[] cities = new GameObject[] {};
        while (cities.Length == 0)
        {
            cities = GameObject.FindGameObjectsWithTag("TradeCity");
        }
        return cities[0].GetComponent<TradeCity>();

    }
}
