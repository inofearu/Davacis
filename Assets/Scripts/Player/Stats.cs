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
using System.Collections.Generic;

namespace Player
{
    public class Stats : Entity.Stats
    {
        [SerializeField] private UI playerUI;
        public static Stats instance { get; private set; } // instance can be gotten from outside, but not set
        private int xp;
        private int level;
        private int xpToNextLevel = 10;
        private float multiplier = 0.8f;
        [SerializeField] private float xpScale = 1.2f;

        private List<int> davacis;
        [UsedImplicitly]
        new private void Awake()
        {
            base.Awake();
            CreateInstance();
            playerUI.UpdateXPDisplay(level, xp, xpToNextLevel);
            playerUI.UpdateHPDisplay(Health, MaxHealth);

            davacis = new List<int>() { 0, 0, 0, 0, 0, 0, 0 }; // TODO: this will need to change to load in the future
            // Dexterity, Arcana, Vitality, Awareness, Charisma, Intelligence and Strength
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
                playerUI.UpdateXPDisplay(level, xp, xpToNextLevel);
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

        private void StatUp(List<int> old, int balance)
        {
            int chosenStat = 2; //! PLACEHOLDER
            List<int> costs = new List<int> { };
            List<bool> affordability = new List<bool> { };
            foreach (int stat in old)
            {
                int cost = Mathf.RoundToInt(stat * multiplier);
                if (cost == 0)
                {
                    cost = 1;
                }
                costs.Add(cost);
                bool affordable = true;
                if (cost > balance)
                {
                    affordable = false;
                }
                affordability.Add(affordable);
            }
        }

        private void LevelUp()
        {
               
        }
    }
}