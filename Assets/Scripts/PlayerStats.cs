using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance {get; private set;} 
    private float health;
    public float maxHealth;
    public UnityEvent onPlayerDeath;
    [UsedImplicitly]
    private void Awake()
    {
        CreateInstance();
    }
    private void CreateInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    public float Health
    {
        get => health;
        set
        {
            if (health + value > maxHealth) // max cap
            {
                health = maxHealth;
            }
            health = value;
        }
    }
    [UsedImplicitly]
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
// https://awesometuts.com/blog/singletons-unity/ singletons
// health in HitResponder and PlayerStats arent the same