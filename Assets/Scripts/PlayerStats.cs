using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStats : BaseStats
{
    public static PlayerStats instance {get; private set;} // instance can be gotten outside, but not set
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
    [UsedImplicitly]
    private void OnDeath()
    {
        if (Health < 0)
        {
            (gameObject.GetComponent("FirstPersonController") as MonoBehaviour).enabled = false;
            this.enabled = false;
            onPlayerDeath.Invoke();
        }
    }
}
// https://awesometuts.com/blog/singletons-unity/ singletons