using UnityEngine;
using System.Collections.Generic;
using System;

public class NPCOracle
{

    public Logger logger;
    private bool debug = false;
    public JobOracle jobOracle;

    public void Start()
    {
        this.logger = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Logger>();
        this.jobOracle = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().GetJobOracle();
        logger.Log(debug, "jobOracle is:" + (jobOracle != null));
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
                i = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().GetArmorSmithOracle().GetInstructions(sheet.baseCity);
                break;
            
            case Jobs.BAKER:
                i = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().GetBakerOracle().GetInstructions(sheet.baseCity);
                break;

            case Jobs.BREWMASTER:
                i = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().GetBrewMasterOracle().GetInstructions(sheet.baseCity);
                break;

            case Jobs.COLLIER:
                i = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().GetCollierOracle().GetInstructions(sheet.baseCity);
                break;

            case Jobs.FARMER:
                i = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().GetFarmOracle().GetInstructions(sheet.baseCity);
                break;

            case Jobs.FISHERMAN:
                i = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().GetFishermanOracle().GetInstructions(sheet.baseCity);
                break;

            case Jobs.FLETCHER:
                i = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().GetFletcherOracle().GetInstructions(sheet.baseCity);
                break;

            case Jobs.FORESTER:
                i = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().GetForesterOracle().GetInstructions(sheet.baseCity);
                break;
            
            case Jobs.SMITH:
                i = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().GetFoundryOracle().GetInstructions(sheet.baseCity);
                break;

            case Jobs.HUNTER:
                i = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().GetHunterOracle().GetInstructions(sheet.baseCity);
                break;

            case Jobs.INNKEEPER:
                i = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().GetInnKeeperOracle().GetInstructions(sheet.baseCity);
                break;

            case Jobs.MILLER:
                i = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().GetMillOracle().GetInstructions(sheet.baseCity);
                break;

            case Jobs.MINER:
                i = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().GetMineOracle().GetInstructions(sheet.baseCity);
                break;

            case Jobs.QUATERMASTER:
                i = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().GetQuaterMasterOracle().GetInstructions(sheet.baseCity);
                break;

            case Jobs.SAWWORKER:
                i = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().GetSawWorkerOracle().GetInstructions(sheet.baseCity);
                break;

            case Jobs.STONECUTTER:
                i = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().GetStoneCutterOracle().GetInstructions(sheet.baseCity);
                break;

            case Jobs.TOOLSMITH:
                i = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().GetToolSmithOracle().GetInstructions(sheet.baseCity);
                break;

            case Jobs.WEAPONSMITH:
                i = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().GetWeaponSmithOracle().GetInstructions(sheet.baseCity);
                break;

            case Jobs.WOODCUTER:
                i = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().GetWoodCuterOracle().GetInstructions(sheet.baseCity);
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
