using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Weapon : MonoBehaviour
{
    [SerializeField] GameObject Projectile;
    [SerializeField] int damage;
    [SerializeField] int destructionDamage;
    [SerializeField] float speed;
    [SerializeField] float projectileRadius;
    [SerializeField] float damageRadius;
    [SerializeField] float destructionRadius;
    [SerializeField] bool useGravity;
    [SerializeField] float softLife;
    [SerializeField] float hardLife;
    [SerializeField] int manaUse;
    [SerializeField] int cooldown;
    private float lastFired = 0;
    PlayerStats playerStatFile;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");   
        playerStatFile = player.GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        int mana = playerStatFile.mana; // mana needs to be returned
        int left = 0;
        bool test1 = mana > manaUse;
        bool test2 = Time.time > lastFired + cooldown;
        Debug.Log($"\nMana - {test1}\nCooldown-{test2}");
        if(Input.GetMouseButton(left) && Time.time > lastFired + cooldown && mana > manaUse)
        {
            Fire();
            lastFired = Time.time;
            mana -= manaUse;
        }
    }

    void Fire()
    {
        GameObject instantiatedProjectile = Instantiate(Projectile, transform.position, Quaternion.identity, gameObject.transform);
        instantiatedProjectile.transform.rotation = instantiatedProjectile.transform.parent.parent.rotation;
        instantiatedProjectile.transform.parent = null;
        instantiatedProjectile.transform.Rotate(-90,0,0,Space.Self);
    }
}

/*       rb.AddForce(transform.up * bulletForce * Time.deltaTime);
        Destroy(gameObject,bulletHardLife); */