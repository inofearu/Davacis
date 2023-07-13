using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory.asset", menuName = "Inventory/Container")]
public class Inventory : ScriptableObject
{
    private int inventoryMaxSize = 24;
    public List<ItemData> inventoryContents = new();
    public bool AddItem(ItemInstance itemToAdd)
    {

        if (inventoryContents.Count < inventoryMaxSize)
        {
            inventoryContents.Add(itemToAdd);
            return true;
        }
        Debug.Log($"Failed to add {itemToAdd} due to full inventory.");
        return false;
    }
}
