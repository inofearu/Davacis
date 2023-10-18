using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStats : MonoBehaviour
{
    private float health;
    private float maxHealth;
    public UnityEvent onPlayerDeath;

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
    private void OnDeath()
    {
        if (health < 0)
        {
            (gameObject.GetComponent("FirstPersonController") as MonoBehaviour).enabled = false;
            this.enabled = false;
            onPlayerDeath.Invoke();
        }
    }
}
