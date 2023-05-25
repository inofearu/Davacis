using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GenericItem.asset", menuName = "Inventory/Item/Generic")]
public abstract class GenericItem : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    public GameObject itemModel;
    public int goldValue;
    //public GameObject gameObject;
}

[CreateAssetMenu(fileName = "Weapon.asset", menuName = "Inventory/Item/Weapon")]
public class WeaponItem : GenericItem
{
    [SerializeField] private GameObject Projectile;
    [SerializeField] private int damage,destructionDamage,manaUse;
    [SerializeField] private float damageRadius,destructionRadius,projectileRadius,softLife,hardLife,cooldown,speed;
    [SerializeField] private bool useGravity;
}
[SerializeField] public class ItemDatabase : MonoBehaviour
{
    public List<GenericItem> ItemList;
    void Start()
    {
    }
}
