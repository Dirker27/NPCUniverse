using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Smithy : BaseBuilding
{
    public ItemType producesArmor;
    public ItemType producesWeapon;

    public void Start()
    {
        base.Start();
        this.producesArmor = ItemType.ARMOR;
        this.producesWeapon = ItemType.WEAPON;
        this.debug = false;
    }

    public ItemType WorkSmithyArmor(Item input)
    {
        return producesArmor;
    }

    public ItemType WorkSmithyWeapon(Item input)
    {
        return producesWeapon;
    }

    public ItemType WorkSmithyTool(Item input)
    {
        return producesWeapon;
    }
}

