using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.TradeEngine
{
    class TradeCity
    {
        public List<TradeRoute> Routes { get; set; }
        public TradeMarketPlace MarketPlace { get; set; }

        public TradeCity(List<TradeRoute> routes, TradeMarketPlace marketPlace)
        {
            Routes = routes;
            MarketPlace = marketPlace;
        }
    }
}
