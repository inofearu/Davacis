using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Weapon.asset", menuName = "Inventory/Item/Consumable")]
public class WeaponItem : GenericItem
{
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
