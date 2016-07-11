using UnityEngine;
using System.Collections.Generic;

public class MineOracle : MonoBehaviour
{
    public Logger logger;
    private bool debug = false;

    private bool ToOre = true;
    void Start()
    {
        this.logger = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Logger>();
    }

    /**
     * Nathaniel Hates context and happiness part two. For solution see FarmOracle.
     * 
     * The Ore mine is at [0] in Fort Kickass' Mine pool.
     * The Stone mine is at [1] in Fort Kickass' Mine pool. 
     * 
     */
    public Mine WhereShouldIMine(TradeCity currentCity)
    {
        if (ToOre)
        {
            ToOre = false;
            return currentCity.Mines[0];
        }
        ToOre = true;
        return currentCity.Mines[1];
    }

    public OreShop WhereShouldIStore(TradeCity currentCity)
    {
        return currentCity.OreShops[0];
    }
}
