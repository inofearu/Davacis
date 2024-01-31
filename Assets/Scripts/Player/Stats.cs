/*
FileName : Stats.cs 
Namespace: Player
FileType : C# Source File
Author : Christopher Huskinson
Created On : 27 April 2023, 13:57
Description : Script to handle player stats, is a singleton.
*/
using JetBrains.Annotations;
using UnityEngine;

namespace Player
{
    public class Stats : Entity.Stats
    {
        [SerializeField] private UI playerUI;
        public static Stats instance { get; private set; } // instance can be gotten from outside, but not set
        private int currentXP;
        private int currentLevel;
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
            get => base.Health;
            set
            {
                base.Health = value;
                playerUI.UpdateHPDisplay(Health, MaxHealth);
            }
        }
        protected override void Die()
        {
            GameObject aStar = GameObject.FindGameObjectsWithTag("A*")[0];
            aStar.SetActive(false);
            playerUI.DisplayDeathScreen();
            Destroy(this.gameObject);
        }
        [UsedImplicitly]
        private void OnEnable()
        {
            Enemy.Stats.OnEnemyDeath += EnemyKilled;
        }
        [UsedImplicitly]
        private void OnDisable()
        {
            Enemy.Stats.OnEnemyDeath -= EnemyKilled;
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
        public int CurrentXP
        {
            get => currentXP;
            set
            {
                currentXP += value;
                while (xpToNextLevel < currentXP)
                {
                    currentXP -= xpToNextLevel;
                    currentLevel++;
                    xpToNextLevel = (int)(xpToNextLevel * xpScale);
                }
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
        private void EnemyKilled(Enemy.Stats enemy)
        {
            Debug.Log("Enemy killed - Player Stats");
            CurrentXP += enemy.xpValue;
        }
    }
}