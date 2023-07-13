using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory.asset", menuName = "Inventory/Container")]
public class Inventory : ScriptableObject
{
    private int inventoryMaxSize = 24;
    public List<ItemInstance> items = new();
    public bool AddItem(ItemInstance itemToAdd)
    {
       /* for (int i = 0; i < ItemInstance.Count; i++)
        {
            if (items[i] == null)
            {
                var debugCache = items[i];
                items[i] = itemToAdd;
                Debug.Log($"Replaced item {debugCache} at pos {i} with {itemToAdd}");
                return true;
            }
        }*/
        if (items.Count < inventoryMaxSize)
        {
            items.Add(itemToAdd);
            return true;
        }
        Debug.Log($"Failed to add {itemToAdd} due to full inventory.");
        return false;
    }
}