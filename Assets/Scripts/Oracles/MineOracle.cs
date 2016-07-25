using UnityEngine;
using System.Collections.Generic;

public class MineOracle : MonoBehaviour
{
    public Logger logger;
    private bool debug = false;

    private bool Ore = true;
    private bool Stone = false;
    void Start()
    {
        this.logger = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Logger>();
    }

    public Mine WhereShouldIMine(TradeCity currentCity)
    {
        return currentCity.Mines[0];
    }

    public OreShop WhereShouldIStore(TradeCity currentCity)
    {
        return currentCity.OreShops[0];
    }

    public ItemType WhatShouldIMine()
    {
        if (Ore)
        {
            Ore = false;
            Stone = true;
            return ItemType.ORE;
        }
        else if (Stone)
        {
            Stone = false;
            Ore = true;
            return ItemType.STONE;
        }
        return ItemType.INVALID;
    }
}
