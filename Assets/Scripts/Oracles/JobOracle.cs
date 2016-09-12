using UnityEngine;
using System.Collections.Generic;
using System;

public class JobOracle
{
    Dictionary<Jobs, int> CurrentPositions;
    Dictionary<Jobs, int> TotalPositions;
    Logger logger;
    bool debug = false;

    public JobOracle()
    {
        CurrentPositions = new Dictionary<Jobs,int>();
        TotalPositions = new Dictionary<Jobs, int>();
        this.logger = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().GetLogger();
 

    }

    public Jobs GetJob(CharacterSheet sheet)
    {
        foreach (Jobs job in CurrentPositions.Keys)
        {
            if (CurrentPositions[job] > 0)
            {
                CurrentPositions[job]--;
                return job;
            }
        }
        return Jobs.NONE;
    }

    public void LeftJob(Jobs job)
    {
        CurrentPositions[job]++;
    }

    public void AddJobs(Jobs job, int toAdd)
    {
        if (!TotalPositions.ContainsKey(job))
        {
            TotalPositions[job] = toAdd;
            CurrentPositions[job] = toAdd;
        }
        else
        {
            TotalPositions[job] += toAdd;
            CurrentPositions[job] += toAdd;
        }
    }
}
