using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
