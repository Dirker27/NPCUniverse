using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Mine : MonoBehaviour
{
    public ItemType produces;


    public ItemType WorkMine()
    {
        return produces;
    }
}
