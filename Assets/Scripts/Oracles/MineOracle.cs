using UnityEngine;
using System.Collections.Generic;

public class MineOracle : MonoBehaviour
{
    public Logger logger;
    private bool debug = false;

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
}
