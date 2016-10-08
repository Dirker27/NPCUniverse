using Npgsql;

public class CharacterSheet
{
    public int id;

    public string name;
    public int health;
    public int hunger;
    public int energy;


    public TradeOracle tradeOracle;
    public NPCOracle npcOracle;
    
    public TradeCity baseCity;

    public Inventory inventory;
    
    public NPCStates previousState = NPCStates.WORK;
    public NPCStates currentState = NPCStates.WORK;
    public bool destinationIsBaseCity = false;
    public bool destinationIsHome = false;

    public Jobs job = Jobs.NONE;

    public void Save()
    {
        DatabaseInterface di = new DatabaseInterface();
        di.Save(this);
    }
}