using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{

    //Logging

    public Logger logger;

    //List of all oracles

    private ArmorSmithOracle armorSmithOracle;
    private BakerOracle bakerOracle;
    private BrewMasterOracle brewMasterOracle;
    private CollierOracle collierOracle;
    private FarmOracle farmOracle;
    private FishermanOracle firshermanOracle;
    private FletcherOracle fletcherOracle;
    private ForesterOracle foresterOracle;
    private FoundryOracle foundryOracle;
    private HunterOracle hunterOracle;
    private InnKeeperOracle innKeeperOracle;
    private MillOracle millOracle;
    private MineOracle mineOracle;
    private QuaterMasterOracle quaterMasterOracle;
    private SawWorkerOracle sawWorkerOracle;
    private StoneCutterOracle stoneCutterOracle;
    private ToolSmithOracle toolSmithOracle;
    private WeaponSmithOracle weaponSmithOracle;
    private WoodCuterOracle woodCuterOracle;
    private TownOracle townOracle;
    private RegionOracle regionOracle;

    // Non job oracles
    private JobOracle jobOracle;
    private NPCOracle npcOracle;

    // Use this for initialization
    void Start()
    {
        //GetTownOracle().BuildBasicBuildings();
        GetRegionOracle().NewTown();
        InvokeRepeating("UpdateRegion", 1, 3);
    }

    void UpdateRegion()
    {
        GetRegionOracle().UpdateTowns();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public RegionOracle GetRegionOracle()
    {
        if (regionOracle == null)
        {
            regionOracle = new RegionOracle();
        }

        return regionOracle;
    }

    public JobOracle GetJobOracle()
    {
        if (jobOracle == null)
        {
            jobOracle = new JobOracle();
        }

        return jobOracle;
    }

    public ArmorSmithOracle GetArmorSmithOracle()
    {
        if (armorSmithOracle == null)
        {
            armorSmithOracle = new ArmorSmithOracle();
        }

        return armorSmithOracle;
    }

    public BakerOracle GetBakerOracle()
    {
        if (bakerOracle == null)
        {
            bakerOracle = new BakerOracle();
        }

        return bakerOracle;
    }

    public BrewMasterOracle GetBrewMasterOracle()
    {
        if (brewMasterOracle == null)
        {
            brewMasterOracle = new BrewMasterOracle();
        }

        return brewMasterOracle;
    }

    public CollierOracle GetCollierOracle()
    {
        if (collierOracle == null)
        {
            collierOracle = new CollierOracle();
        }

        return collierOracle;
    }

    public FarmOracle GetFarmOracle()
    {
        if (farmOracle == null)
        {
            farmOracle = new FarmOracle();
        }

        return farmOracle;
    }

    public FishermanOracle GetFishermanOracle()
    {
        if (firshermanOracle == null)
        {
            firshermanOracle = new FishermanOracle();
        }

        return firshermanOracle;
    }

    public FletcherOracle GetFletcherOracle()
    {
        if (fletcherOracle == null)
        {
            fletcherOracle = new FletcherOracle();
        }

        return fletcherOracle;
    }

    public ForesterOracle GetForesterOracle()
    {
        if (foresterOracle == null)
        {
            foresterOracle = new ForesterOracle();
        }

        return foresterOracle;
    }

    public FoundryOracle GetFoundryOracle()
    {
        if (foundryOracle == null)
        {
            foundryOracle = new FoundryOracle();
        }

        return foundryOracle;
    }

    public HunterOracle GetHunterOracle()
    {
        if (hunterOracle == null)
        {
            hunterOracle = new HunterOracle();
        }

        return hunterOracle;
    }

    public InnKeeperOracle GetInnKeeperOracle()
    {
        if (innKeeperOracle == null)
        {
            innKeeperOracle = new InnKeeperOracle();
        }

        return innKeeperOracle;
    }

    public MillOracle GetMillOracle()
    {
        if (millOracle == null)
        {
            millOracle = new MillOracle();
        }

        return millOracle;
    }

    public MineOracle GetMineOracle()
    {
        if (mineOracle == null)
        {
            mineOracle = new MineOracle();
        }

        return mineOracle;
    }

    public QuaterMasterOracle GetQuaterMasterOracle()
    {
        if (quaterMasterOracle == null)
        {
            quaterMasterOracle = new QuaterMasterOracle();
        }

        return quaterMasterOracle;
    }

    public SawWorkerOracle GetSawWorkerOracle()
    {
        if (sawWorkerOracle == null)
        {
            sawWorkerOracle = new SawWorkerOracle();
        }

        return sawWorkerOracle;
    }

    public StoneCutterOracle GetStoneCutterOracle()
    {
        if (stoneCutterOracle == null)
        {
            stoneCutterOracle = new StoneCutterOracle();
        }

        return stoneCutterOracle;
    }

    public ToolSmithOracle GetToolSmithOracle()
    {
        if (toolSmithOracle == null)
        {
            toolSmithOracle = new ToolSmithOracle();
        }

        return toolSmithOracle;
    }

    public WeaponSmithOracle GetWeaponSmithOracle()
    {
        if (weaponSmithOracle == null)
        {
            weaponSmithOracle = new WeaponSmithOracle();
        }

        return weaponSmithOracle;
    }

    public WoodCuterOracle GetWoodCuterOracle()
    {
        if (woodCuterOracle == null)
        {
            woodCuterOracle = new WoodCuterOracle();
        }

        return woodCuterOracle;
    }

    public NPCOracle GetNPCOracle()
    {
        if (npcOracle == null)
        {
            npcOracle = new NPCOracle();
        }

        return npcOracle;
    }

    public Logger GetLogger()
    {
        if (logger == null)
        {
            logger = new Logger();
        }
        return logger;
    }
}
