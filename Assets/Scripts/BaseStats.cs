using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JetBrains.Annotations;

public class BaseStats : MonoBehaviour
{
    protected float health;
    public float maxHealth;
    public virtual float Health
    {
        get => health;
        set
        {
            if (value > maxHealth) // max cap
            {
                health = maxHealth;
            }
            else
            {
                health = value;
                if (health < 0)
                {
                    Die();
                }
            }
        }
    }
    protected virtual void Die()
    {
        Destroy(this.gameObject);
    }
    [UsedImplicitly]
    protected void Awake() //! This will be an issue on save/loading for the player
    {
        health = maxHealth;
    }
}
