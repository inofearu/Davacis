using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public Inventory inventory;
    private GameObject equippedItem;
    private int equippedIndex = 0;
    [SerializeField] private GameObject playerItemSlot;
    private int modifyEquippedIndex
    {
        get{return equippedIndex;}
        set
        {
            if(value < 0)
                {value = inventory.items.Count - 1;}
            else if(value > inventory.items.Count - 1)
                {value = 0;}
            equippedIndex = value;
            Debug.Log($"{equippedItem} {equippedIndex} - {inventory.items.Count}");
        }   
    }
    private void ChangeItem()
    {
        ItemInstance desiredItem = inventory.items[equippedIndex];
        Destroy(equippedItem);
        loadItem(desiredItem);
    }
    private void loadItem(ItemInstance desiredItem)
    {
        equippedItem = new GameObject();
        equippedItem.transform.SetParent(playerItemSlot.transform);
        equippedItem.transform.rotation = Quaternion.identity;
        equippedItem.transform.position = new Vector3(0,0,0);

        equippedItem.name = "EquippedItem";
        equippedItem.model = desiredItem.itemModel;
        /*if(desiredItem.GetType() == typeof(WeaponData))
        {
            equippedItem.AddComponent<Weapon>();
            equippedItem.Weapon.stats = desiredItem.stats;
        }*/
    }
    private void Start() 
    {
        equippedItem = new GameObject();
    }
    private void Update() 
    {
        if(Input.GetKeyDown("e"))// TODO: make rebindable
        {
            modifyEquippedIndex += 1;
            ChangeItem();
        }
        if(Input.GetKeyDown("q"))// TODO: make rebindable
        {
            modifyEquippedIndex -= 1;
            ChangeItem();
        }
    }
}
