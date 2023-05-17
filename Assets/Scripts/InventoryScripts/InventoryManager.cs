using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] GameObject playerWeaponSlot;
    [SerializeField] GameObject ItemDatabase;
    [SerializeField] List<GenericItem> InventoryList;
    private GameObject gameObject; 
    private int equippedIndex = 0;
    private int modifyEquippedIndex
    {
        get{return equippedIndex;}
        set
        {
            if(value < 0)
                {value = InventoryList.Count() - 1;}
            else if(value > InventoryList.Count() - 1);
                {value = 0;}
            equippedIndex = value;
        }   
    }
    private void Update() 
    {
        if(Input.GetKeyDown("e"))
        {
            equippedIndex += 1;
            ChangeWeapon();
        }
        if(Input.GetKeyDown("q"))
        {
            equippedIndex -= 1;
            ChangeWeapon();
        }
    }
    private void ChangeWeapon()
    {
        Instantiate(InventoryList[equippedIndex]);
    }
}
