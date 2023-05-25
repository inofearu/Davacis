using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ItemInterface
{

}

[CreateAssetMenu(fileName = "Weapon.asset", menuName = "Inventory/Item/Weapon")]
public class WeaponItem : ItemInterface
{
    public string itemName;
    public string itemDescription;
    public GameObject itemModel;
    public int goldValue;
    [SerializeField] private GameObject Projectile;
    [SerializeField] private int damage,destructionDamage,manaUse;
    [SerializeField] private float damageRadius,destructionRadius,projectileRadius,softLife,hardLife,cooldown,speed;
    [SerializeField] private bool useGravity;
}
[SerializeField] public class ItemDatabase : MonoBehaviour
{
    public List<ItemInterface> ItemList;
    void Start()
    {
    }
}
