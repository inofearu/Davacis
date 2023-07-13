using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item.asset", menuName = "Inventory/Item/Generic")]

public class ItemData : ScriptableObject
{
    public string itemName;
    [TextArea] public string itemDescription;
    public GameObject itemModel;
    public int goldValue;
    public int baseCondition;
    
}
