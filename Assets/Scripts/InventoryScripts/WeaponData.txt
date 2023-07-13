using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon.asset", menuName = "Inventory/Item/Weapon")]
public class WeaponData : ItemData
{
    [SerializeField] private GameObject Projectile;
    [SerializeField] private int damage,destructionDamage,manaUse;
    [SerializeField] private float damageRadius,destructionRadius,projectileRadius,softLife,hardLife,cooldown,speed;
    [SerializeField] private bool useGravity;
}