using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class TradeData
{
    public Item Item { get; set; }
    public int BaseCost { get; set; }
    public int DesiredAmount { get; set; }
    public int CurrentAmount { get; set; }

    public TradeData(Item item, int baseCost, int desiredAmount, int currentAmount)
    {
        Item = item;
        BaseCost = baseCost;
        DesiredAmount = desiredAmount;
        CurrentAmount = currentAmount;
    }

    public int CurrentCost()
    {
        return BaseCost;
    }
}