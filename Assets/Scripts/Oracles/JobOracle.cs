using UnityEngine;
using System.Collections.Generic;
using System;

public class JobOracle
{
    Dictionary<Jobs, int> OpenPositions;
    Dictionary<Jobs, int> AvaliableSpots;
    Logger logger;
    bool debug = false;

    public JobOracle()
    {
        OpenPositions = new Dictionary<Jobs,int>();
        AvaliableSpots = new Dictionary<Jobs, int>();
        this.logger = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Logger>();
        foreach (Jobs job in Enum.GetValues(typeof(Jobs)))
        {
            OpenPositions.Add(job, 1);
        }
        foreach (Jobs job in Enum.GetValues(typeof(Jobs)))
        {
            AvaliableSpots.Add(job, 1);
        }
        OpenPositions.Remove(Jobs.NONE);
        AvaliableSpots.Remove(Jobs.NONE);
        OpenPositions[Jobs.FARMER] = 2;
        AvaliableSpots[Jobs.FARMER] = 2;
        OpenPositions[Jobs.MINER] = 2;
        AvaliableSpots[Jobs.MINER] = 2;

    }

    public Jobs GetJob(CharacterSheet sheet)
    {
        foreach (Jobs job in OpenPositions.Keys)
        {
            if (OpenPositions[job] > 0)
            {
                OpenPositions[job]--;
                return job;
            }
        }
        return Jobs.NONE;
    }

    public void LeftJob(Jobs job)
    {
        OpenPositions[job]++;
    }

  
}
