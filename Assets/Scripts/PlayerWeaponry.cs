using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerWeaponry : MonoBehaviour
{
    [SerializeField] GameObject Projectile;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int left = 0;
        int middle = 2;
        int right = 1;
        if(Input.GetMouseButton(left))
        {
            Fire();
        }

    }
    void Fire()
    {
        GameObject instantiatedProjectile = Instantiate(Projectile, transform.position, Quaternion.identity, gameObject.transform);
        instantiatedProjectile.transform.rotation = instantiatedProjectile.transform.parent.rotation;
        instantiatedProjectile.transform.parent = null;
    }
}
// Instantiate(Bullet, transform.position, Quaternion.identity, gameObject.transform);
/* 		transform.rotation = transform.parent.rotation;
		transform.Rotate(-90,0,0,Space.Self);
        gameObject.transform.parent = null;
        gameObject.transform.localScale = new Vector3(bulletSize,bulletSize,bulletSize);
        firedAt = Time.time;
        rb.AddForce(transform.up * bulletForce * Time.deltaTime);
        Destroy(gameObject,bulletHardLife); */