/*
FileName : Stats.cs 
Namespace: Enemy
FileType : C# Source File
Author : Christopher Huskinson
Created On : 8th November 2023, 11:23
Description : Script to track enemy stats.
*/
using UnityEngine;

namespace Enemy
{
    public class Stats : Entity.Stats
    {
        public int xpValue;
        public delegate void EnemyDied(Stats enemy);
        public static event EnemyDied OnEnemyDeath;
        protected override void Die()
        {
            Debug.Log("Enemy Death Event - Enemy Stats");
            OnEnemyDeath?.Invoke(this);
            Destroy(this.gameObject);
        }
    }
}