using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;


public class PlayerStats : BaseStats
{
    public static PlayerStats instance {get; private set;} // instance can be gotten outside, but not set
    private int currentXP = 0;
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
        }
        }
    }
}
// https://awesometuts.com/blog/singletons-unity/ singletons