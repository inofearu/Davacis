using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Weapon : MonoBehaviour
{
    private GameObject Projectile;
    private int damage;
    private int destructionDamage;
    private float speed;
    //private float projectileRadius;
    private float damageRadius;
    private float destructionRadius;
    private bool useGravity;
    private float softLife;
    private float hardLife;
    private int manaUse;
    private float cooldown;
    private bool printDebug = false; //move this, has to be changed in script right now
    private float lastFired = 0;
    PlayerStats playerStatFile;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");   
        playerStatFile = player.GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    private void Update()
    {
        int mana = playerStatFile.accessMana;
        bool manaReq = mana >= manaUse;
        
        string weaponDebugInfo = @$"Weapon:
        ---Stats---
        Mana - {mana}
        ---Tests---
        ManaCheck - {mana>=manaUse}
        CooldownCheck - {Time.time > lastFired + cooldown}";
        if(printDebug)
        {
            Debug.Log(weaponDebugInfo);
        }
        if(Input.GetMouseButton(0) && Time.time > lastFired + cooldown && manaReq)
        {
            Fire();
            lastFired = Time.time;
            playerStatFile.accessMana = mana - manaUse;
        }
    }

    private void Fire()
    {
        GameObject instantiatedProjectile = Instantiate(Projectile, transform.position, Quaternion.identity, gameObject.transform);
        instantiatedProjectile.transform.rotation = instantiatedProjectile.transform.parent.parent.rotation;
        instantiatedProjectile.transform.parent = null;
        instantiatedProjectile.transform.Rotate(-90,0,0,Space.Self);
    }
}