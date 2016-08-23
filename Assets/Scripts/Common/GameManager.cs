﻿using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{

    //Logging

    public Logger logger;

    //List of all oracles

    public ArmorSmithOracle armorSmithOracle;
    public JobOracle jobOracle;

	// Use this for initialization
	void Start () 
    {

	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public JobOracle GetJobOracle()
    {
        return jobOracle;
    }
}
