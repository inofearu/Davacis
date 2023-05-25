using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
                {value = InventoryList.Count - 1;}
            else if(value > InventoryList.Count - 1)
                {value = 0;}
            equippedIndex = value;
            if(debugSwitch == true)
            {Debug.Log($"{equippedItem} {equippedIndex} - {InventoryList.Count}");}
        }   
    }
    private void Start() 
    {
        equippedItem = new GameObject();    
    }
    private void Update() 
    {
        if(Input.GetKeyDown("e")) // make rebindable
        {
            modifyEquippedIndex += 1;
            ChangeWeapon();
        }
        if(Input.GetKeyDown("q"))// make rebindable
        {
            modifyEquippedIndex -= 1;
            ChangeWeapon();
        }
    }
    private void ChangeWeapon()
    {
        GenericItem desiredItem = InventoryList[equippedIndex];
        Destroy(equippedItem);
        makeBaseItem(desiredItem);
    }
    private void makeBaseItem(GenericItem desiredItem)
    {
        equippedItem = new GameObject();
        equippedItem.transform.SetParent(playerWeaponSlot.transform);
        equippedItem.transform.rotation = Quaternion.identity;
        equippedItem.transform.position = new Vector3(0,0,0);
        equippedItem.name = "EquippedItem";
        if(desiredItem.GetType() == typeof(WeaponItem))
        {
            equippedItem.AddComponent<Weapon>();
        }
        /*else if(desiredItem.GetType == typeof(Item)) // if weapon isnt weapon assign other script
        {
            
        }*/
    }
}
