/*
FileName : DamageModifier.cs 
FileType : C# Source File
Author : Christopher Huskinson
Created On : 15 September 2023, 13:27:36
Description : Script to handle receiving damage
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JetBrains.Annotations;
public class DamageModifier : MonoBehaviour
{
    public enum DamageType
    {
        Blunt,
        Slash,
        Stab,
        Magic,
    }

    [System.Serializable]
    public class DamageModifierPair
    {
        public DamageType damage;
        public float modifier;
    }

    [SerializeField]
    public List<DamageModifierPair> modifiers = new List<DamageModifierPair>();

    private Dictionary<DamageType, float> _modifiers = new Dictionary<DamageType, float>();

    private void Awake()
    {
        foreach (var pair in modifiers)
        {
            _modifiers[pair.damage] = pair.modifier;
        }
    }
    [UsedImplicitly]
    private void Reset()
    {
        modifiers.Clear();
        foreach (DamageType type in System.Enum.GetValues(typeof(DamageType)))
        {
            modifiers.Add(new DamageModifierPair { damage = type, modifier = 1.0f }); // default value
        }
    }
}
