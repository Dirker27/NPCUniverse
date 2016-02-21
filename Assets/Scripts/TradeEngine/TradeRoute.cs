﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class TradeRoute
{
    public TradeCity CityOne { get; set; }
    public TradeCity CityTwo { get; set; }

    public TradeRoute(TradeCity cityone, TradeCity citytwo)
    {
        CityOne = cityone;
        CityTwo = citytwo;
    }
}
