using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class EntityStats : MonoBehaviour
{
    private float health;
    public float maxHealth;
    public float Health
    {
        get => health;
        set
        {
            if (health + value > maxHealth)
            {
                health = maxHealth;
            }
            health = value;
        }
    }
}
