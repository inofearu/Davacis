/*
FileName : HitResponder.cs 
FileType : C# Source File
Author : Christopher Huskinson
Created On : 31 August 2023, 18:24:46
Description : Script to handle receiving damage
*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JetBrains.Annotations;

public class HitResponder : MonoBehaviour, IHit
{
    private DamageModifier DamageModifier;
    List<DamageModifier.DamageModifierPair> modifiers;
    [SerializeField] BaseStats StatFile;
    [UsedImplicitly]
    private void Awake()
    {
        DamageModifier = GetComponent<DamageModifier>();
        modifiers = DamageModifier.modifiers;
    }
    public void OnHit(int incomingDamage, DamageModifier.DamageType damageType)
    {
        incomingDamage = CalculateFinalDamage(incomingDamage, damageType);
        Debug.Log($"{incomingDamage}-{StatFile.Health}-{this}");
        StatFile.Health -= incomingDamage;
        if (StatFile.Health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (gameObject.tag == "Player")
        {
            GameObject aStar = GameObject.FindGameObjectsWithTag("A*")[0];
            aStar.SetActive(false);
        }
        Destroy(this.gameObject);
    }

    private int CalculateFinalDamage(float incomingDamage, DamageModifier.DamageType damageType)
    {
        foreach (DamageModifier.DamageModifierPair modifierPair in modifiers)
        {
            if (damageType == modifierPair.type)
            {
                incomingDamage *= modifierPair.modifier;
            }
        }
        return Mathf.RoundToInt(incomingDamage);
    }
}
