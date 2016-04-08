using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Farm : MonoBehaviour
{
    public ItemType produces;


    public ItemType WorkFarm()
    {
        return produces;
    }
}
