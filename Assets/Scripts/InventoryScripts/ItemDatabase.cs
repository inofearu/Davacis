using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GenericItem.asset", menuName = "Inventory/Item/Generic")]
public class GenericItem : ScriptableObject
{
    public string type = "Generic";
    public int itemID;
    public string itemName;
    public string itemDescription;
    public GameObject itemModel;
    public int goldValue;
    //public GameObject gameObject;
}

[CreateAssetMenu(fileName = "Weapon.asset", menuName = "Inventory/Item/Weapon")]
public class WeaponItem : GenericItem
{
    new public string type = "Weapon";
    private GameObject Projectile;
    public int damage;
    public int destructionDamage;
    public float speed;
    //[SerializeField] float projectileRadius;
    public float damageRadius;
    public float destructionRadius;
    public bool useGravity;
    public float softLife;
    public float hardLife;
    public int manaUse;
    public float cooldown;
}
[System.Serializable]
public class ItemDatabase : MonoBehaviour
{
    public List<GenericItem> ItemList;
    void Start()
    {
    }
}
