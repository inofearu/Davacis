using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GenericItem.asset", menuName = "Inventory/Item/Generic")]
public class GenericData : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    public GameObject itemModel;
    public int goldValue;
}
