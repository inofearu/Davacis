using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    
    private int damage;
    private int destructionDamage;
    private float speed;
    private float projectileRadius;
    private float damageRadius;
    private float destructionRadius;
    private bool useGravity;
    private float softLife;
    private float hardLife;
    private Projectile varSource;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        GameObject equippedWeapon = GameObject.FindWithTag("EquippedWeapon");
        varSource = equippedWeapon.GetComponent<Projectile>();
        rb = GetComponent<Rigidbody>(); 

        damage = varSource.damage;
        destructionDamage = varSource.destructionDamage;
        speed = varSource.speed;
        //projectileRadius = varSource.projectileRadius;
        damageRadius = varSource.damageRadius;
        destructionRadius = varSource.destructionRadius;
        rb.useGravity = varSource.useGravity;
        softLife = varSource.softLife;
        hardLife = varSource.hardLife;

        rb.AddForce(transform.up * speed);
        Debug.Log($"{transform.up * speed}");
        if(softLife != 0)
        {
            softLife += Time.time; // could pull time from weapon.cs but the small difference means its not worth it.
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > hardLife)
        {
            Destroy(gameObject);
        }
        else if(Time.time > softLife)
        {
            rb.useGravity = true;
        }
    }
}