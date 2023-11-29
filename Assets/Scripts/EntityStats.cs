using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class EntityStats : BaseStats
{
    public int xpValue;
    public delegate void EntityDied(EntityStats entity);
    public static event EntityDied OnEntityDeath;
    protected override void Die()
    {
        Debug.Log("Entity Death Event - Entity Stats");
        (gameObject.GetComponent("FirstPersonController") as MonoBehaviour).enabled = false;
        OnEntityDeath?.Invoke(this);
        Destroy(this.gameObject);
    }
}
