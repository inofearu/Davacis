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
        private int xp;
        public int level;
        private int xpToNextLevel = 10;
        [SerializeField] private float xpScale = 1.2f;
        [UsedImplicitly]
        new private void Awake()
        {
            base.Awake();
            CreateInstance();
            playerUI.UpdateXPDisplay(level, xp, xpToNextLevel);
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
        public int XP
        {
            get => xp;
            set
            {
                xp += value;
                while (xpToNextLevel < xp)
                {
                    xp -= xpToNextLevel;
                    level++;
                    xpToNextLevel = (int)(xpToNextLevel * xpScale);
                }
                playerUI.UpdateXPDisplay(level, xp, xpToNextLevel);
            }
        }
        public int Level
        {
            get => level;
            set
            {
                level = value;
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
            xp += enemy.xpValue;
        }
    }
}