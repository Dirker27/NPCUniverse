using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TradeEngine
{
    class MarketPlace
    {
        public List<TradeData> TradeDataManifest { get; set; }
        public List<Route> TradeRoutes { get; set; }
        public MarketPlace(List<TradeData> tradeDataManifest, List<Route> tradeRoutes)
        {
            TradeDataManifest = tradeDataManifest;
            TradeRoutes = tradeRoutes;
        }
    }
}
