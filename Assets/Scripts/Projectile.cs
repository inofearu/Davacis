using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Projectile varSource;
    // Start is called before the first frame update
    void Start()
    {
        GameObject equippedWeapon = GameObject.FindWithTag("EquippedWeapon");
        varSource = equippedWeapon.GetComponent<Projectile>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}