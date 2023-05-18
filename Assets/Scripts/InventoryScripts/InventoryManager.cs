using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InventoryManager : MonoBehaviour
{
    private bool debugSwitch = true;
    [SerializeField] GameObject playerWeaponSlot;
    [SerializeField] GameObject ItemDatabase;
    [SerializeField] List<GenericItem> InventoryList;
    private GameObject equippedItem;
    private int equippedIndex = 0;
    private int modifyEquippedIndex
    {
        get{return equippedIndex;}
        set
        {
            if(value < 0)
                {value = InventoryList.Count() - 1;}
            else if(value > InventoryList.Count() - 1)
                {value = 0;}
            equippedIndex = value;
            if(debugSwitch == true)
            {Debug.Log($"{equippedItem} {equippedIndex} - {InventoryList.Count()}");}
        }   
    }
    private void Update() 
    {
        if(Input.GetKeyDown("e"))
        {
            modifyEquippedIndex += 1;
            ChangeWeapon();
        }
        if(Input.GetKeyDown("q"))
        {
            modifyEquippedIndex -= 1;
            ChangeWeapon();
        }
    }
    private void ChangeWeapon()
    {
        equippedItem = Instantiate(InventoryList[equippedIndex],playerWeaponSlot.transform);
    }
}
