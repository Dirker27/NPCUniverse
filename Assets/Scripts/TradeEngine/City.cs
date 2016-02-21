using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.TradeEngine
{
    class City
    {
        public List<Route> Routes { get; set; }
        public MarketPlace MarketPlace { get; set; }

        public City(List<Route> routes, MarketPlace marketPlace)
        {
            Routes = routes;
            MarketPlace = marketPlace;
        }
    }
}
