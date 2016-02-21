using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.TradeEngine
{
    class TradeData
    {
        public TradeItem Item { get; set; }
        public int BaseCost { get; set; }
        public int DesiredAmount { get; set; }
        public int CurrentAmount { get; set; }

        public TradeData(TradeItem item, int baseCost, int desiredAmount, int currentAmount)
        {
            Item = item;
            BaseCost = baseCost;
            DesiredAmount = desiredAmount;
            CurrentAmount = currentAmount;
        }

        public int CurrentCost()
        {
            return (DesiredAmount / CurrentAmount) * BaseCost;
        }
    }
}
