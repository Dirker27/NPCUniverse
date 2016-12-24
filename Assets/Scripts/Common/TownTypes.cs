using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TownTypes
{
    static class Types
    {
        public static List<Type> TOWN_BASEBUILDING = new List<Type> { typeof(Barn), typeof(LogStore), typeof(OreShop), typeof(TradeHouse) };

        public static List<Type> TOWN_EVERYBUILDING = new List<Type> { typeof(Forest), typeof(Farm), typeof(Pond), typeof(Mine),
                                                         typeof(WoodCut), typeof(Mill), typeof(Brewhouse), typeof(Foundry), typeof(BowShop), typeof(SawHouse), typeof(Masonry),
                                                         typeof(CharcoalPit), typeof(Smithy), typeof(Bakery), typeof(HuntingLodge),
                                                         typeof(GuildHall), typeof(Tavern),
                                                       };
        public static List<Type> TOWN_MINNINGTOWN = new List<Type> { typeof(Mine),
                                                         typeof(Foundry), typeof(Masonry),
                                                         typeof(Smithy),
                                                         typeof(GuildHall), typeof(Tavern),
                                                       };
    }
}