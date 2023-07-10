using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class InventoryManager : MonoBehaviour
{
    private bool debugSwitch = true;
    [SerializeField] private GameObject playerWeaponSlot;
    [SerializeField] private GameObject itemDatabase;
    [SerializeField] private List<ItemData> inventory;
    private List<WeaponData> equippable;
    private GameObject equippedItem;
    private int equippedIndex = 0;
    private int modifyEquippedIndex
    {
        get{return equippedIndex;}
        set
        {
            if(value < 0)
                {value = inventory.Count - 1;}
            else if(value > inventory.Count - 1)
                {value = 0;}
            equippedIndex = value;
            if(debugSwitch == true)
            {Debug.Log($"{equippedItem} {equippedIndex} - {inventory.Count}");}
        }   
    }
    private void Start() 
    {
        equippedItem = new GameObject();
        inventory = fetchInventory();
    }
    private List<ItemData> fetchInventory() // TODO: add dynamic path
    {
       // DirectoryInfo DI = new DirectoryInfo("C:/Users/Inofearu/Unity Projects/Magic RPG/Assets");
       // FileInfo[] info = DI.GetFiles("*.*");
       // foreach (FileInfo file in info) 
       // {
         //   var item = AssetDatabase.LoadAssetAtPath(file.FullName, typeof(UnityEngine.Object));
         //   Debug.Log(item);
          //  inventory.Add(item);
      //  }
       // return inventory;
    }
    private void Update() 
    {
        if(Input.GetKeyDown("e")) // TODO: make rebindable
        {
            modifyEquippedIndex += 1;
            ChangeWeapon();
        }
        if(Input.GetKeyDown("q"))// TODO: make rebindable
        {
            modifyEquippedIndex -= 1;
            ChangeWeapon();
        }
    }
    private void ChangeWeapon()
    {
        ItemData desiredItem = inventory[equippedIndex];
        Destroy(equippedItem);
        initaliseItem(desiredItem);
    }
    private void initaliseItem(ItemData desiredItem)
    {
        equippedItem = new GameObject();
        equippedItem.transform.SetParent(playerWeaponSlot.transform);
        equippedItem.transform.rotation = Quaternion.identity;
        equippedItem.transform.position = new Vector3(0,0,0);

        equippedItem.name = "EquippedItem";
        Debug.Log(desiredItem.GetType() == typeof(WeaponData));
        if(desiredItem.GetType() == typeof(WeaponData))
        {
            //equippedItem.AddComponent<Weapon>();
            //equippedItem.Weapon.stats = desiredItem.stats;
        }
        /*else if(desiredItem.GetType == typeof(Item)) // if weapon isnt weapon assign other script
        {
            
        }*/
    }
}
// TODO: actually load the fucking item sometime