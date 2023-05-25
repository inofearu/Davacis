using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Weapon : MonoBehaviour
{
    private bool debugSwitch = false; // make this togglable elsewhere
    private GameObject Projectile;
    private float lastFired = 0;
    private PlayerStats playerStatFile;
    private GameObject player;
    public Dictionary<string, object> stats;
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
        if(debugSwitch == true)
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