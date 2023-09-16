/*
FileName : HitResponder.cs 
FileType : C# Source File
Author : Christopher Huskinson
Created On : 31 August 2023, 18:24:46
Description : Script to handle receiving damage
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JetBrains.Annotations;

public class HitResponder : MonoBehaviour, IHit
{
    private DamageModifier DamageModifier;
    private int health = 0;
    public int Health
    {
        get 
        { 
            return health; 
        }
        set 
        { 
            if ((health - value) < 0)
            {
                Die();
            }
        }
    }
    [UsedImplicitly]
    private void Awake()
    {
        DamageModifier = GetComponent<DamageModifier>();
        List<DamageModifier.DamageModifierPair> modifiers = DamageModifier.modifiers;
    }
    public void OnHit(int damage, DamageModifier damageType)
    {
    }

    private void Die()
    {
        Destroy(this.gameObject);
    }

    private void CalculateFinalDamage(int incomingDamage)
    {
        foreach (DamageModifier in DamageModifier.damageModifiers.modifiers)
    }
}
