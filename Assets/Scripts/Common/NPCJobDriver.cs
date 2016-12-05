﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class NPCJobDriver : NonPlayableCharacter
{

    List<Instruction> instructions;
    int currentIntruction;

    public override void Start()
    {
        base.Start();

        debug = false;
        logger.Log(debug, "sheet is:" + (sheet != null));
        instructions = new List<Instruction>();
        currentIntruction = 0;
    }

    void Update()
    {

        sheet.currentState = sheet.npcOracle.WhatShouldIDo(sheet);
        
        if (sheet.currentState != NPCStates.WORK && sheet.previousState == NPCStates.WORK)
        {
            instructions.InsertRange(currentIntruction, NPCInstructions(sheet));
            sheet.previousState = sheet.currentState;
            logger.Log(debug, "Inserterting (" + instructions.Count + ") npc instructions at:" + currentIntruction + " changing state to:" + sheet.currentState + " and previous state to:" + sheet.previousState);
            GetComponent<CharacterMovement>().destination = instructions[currentIntruction].destination;
            
        }
        if (instructions.Count == 0)
        {
            instructions = sheet.npcOracle.GetInstruction(sheet);
            currentIntruction = 0;
            logger.Log(debug, "Adding instruction of length:" + instructions.Count);
            if (instructions[currentIntruction].destination != null)
            {
                GetComponent<CharacterMovement>().destination = instructions[currentIntruction].destination;
            }
        }
        else if (GetComponent<CharacterMovement>() != null && !GetComponent<CharacterMovement>().isInTransit())
        {
            logger.Log(debug, "Traveling");
            if (GetComponent<CharacterMovement>().location == instructions[currentIntruction].destination)
            {
                logger.Log(debug, "doing action");
                if (instructions[currentIntruction].fun1 != null)
                {
                    instructions[currentIntruction].fun1(instructions[currentIntruction], sheet);
                }
                if (instructions[currentIntruction].fun2 != null)
                {
                    instructions[currentIntruction].fun2();
                }
                currentIntruction++;
                logger.Log(debug, "CurrentInstruction is now:" + currentIntruction + " total instructions:" + instructions.Count);
                if (currentIntruction == instructions.Count)
                {
                    logger.Log(debug, "getting new instructions");
                    currentIntruction = 0;
                    instructions.Clear();
                    instructions = sheet.npcOracle.GetInstruction(sheet);
                }
                GetComponent<CharacterMovement>().destination = instructions[currentIntruction].destination;
                logger.Log(debug, "going back to work");
                sheet.currentState = NPCStates.WORK;
                sheet.previousState = NPCStates.WORK;
            }
        }

        DatabaseInterface di = new DatabaseInterface();
        di.Update(sheet);
    }
}