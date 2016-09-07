using UnityEngine;
using System.Collections.Generic;
using System;

public class NPCOracle : MonoBehaviour
{

    public Logger logger;
    private bool debug = false;
    public JobOracle jobOracle;

    void Start()
    {
        this.logger = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Logger>();

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

            case Jobs.FLETCHER:
                i = GameObject.FindGameObjectWithTag("GameManager").GetComponent<FletcherOracle>().GetInstructions(sheet.baseCity);
                break;

            case Jobs.FORESTER:
                i = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ForesterOracle>().GetInstructions(sheet.baseCity);
                break;
            
            case Jobs.SMITH:
                i = GameObject.FindGameObjectWithTag("GameManager").GetComponent<FoundryOracle>().GetInstructions(sheet.baseCity);
                break;

            case Jobs.HUNTER:
                i = GameObject.FindGameObjectWithTag("GameManager").GetComponent<HunterOracle>().GetInstructions(sheet.baseCity);
                break;

            case Jobs.INNKEEPER:
                i = GameObject.FindGameObjectWithTag("GameManager").GetComponent<InnKeeperOracle>().GetInstructions(sheet.baseCity);
                break;

            case Jobs.MILLER:
                i = GameObject.FindGameObjectWithTag("GameManager").GetComponent<MillOracle>().GetInstructions(sheet.baseCity);
                break;

            case Jobs.MINER:
                i = GameObject.FindGameObjectWithTag("GameManager").GetComponent<MineOracle>().GetInstructions(sheet.baseCity);
                break;

            case Jobs.QUATERMASTER:
                i = GameObject.FindGameObjectWithTag("GameManager").GetComponent<QuaterMasterOracle>().GetInstructions(sheet.baseCity);
                break;

            case Jobs.SAWWORKER:
                i = GameObject.FindGameObjectWithTag("GameManager").GetComponent<SawWorkerOracle>().GetInstructions(sheet.baseCity);
                break;

            case Jobs.STONECUTTER:
                i = GameObject.FindGameObjectWithTag("GameManager").GetComponent<StoneCutterOracle>().GetInstructions(sheet.baseCity);
                break;

            case Jobs.TOOLSMITH:
                i = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ToolSmithOracle>().GetInstructions(sheet.baseCity);
                break;

            case Jobs.WEAPONSMITH:
                i = GameObject.FindGameObjectWithTag("GameManager").GetComponent<WeaponSmithOracle>().GetInstructions(sheet.baseCity);
                break;

            case Jobs.WOODCUTER:
                i = GameObject.FindGameObjectWithTag("GameManager").GetComponent<WoodCuterOracle>().GetInstructions(sheet.baseCity);
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
