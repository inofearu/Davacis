using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] int maxHealth;
    public int health;
    [SerializeField] int maxMana;
    public int mana;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        mana = maxMana;   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
