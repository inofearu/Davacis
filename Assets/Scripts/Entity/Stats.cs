/*
FileName : Stats.cs 
Namespace : Entity
FileType : C# Source File
Author : Christopher Huskinson
Created On : 8th November 2023, 12:31
Description : Exists as a basic stat class for other types to extend on.
*/
using UnityEngine;
using JetBrains.Annotations;

namespace Entity
{
    public class Stats : MonoBehaviour
    {
        protected float health;
        public float maxHealth;
        public virtual float Health
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
                    if (health < 0)
                    {
                        Die();
                    }
                }
            }
        }
        protected virtual void Die()
        {
            Destroy(this.gameObject);
        }
        [UsedImplicitly]
        protected void Awake() //! This will be an issue on save/loading for the player
        {
            health = maxHealth;
        }
    }
}