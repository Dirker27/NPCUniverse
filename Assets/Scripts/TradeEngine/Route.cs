using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.TradeEngine
{
    class Route
    {
        public City CityOne { get; set; }
        public City CityTwo { get; set; }

        public Route(City cityone, City citytwo)
        {
            CityOne = cityone;
            CityTwo = citytwo;
        }
    }
}
