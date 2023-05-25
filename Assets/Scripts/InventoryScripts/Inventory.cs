using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory.asset", menuName = "Inventory/Container")]
public class Inventory : ScriptableObject
{
    public List<GenericData> ItemList = new();
    void Start()
    {
    }
}
