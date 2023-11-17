using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JetBrains.Annotations;

public class BaseStats : MonoBehaviour
{
    private float health;
    public float maxHealth;
    public float Health
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
            }
        }
    }
    [UsedImplicitly]
    protected void Awake() //! This will be an issue on save/loading for the player
    {
        health = maxHealth;
    }
}
