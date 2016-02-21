using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.TradeEngine
{
    class Map
    {
        public List<City> Cities { get; set; }
        public List<Route> Routes { get; set; }

        public Map(List<City> cities, List<Route> routes)
        {
            Cities = cities;
            Routes = routes;
        }
    }
}
