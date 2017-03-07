using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TradeCity : MonoBehaviour
{
    public List<TradeRoute> Routes;
    public TradeMarketPlace MarketPlace;
    public List<Farm> Farms;
    public List<Barn> Barns;
    public List<Mill> Mills;
    public List<Bakery> Bakeries;
    public List<Mine> Mines;
    public List<OreShop> OreShops;
    public List<Foundry> Foundries;
    public List<Forest> Forests;
    public List<WoodCut> WoodCuts;
    public List<CharcoalPit> CharcoalPits;
    public List<LogStore> LogStores;
    public List<Smithy> Smithies;
    public List<GuildHall> GuildHalls;
    public List<Brewhouse> Brewhouses;
    public List<Pond> Ponds;
    public List<Tavern> Taverns;
    public List<Masonry> Masonries;
    public List<SawHouse> SawHouses;
    public List<BowShop> BowShops;
    public List<HuntingLodge> HuntingLodges;
    public List<TradeHouse> TradeHouses;
    public TownOracle townOracle;

    public void Start()
    {
        Routes = new List<TradeRoute>();
        Farms = new List<Farm>();
        Barns = new List<Barn>();
        Mills = new List<Mill>();
        Bakeries = new List<Bakery>();
        Mines = new List<Mine>();
        OreShops = new List<OreShop>();
        Foundries = new List<Foundry>();
        Forests = new List<Forest>();
        WoodCuts = new List<WoodCut>();
        CharcoalPits = new List<CharcoalPit>();
        LogStores = new List<LogStore>();
        Smithies = new List<Smithy>();
        GuildHalls = new List<GuildHall>();
        Brewhouses = new List<Brewhouse>();
        Ponds = new List<Pond>();
        Taverns = new List<Tavern>();
        Masonries = new List<Masonry>();
        SawHouses = new List<SawHouse>();
        BowShops = new List<BowShop>();
        HuntingLodges = new List<HuntingLodge>();
        TradeHouses = new List<TradeHouse>();
    }
}
