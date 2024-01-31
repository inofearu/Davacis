/*
FileName : DamageModifier.cs 
Namespace : Item
FileType : C# Source File
Author : Christopher Huskinson
Created On : 16 September 2023, 18:53
Description : Script to handle modifiers for incoming damage.
*/
using System.Collections.Generic;
using UnityEngine;
using JetBrains.Annotations;

namespace Item
{
    public class DamageModifier : MonoBehaviour
    {
        public enum DamageType
        {
            Blunt,
            Slash,
            Stab,
            Magic,
        }

        public System.Array damageTypes = System.Enum.GetValues(typeof(DamageType));

        [System.Serializable]
        public class DamageModifierPair
        {
            public DamageType type;
            public float modifier;
        }
        public List<DamageModifierPair> modifiers = new();

        private Dictionary<DamageType, float> _modifiers = new();

        [UsedImplicitly]
        private void Awake()
        {
            foreach (var pair in modifiers)
            {
                _modifiers[pair.type] = pair.modifier;
            }
        }
        [UsedImplicitly]
        private void Reset()
        {
            modifiers.Clear();
            foreach (DamageType type in damageTypes)
            {
                modifiers.Add(new DamageModifierPair { type = type, modifier = 1.0f }); // default value
            }
        }
    }
}