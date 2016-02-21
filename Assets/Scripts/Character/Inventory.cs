using UnityEngine;
using System.Collections;
using System.Collections.Generic;  // dafuq? - No. Fuck You, Nathaniel.

public class Inventory : MonoBehaviour
{
    public int currency;
    public List<Item> items { get; set; }
}
