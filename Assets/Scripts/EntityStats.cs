using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class EntityStats : BaseStats
{
    public int xpValue;
    public delegate void EntityDied(EntityStats entity);
    public static event EntityDied OnEntityDeath;
    private void OnDeath()
    {
        if (Health < 0)
        {
            Debug.Log("Entity Death Event - Entity Stats");
            (gameObject.GetComponent("FirstPersonController") as MonoBehaviour).enabled = false;
            this.enabled = false;
            OnEntityDeath?.Invoke(this);
        }
    }
}
