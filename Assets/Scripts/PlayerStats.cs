using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;


public class PlayerStats : BaseStats
{
    [SerializeField] private PlayerUI playerUI;
    public static PlayerStats instance {get; private set;} // instance can be gotten from outside, but not set
    private int currentXP = 0;
    private int currentLevel = 0;
    private int xpToNextLevel = 10;
    [SerializeField] private float xpScale = 1.2f;
    [UsedImplicitly]
    new private void Awake()
    {
        base.Awake();
        CreateInstance();
        playerUI.UpdateXPDisplay(currentLevel, currentXP, xpToNextLevel);
        playerUI.UpdateHPDisplay(Health, MaxHealth);
    }
    public override float Health
    {
        get => base.health;
        set
        {
            base.health = value;
            playerUI.UpdateHPDisplay(Health, MaxHealth);
        }
    }
    protected override void Die()
    {
        GameObject aStar = GameObject.FindGameObjectsWithTag("A*")[0];
        aStar.SetActive(false);
        Destroy(gameObject);
    }
    [UsedImplicitly]
    private void OnEnable()
    {
        EntityStats.OnEntityDeath += EnemyKilled;
    }
    [UsedImplicitly]
    private void OnDisable()
    {
        EntityStats.OnEntityDeath -= EnemyKilled;
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
    private void OnDeath() //? Do we need a HitResponder file still?
    {
        if (Health < 0)
        {
            (gameObject.GetComponent("FirstPersonController") as MonoBehaviour).enabled = false;
            this.enabled = false;
        }
    }
    public int CurrentXP
    {
        get => currentXP;
        set
        {
            currentXP += value;
            currentLevel += xpToNextLevel / currentXP;
            xpToNextLevel = Mathf.RoundToInt(xpToNextLevel * xpScale);
            playerUI.UpdateXPDisplay(currentLevel, currentXP, xpToNextLevel);
        }
    }
    public int CurrentLevel
    {
        get => CurrentLevel;
        set
        {
            currentLevel += value;
        }
    }
    public float MaxHealth
    {
        get => maxHealth;
        set
        {
            maxHealth = value;
        }
    }
    private void EnemyKilled(EntityStats entity)
    {
        Debug.Log("Enemy killed - Player Stats");
        CurrentXP += entity.xpValue;
    }
}