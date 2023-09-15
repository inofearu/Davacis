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
    private int health = 0;
    private enum damageTypes
    { 
        Blunt,
        Slash,
        Magic
    }
    [SerializeField] List<damageTypes> damageTypeKeys = new List<damageTypes>();
    [SerializeField] List<float> damageTypeValues = new List<float>();
    private Dictionary<damageTypes, float> modifiers;
    public int Health   // property
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
        if (damageTypeKeys.Count != damageTypeValues.Count)
        {
            Debug.LogWarning("damageTypeKeys length differs to damageTypeValues length. Assuming Default values.");
        }
        modifiers = new Dictionary<damageTypes, float>();
        for (int i = 0; i < Mathf.Min(damageTypeKeys.Count, damageTypeValues.Count); i++)
        {
            modifiers[damageTypeKeys[i]] = damageTypeValues[i];
        }
    }
    public void OnHit(RaycastHit hitData, int damage)
    {
    }

    private void TakeDamage(RaycastHit hitData, int damage)
    {   
    }

    private void Die()
    {
    }

    private void CalculateFinalDamage(int incomingDamage)
    {
    }
}
